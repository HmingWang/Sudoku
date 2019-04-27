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
        private static int currentNumber;
        private static bool isNoteMode=false;
        public static int CurrentNumber { get => currentNumber; set => currentNumber = value; }
        public static bool IsNoteMode { get => isNoteMode; set => isNoteMode = value; }

        private static Random random = new Random();

        private static Grid MakeGrid(int row, int col)
        {
            Grid g = new Grid();
            while (row-- > 0)
                g.RowDefinitions.Add(new RowDefinition());

            while (col-- > 0)
                g.ColumnDefinitions.Add(new ColumnDefinition());

            //g.Background = new SolidColorBrush(Color.FromRgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)));
            //g.Margin = new Thickness(0);
            //g.HorizontalAlignment = HorizontalAlignment.Stretch;
            //g.VerticalAlignment = VerticalAlignment.Stretch;
            //g.ShowGridLines = true;
            return g;
        }
        private static void MakeG1()
        {
            G1 = MakeGrid(3, 3);
        }
        private static void MakeG2()
        {
            G2 = new Grid[3, 3];
            int row = 0, col = 0;
            for (int i = 0; i < 9; ++i)
            {
                row = i / 3;
                col = i % 3;
                G2[row, col] = MakeGrid(3, 3);

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
        private static void MakeG3()
        {
            G3 = new Grid[9, 9];
            int row, col, x, y, r, c;
            for (int i = 0; i < 81; ++i)
            {
                row = i / 9 / 3;
                col = i % 9 / 3;
                r = i / 9 % 3;
                c = i % 9 % 3;
                x = i / 9;
                y = i % 9;
                G3[x, y] = MakeGrid(3, 3);
                int[] index = new int[2];
                index[0] = x;
                index[1] = y;
                G3[x, y].Tag = index;
                G3[x, y].MouseLeftButtonDown += new MouseButtonEventHandler(MouseDown_SetNumber);
                G3[x, y].KeyDown += new KeyEventHandler(KeyDown_SetNumber);
                G2[row, col].Children.Add(G3[x, y]);
                Grid.SetRow(G3[x, y], r);
                Grid.SetColumn(G3[x, y], c);
                
                G3Prepare(G3[x, y]);

                var b = new Border();
                //b.Margin = new Thickness(1);
                b.BorderBrush = Brushes.Gray;
                b.BorderThickness = new Thickness(1);

                Grid.SetRow(b, 0);
                Grid.SetColumn(b, 0);
                Grid.SetRowSpan(b, 3);
                Grid.SetColumnSpan(b, 3);
                G3[x, y].Children.Add(b);
            }
        }

        public static void Reset()
        {
            MakeMap();
        }

        private static void G3Prepare(Grid g)
        {
            Label lblbg = new Label();
            lblbg.HorizontalAlignment = HorizontalAlignment.Stretch;
            lblbg.VerticalAlignment = VerticalAlignment.Stretch;
            Grid.SetRow(lblbg, 0);
            Grid.SetColumn(lblbg, 0);
            Grid.SetRowSpan(lblbg, 3);
            Grid.SetColumnSpan(lblbg, 3);
            g.Children.Add(lblbg);

            var b = new Border();
            //b.Margin = new Thickness(1);
            b.BorderBrush = Brushes.Gray;
            b.BorderThickness = new Thickness(1);
            Grid.SetRow(b, 0);
            Grid.SetColumn(b, 0);
            Grid.SetRowSpan(b, 3);
            Grid.SetColumnSpan(b, 3);
            g.Children.Add(b);
        }


        private static void MouseDown_SetNumber(object sender, MouseButtonEventArgs args)
        {
            int x = ((sender as Grid).Tag as int[])[0];
            int y = ((sender as Grid).Tag as int[])[1];
            if (CurrentNumber > 0)
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
            
        }

        private static void KeyDown_SetNumber(object sender, KeyEventArgs args)
        {

        }
        public static Grid MakeMainGrid()
        {
            
            MakeG1();
            MakeG2();
            MakeG3();
            MakeMap();

            return G1;
        }

        private static void MakeMap()
        {
            oper = new SudokuOperator();
            oper.InitSudokuGrid();
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {                    
                    ShowGrid(i, j);                   
                }
            }
        }

        private static void ShowGrid(int row,int col)
        {
            Grid g = G3[row, col];
            g.Children.Clear();
           
            G3Prepare(g);

            SudokuCell cell= oper.GetCell(row, col);
            if (cell.Number != 0)
            {
                Label lblnum = new Label();
                lblnum.Content = cell.Number.ToString(); ;
                lblnum.FontSize = 24;
                lblnum.HorizontalAlignment = HorizontalAlignment.Center;
                lblnum.VerticalAlignment = VerticalAlignment.Center;
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
                    g.Children.Add(lbl);
                    Grid.SetRow(lbl, (i - 1) / 3);
                    Grid.SetColumn(lbl, (i - 1) % 3);
                }
            }
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
            ShowGrid(row, col);
        }

        public static void WirteNote(int num, int row, int col)
        {
            oper.SetClue(num, row, col);
            ShowGrid(row, col);
        }

        private static void lbl_DoubleClick(object sender, MouseButtonEventArgs arg)
        {
            MessageBox.Show((sender as Label).Content.ToString());
        }
    }
}
