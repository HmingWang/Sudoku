using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class SudokuGrid
    {
        int number;
        int[] notes;
        bool isDefault;

        public SudokuGrid()
        {
            number = '0';
            Notes = new int[9];
            isDefault = false;
        }

        public int Number { get => number; set => number = value; }
        public bool IsDefault { get => isDefault; set => isDefault = value; }
        public int[] Notes { get => notes; set => notes = value; }
    }
}
