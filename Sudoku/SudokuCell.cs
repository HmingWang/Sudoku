using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class SudokuCell
    {
        int number;
        int[] clues;
        bool isDefault;
        int key;  //the real anwser

        public SudokuCell()
        {
            number = 0;
            clues = new int[9];
            isDefault = false;
        }

        public int Number { get => number; set => number = value; }
        public bool IsDefault { get => isDefault; set => isDefault = value; }
        public int[] Clues { get => clues; set => clues = value; }
        public int Key { get => key; set => key = value; }
    }
}
