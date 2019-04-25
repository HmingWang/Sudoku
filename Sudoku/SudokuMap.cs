using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sudoku
{
    class SudokuMap
    {
        private static SudokuGrid[,] ansMatrix; //9x9

        internal static SudokuGrid[,] AnsMatrix { get => ansMatrix; set => ansMatrix = value; }

        public static void SetNumber(int num, int row, int col)
        {
            AnsMatrix[row, col].Number = num;
            AnsMatrix[row, col].Notes =null;
            AnsMatrix[row, col].IsDefault = false;
        }

        public static void SetNote(int num, int row, int col)
        {
            AnsMatrix[row, col].Notes[num] = num;
        }
    }
}
