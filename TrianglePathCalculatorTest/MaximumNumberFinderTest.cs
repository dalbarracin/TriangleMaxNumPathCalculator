using System;
using NUnit.Framework;
using TrianglePathCalculator;
using TrianglePathCalculator.Common.Enum;
using TrianglePathCalculator.Helpers;

namespace TrianglePathCalculatorTest
{
    public class MaximumNumberFinderTest
    {
        MaximumNumberPathFinder finder;

        [SetUp]
        public void Setup()
        {
            finder = new MaximumNumberPathFinder();
        }

        [Test]
        public void Find_Max_Number_And_Path_Input_Excercise()
        {
            var input =
@"215
192 124
117 269 442
218 836 347 235
320 805 522 417 345
229 601 728 835 133 124
248 202 277 433 207 263 257
359 464 504 528 516 716 871 182
461 441 426 656 863 560 380 171 923
381 348 573 533 448 632 387 176 975 449
223 711 445 645 245 543 931 532 937 541 444
330 131 333 928 376 733 017 778 839 168 197 197
131 171 522 137 217 224 291 413 528 520 227 229 928
223 626 034 683 839 052 627 310 713 999 629 817 410 121
924 622 911 233 325 139 721 218 253 223 107 233 230 124 233";

            var result = finder.Search(input);

            Assert.Greater(result.MaxSum, 0);
            Assert.AreEqual(8186, result.MaxSum);
            Console.WriteLine($"Max. number found: {result.MaxSum}");
            Console.WriteLine($"Path: {result.Path}");
            Console.WriteLine(input);
        }

        [Test]
        public void Get_Zero_And_Empty_Path_When_Input_Is_Empty_Or_Null()
        {
            var input = @"";

            var result = finder.Search(input);

            Assert.AreEqual(0, result.MaxSum);
            Assert.AreEqual(string.Empty, result.Path);
            Console.WriteLine($"Max. number found: {result.MaxSum}");
            Console.WriteLine($"Path: {result.Path}");
            Console.WriteLine(input);
        }

        [Test]
        public void Throw_FormatException_When_Input_Contains_Not_Valid_Element()
        {
            {
                var input = @"3$€D";
                var ex = Assert.Throws<FormatException>(() => finder.Search(input));
                Assert.AreEqual("Element 3$€D Pos. [0,0] is not a valid number.", ex.Message);
                Console.WriteLine(ex.Message);
            }

            {
                var input =
@"215
192 124
117 269 442
218 836 347 235
320 GSF 522 417 345
229 601 728 835 133 124";
                var ex = Assert.Throws<FormatException>(() => finder.Search(input));
                Assert.AreEqual("Element GSF Pos. [4,1] is not a valid number.", ex.Message);
                Console.WriteLine(ex.Message);
            }

            {
                var input =
@"215
192 4JFR";
                var ex = Assert.Throws<FormatException>(() => finder.Search(input));
                Assert.AreEqual("Element 4JFR Pos. [1,1] is not a valid number.", ex.Message);
                Console.WriteLine(ex.Message);
            }

        }

        [Test]
        public void Find_Number_When_Input_Is_Greater_Than_Zero_Four_Level_Tree()
        {
            var input =
@"1
8 9
1 5 9
4 5 2 3";

            var result = finder.Search(input);
            
            Assert.Greater(result.MaxSum, 0);
            Assert.AreEqual(16, result.MaxSum);
            Console.WriteLine($"Max. number found: {result.MaxSum}");
            Console.WriteLine($"Path: {result.Path}");
            Console.WriteLine(input);
        }

        [Test]
        public void Find_Number_When_Input_Contains_Positve_Negative_And_Zero_Values()
        {
            var input =
@"1
0 0
1 -5 0
-14 5 2 3";

            var result = finder.Search(input);

            Assert.AreEqual(-2, result.MaxSum);
            Console.WriteLine($"Max. number found: {result.MaxSum}");
            Console.WriteLine($"Path: {result.Path}");
            Console.WriteLine(input);
        }

        [Test]
        public void Find_Number_When_Input_Is_Greater_Than_Zero_Five_Level_Tree()
        {
            var input =
@"53
99 -47
95 30 91
77 71 86 67
97 13 35 89 45";

            var result = finder.Search(input);

            Assert.Greater(result.MaxSum, 0);
            Assert.AreEqual(53, result.MaxSum);
            Console.WriteLine($"Max. number found: {result.MaxSum}");
            Console.WriteLine($"Path: {result.Path}");
            Console.WriteLine(input);
        }

        [Test]
        public void Find_Number_When_Input_Is_Greater_Than_Zero_Sixth_Level_Tree()
        {
            var input =
@"215
192 124
117 269 442
218 836 347 235
320 805 522 417 345
229 601 728 835 133 124";

            var result = finder.Search(input);

            Assert.Greater(result.MaxSum, 0);
            Assert.AreEqual(3045, result.MaxSum);
            Console.WriteLine($"Max. number found: {result.MaxSum}");
            Console.WriteLine($"Path: {result.Path}");
            Console.WriteLine(input);
        }

        [Test]
        public void Find_Number_When_Input_Contains_Negative_Elements()
        {
            var input =
@"-1
-7 -6
-7 -5 -9
-4 -5 -2 -3";

            var result = finder.Search(input);

            Assert.Greater(0, result.MaxSum);
            Assert.AreEqual(-14, result.MaxSum);
            Console.WriteLine($"Max. number found: {result.MaxSum}");
            Console.WriteLine($"Path: {result.Path}");
            Console.WriteLine(input);
        }

        [Test]
        [TestCase(0, Parity.Even)]
        [TestCase(1, Parity.Odd)]
        [TestCase(24, Parity.Even)]
        [TestCase(5, Parity.Odd)]
        [TestCase(37, Parity.Odd)]
        [TestCase(-8, Parity.Even)]
        public void Asser_Parity_When_Number_Given_A_Number(int number, Parity expected)
        {
            Assert.AreEqual(expected, ParityHelper.GetParity(number));
        }

        [Test]
        public void Test_Convert_Array_From_Input_String()
        {
            var input =
@"215
192 124
117 269 442
218 836 347 235";

            var array = TriangleHelper.ConvertStringToArrayOfArray(input);

            Assert.IsNotNull(array);
            Assert.Greater(array.Length, 0);
            Assert.AreEqual(4, array.Length);

            Console.WriteLine($"Input: \n\r{input}");
        }
    }
}