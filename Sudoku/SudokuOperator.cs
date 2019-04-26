using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class SudokuOperator
    {
        private SudokuGrid sudokuGrid;

        public SudokuOperator()
        {
            sudokuGrid = new SudokuGrid();
        }
        public void InitSudokuGrid()
        {
            SudokuAlgorithm.GenSudokuPazzle();
            int[,] pazzle = SudokuAlgorithm.GetPazzle();
            int[,] solution = SudokuAlgorithm.GetSolution();

            sudokuGrid.InitSudokuGrid(pazzle, solution);
 
        }

        public int GetNumber(int row,int col)
        {
            return sudokuGrid.GetNumber(row, col);
        }

        public void SetNumber(int num,int row, int col)
        {
            sudokuGrid.SetNumber(num, row, col);
        }

        public void SetClue(int num, int row, int col)
        {
            sudokuGrid.SetClue(num, row, col);
        }

        public int[] GetClues(int row,int col)
        {
            return sudokuGrid.GetClues(row, col);
        }

        public bool CheckCell(int row,int col)
        {
            return SudokuAlgorithm.CheckCell(row, col);
        }
    }
}
