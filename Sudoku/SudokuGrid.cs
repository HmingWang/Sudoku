using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sudoku
{
    class SudokuGrid
    {
        private SudokuCell[,] cellMatrix; //9x9

        public SudokuCell[,] CellMatrix { get => cellMatrix; set => cellMatrix = value; }

        public SudokuGrid()
        {
            cellMatrix = new SudokuCell[9, 9];
            for(int i = 0; i < 9; ++i)
                for(int j=0;j<9;++j)
            {
                cellMatrix[i,j] = new SudokuCell();
            }
        }

        public void InitSudokuGrid(int[,] pazzle,int[,] solution)
        {
            for(int i=0;i<9;++i)
                for(int j = 0; j < 9; ++j)
                {
                    cellMatrix[i, j].Number = pazzle[i,j];
                    cellMatrix[i, j].Clues = null;
                    cellMatrix[i, j].IsDefault = pazzle[i,j]!=0;
                    cellMatrix[i, j].Key = solution[i, j];
                }
        }

        public void SetNumber(int num, int row, int col)
        {
            if (cellMatrix[row, col].IsDefault) return;

            cellMatrix[row, col].Number = num;
            cellMatrix[row, col].Clues =null;
            cellMatrix[row, col].IsDefault = false;
        }

        public int GetNumber(int row, int col)
        {
            return CellMatrix[row, col].Number;
        }

        public SudokuCell GetCell(int row, int col)
        {
            return cellMatrix[row, col];
        }

        public void SetClue(int num, int row, int col)
        {
            if (CellMatrix[row, col].IsDefault) return;
            CellMatrix[row, col].Number = 0;
            if (CellMatrix[row, col].Clues == null)
                CellMatrix[row, col].Clues = new int[9];
            CellMatrix[row, col].Clues[num-1] = num;
        }
        public int[] GetClues( int row, int col)
        {
            if (CellMatrix[row, col].IsDefault) return null;
            return CellMatrix[row, col].Clues;
        }
    }
}
