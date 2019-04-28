using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Sudoku
{
    static class Viewer
    {
        private static SudokuOperator oper;
        private static Grid G1; //第一级grid 顶层。
        private static Grid[,] G2; //第二级，grid 3x3
        private static Grid[,] G3;//第三级 grid 9x9
        private static int[] currentCell;
        private static int currentNumber;
        private static bool isNoteMode=false;
        public static int CurrentNumber { get => currentNumber; set => currentNumber = value; }
        public static bool IsNoteMode { get => isNoteMode; set => isNoteMode = value; }

        private static Random random = new Random();

        private static Grid Gride_Make(int row, int col)
        {
            Grid g = new Grid();
            while (row-- > 0)
                g.RowDefinitions.Add(new RowDefinition());

            while (col-- > 0)
                g.ColumnDefinitions.Add(new ColumnDefinition());

            g.Focusable = true;
            return g;
        }
        private static void G1_Make()
        {
            G1 = Gride_Make(3, 3);

            var b = new Border();
            //b.Padding = new Thickness(1);
            b.BorderBrush = Brushes.Black;
            b.BorderThickness = new Thickness(3);
            Grid.SetRow(b, 0);
            Grid.SetColumn(b, 0);
            Grid.SetRowSpan(b, 3);
            Grid.SetColumnSpan(b, 3);
            G1.Children.Add(b);
        }
        private static void G2_Make()
        {
            G2 = new Grid[3, 3];
            int row = 0, col = 0;
            for (int i = 0; i < 9; ++i)
            {
                row = i / 3;
                col = i % 3;
                G2[row, col] = Gride_Make(3, 3);

                G1.Children.Add(G2[row, col]);
                Grid.SetRow(G2[row, col], row);
                Grid.SetColumn(G2[row, col], col);

                var b = new Border();
                //b.Margin = new Thickness(1);
                b.BorderBrush = Brushes.Black;
                b.BorderThickness = new Thickness(2);
                Grid.SetRow(b, 0);
                Grid.SetColumn(b, 0);
                Grid.SetRowSpan(b, 3);
                Grid.SetColumnSpan(b, 3);
                G2[row, col].Children.Add(b);
            }

            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            //G2[0, 0].Background = brush;
            //G2[0, 2].Background = brush;
            //G2[1, 1].Background = brush;
            //G2[2, 0].Background = brush;
            //G2[2, 2].Background = brush;

            

        }
        private static void G3_Make()
        {
            G3 = new Grid[9, 9];
            int f, t, x, y, r, c;
            for (int i = 0; i < 81; ++i)
            {
                int[] xy = SudokuAlgorithm.GetCoordinate(i);                
                x = xy[0];
                y = xy[1];

                int[] ft = SudokuAlgorithm.GetChuteCoordinate(x,y);
                f = ft[0];
                t = ft[1];

                int[] rc = SudokuAlgorithm.GetSubCoordinate(x, y);
                r = rc[0];
                c = rc[1];
   
                G3[x, y] = Gride_Make(3, 3);
                G3[x, y].Tag = xy;
                G3[x, y].MouseLeftButtonDown += new MouseButtonEventHandler(Grid_MouseDown);
                G3[x, y].KeyDown += new KeyEventHandler(Grid_KeyDown);                
                G2[f, t].Children.Add(G3[x, y]);
                Grid.SetRow(G3[x, y], r);
                Grid.SetColumn(G3[x, y], c);
                
                Grid_Init(G3[x, y]);
            }
        }

        public static void G3_Reset()
        {
            oper.InitSudokuGrid();
            foreach (Grid i in G3)
            {
                Grid_Init(i);
            }
        }

        private static void Grid_Init(Grid g)
        {
            g.Children.Clear();
            Label lblbg = new Label();
            lblbg.HorizontalAlignment = HorizontalAlignment.Stretch;
            lblbg.VerticalAlignment = VerticalAlignment.Stretch;
            lblbg.Name = "BKG";
            Grid.SetRow(lblbg, 0);
            Grid.SetColumn(lblbg, 0);
            Grid.SetRowSpan(lblbg, 3);
            Grid.SetColumnSpan(lblbg, 3);
            g.Children.Add(lblbg);

            var b = new Border();
            b.Margin = new Thickness(1);
            b.BorderBrush = Brushes.Gray;
            b.BorderThickness = new Thickness(1);
            Grid.SetRow(b, 0);
            Grid.SetColumn(b, 0);
            Grid.SetRowSpan(b, 3);
            Grid.SetColumnSpan(b, 3);
            g.Children.Add(b);

            int[] index = g.Tag as int[];
            int row = index[0];
            int col = index[1];
            SudokuCell cell = oper.GetCell(row, col);
            if (cell.Number != 0)
            {
                Label lblnum = new Label();
                lblnum.Content = cell.Number.ToString(); ;
                lblnum.FontSize = 24;
                lblnum.HorizontalAlignment = HorizontalAlignment.Center;
                lblnum.VerticalAlignment = VerticalAlignment.Center;
                lblnum.Name = "NUM";
                Grid.SetRow(lblnum, 0);
                Grid.SetColumn(lblnum, 0);
                Grid.SetRowSpan(lblnum, 3);
                Grid.SetColumnSpan(lblnum, 3);
                g.Children.Add(lblnum);
                if (!cell.IsDefault) lblnum.Foreground = Brushes.Blue;
            }
            else
            {
                if (cell.Clues == null) return;
                foreach (int i in cell.Clues)
                {
                    if (i == 0) continue;
                    Label lbl = new Label();
                    lbl.Content = i.ToString();
                    lbl.HorizontalAlignment = HorizontalAlignment.Center;
                    lbl.VerticalAlignment = VerticalAlignment.Center;
                    lbl.Name = "CLU" + i;
                    g.Children.Add(lbl);
                    Grid.SetRow(lbl, (i - 1) / 3);
                    Grid.SetColumn(lbl, (i - 1) % 3);
                }
            }
        }

        private static void Grid_SetBackground(Grid g,Brush brush)
        {
            foreach(var i in g.Children)
            {
                if(i is Label)
                {
                    if((i as Label).Name != null&&(i as Label).Name.ToString() == "BKG")
                    {
                        (i as Label).Background = brush;
                        (i as Label).Margin = new Thickness(2);
                    }
                }
            }
        }

        private static int Grid_GetNumber(Grid g)
        {
            foreach (var e in g.Children)
            {
                if (e is Label && (e as Label).Name == "NUM")
                {
                    return int.Parse((e as Label).Content.ToString());
                }
            }
            return 0;
        }

        private static void G3_ClearBackground()
        {
            foreach(Grid i in G3)
            {
                Grid_SetBackground(i, Brushes.White);
            }
        }
        private static void Grid_Select(int row, int col)
        {
            currentCell = new int[2];
            currentCell[0] = row;
            currentCell[1] = col;

            if(Grid_GetNumber(G3[row, col]) == 0)
            {
                G3_ClearBackground();
                return;
            }

            foreach (Grid i in G3)
            {                
                if (Grid_GetNumber(i) == Grid_GetNumber(G3[row, col]))
                {
                    Grid_SetBackground(i, Brushes.LightYellow);
                }
                else
                {
                    Grid_SetBackground(i, Brushes.White);
                    foreach(var e in i.Children)
                    {
                        if(e is Label&&((e as Label).Name=="CLU"+ Grid_GetNumber(G3[row, col])))
                        {
                            (e as Label).Foreground = Brushes.Blue;
                        }
                    }
                }
            }

            Brush b = Brushes.Gainsboro;
            for (int i = 0; i < 9; ++i)
            {
                Grid_SetBackground(G3[i, col], b);
            }
            for (int j = 0; j < 9; ++j)
            {
                Grid_SetBackground(G3[row, j], b);
            }

            foreach(var g in (G3[row,col].Parent as Grid).Children)
            {
                if(g is Grid)
                    Grid_SetBackground(g as Grid, b);
            }

            Grid_SetBackground(G3[row, col], Brushes.LightYellow);

        }

        private static void Grid_MouseDown(object sender, MouseButtonEventArgs args)
        {
            int x = ((sender as Grid).Tag as int[])[0];
            int y = ((sender as Grid).Tag as int[])[1];
            G3[x, y].Focus();
            if (CurrentNumber > 0&&CurrentNumber!=Grid_GetNumber(G3[x, y])&&!oper.GetCell(x,y).IsDefault)
            {
                if (IsNoteMode)
                {
                    WirteNote(CurrentNumber, x, y);
                }
                else
                {
                    WriteNumber(CurrentNumber, x, y);                    
                }
            }
            else
            {
                Grid_Select(x, y);
            }
        }

        public static void Grid_KeyDown(object sender, KeyEventArgs args)
        {
            if (currentCell == null) return;
            int x = currentCell[0];
            int y = currentCell[1];

            int number = 0;

            if (args.Key >= Key.D1 && args.Key <= Key.D9)
            {
                number = args.Key - Key.D0;
            }
            else if (args.Key >= Key.NumPad1 && args.Key <= Key.NumPad9)
            {
                number = args.Key - Key.NumPad0;
            }
            else return ;

            if (IsNoteMode)
            {
                WirteNote(number, x, y);
            }
            else
            {
                WriteNumber(number, x, y);
                Grid_Select(x, y);
            }


        }
        public static Grid Map_Init()
        {
            oper = new SudokuOperator();
            oper.InitSudokuGrid();

            G1_Make();
            G2_Make();
            G3_Make();

            return G1;
        }

        public static void WriteNumber(int num, int row, int col)
        {
            oper.SetNumber(num, row, col);
            if(oper.CheckCell(row, col) == false)
            {
                oper.SetNumber(0, row, col);
                MessageBox.Show("错误的数字");
                return;
            }
            Grid_Init(G3[row, col]);
        }

        public static void WirteNote(int num, int row, int col)
        {
            oper.SetClue(num, row, col);
            Grid_Init(G3[row, col]);
        }

        private static void lbl_DoubleClick(object sender, MouseButtonEventArgs arg)
        {
            MessageBox.Show((sender as Label).Content.ToString());
        }
    }
}
