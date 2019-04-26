using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
namespace Sudoku_Test
{
    [TestClass]
    public class AlgorithmTest
    {
        [TestMethod]
        public void Test_CheckCell()
        {
            SudokuAlgorithm.GenSudokuPazzle();
            foreach(var i in SudokuAlgorithm.GetPazzle())
            {
                int[] xy = SudokuAlgorithm.GetCoordinate(i);
                bool reslut = SudokuAlgorithm.CheckCell(xy[0],xy[1]);
                Console.Out.WriteLine(i);
                Assert.AreEqual(reslut, true);
            }
        }
    }
}
