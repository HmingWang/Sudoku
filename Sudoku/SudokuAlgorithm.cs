using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public static class SudokuAlgorithm
    {
        static int[,] pazzle = new int[9, 9];
        static int[,] solution = new int[9, 9];

        public static int[,] Pazzle { get => pazzle; set => pazzle = value; }
        public static int[,] Solution { get => solution; set => solution = value; }

        public static int[] GetCoordinate(int index)
        {
            int[] xy = new int[2];
            xy[0] = index / 9;
            xy[1] = index % 9;
            return xy;
        }
        public static int[] GetChuteCoordinate(int row,int col)
        {
            int[] ft = new int[2];
            ft[0] = row / 3;
            ft[1] = col / 3;
            return ft;
        }
        public static int[] GetSubCoordinate(int row,int col)
        {
            int[] xy = new int[2];
            xy[0] = row % 3;
            xy[1] = col % 3;
            return xy;
        }
        public static int GetIndex(int row,int col)
        {
            return row * 9 + col;
        }
        public static int[,] GetBlock(int floor,int tower)
        {
            int[,] block = new int[3, 3];
            for(int i=0;i<3;++i)
                for(int j = 0; j < 3; ++j)
                {
                    block[i, j] = Pazzle[floor * 3 + i, tower * 3 + j];
                }

            return block;
        }
        public static void GenSudokuPazzle()
        {
            Solution = new int[,]{
                { 8,7,1,9,3,2,6,4,5},
                { 4,9,5,8,6,1,2,3,7},
                { 6,3,2,7,5,4,8,1,9},
                { 5,2,8,4,7,3,1,9,6},
                { 9,1,3,6,2,5,7,8,4},
                { 7,6,4,1,9,8,3,5,2},
                { 2,8,7,3,4,9,5,6,1},
                { 1,4,6,5,8,7,9,2,3},
                { 3,5,9,2,1,6,4,7,8}
            };

            Pazzle = new int[,] {
                { 8,0,0,0,3,2,0,4,5},
                { 4,0,0,8,0,1,2,0,0},
                { 0,3,2,0,5,4,0,1,0},
                { 5,2,0,4,7,3,1,0,6},
                { 0,0,0,6,0,5,7,8,4},
                { 0,6,4,1,0,0,0,5,2},
                { 2,0,0,3,4,9,0,6,1},
                { 1,0,6,5,0,0,9,2,3},
                { 0,5,0,2,1,6,4,7,0}
            };
        }

        private static bool CheckRow(int row)
        {
            int[] bits = new int[9];

            for(int j = 0; j < 9; ++j)
            {
                if (Pazzle[row, j] == 0) continue;

                if (bits[Pazzle[row, j] - 1] ==1) return false;
                bits[Pazzle[row, j] - 1] = 1;
            }
            return true;
        }

        private static bool CheckColumn(int col)
        {
            int[] bits = new int[9];
            for (int i = 0; i < 9; ++i)
            {
                if (Pazzle[i, col] == 0) continue;
                if (bits[Pazzle[i, col] - 1] == 1) return false;
                bits[Pazzle[i, col] - 1] = 1;
            }
            return true;
        }

        private static bool CheckBlock(int row,int col)
        {
            int[] bits = new int[9];
            int[] xy = GetChuteCoordinate(row, col);
            int[,] block = GetBlock( xy[0],xy[1]);
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 3; ++j)
                {
                    if (Pazzle[i, j] == 0) continue;
                    if (bits[Pazzle[i, j] - 1] == 1) return false;
                    bits[Pazzle[i, j] - 1] = 1;
                }
            return true;
        }

        public static bool CheckCell(int row ,int col)
        {
            return CheckRow(row) && CheckColumn(col) && CheckBlock(row,col);
        }
    }
}
