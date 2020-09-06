using System;
using System.Collections.Generic;
using System.Linq;
using TrianglePathCalculator.Helpers;

namespace TrianglePathCalculator
{
    public class MaximumNumberPathFinder
    {
        public int?[] arrayNum { get; set; }
        private List<List<int?>> _validNumbers { get; set; }

        public MaximumNumberPathFinder()
        {
            _validNumbers = new List<List<int?>>();
        }

        public (int MaxSum, string Path) Search(string input)
        {
            var triangle = TriangleHelper.ConvertStringToArrayOfArray(input);

            if (!triangle.Any())
                return (0, string.Empty);

            arrayNum = new int?[triangle.Length];

            FindValidChilds(triangle);

            var tuples = GetPathNumbersFromCache();

            return (tuples.max, tuples.path);
        }

        /// <summary>
        /// Go through the triangle recursively and find all valid paths from top to
        /// bottom only with subjacents items (down and right bottom childs) when parity
        /// is the opposite of root item (parent) ie. Parent = odd childs must be even.
        /// </summary>
        /// <param name="triangle">Input converted to array</param>
        /// <param name="sum">acumulative value from top to buttom</param>
        /// <param name="row">current level of array</param>
        /// <param name="col">current column of array</param>
        public void FindValidChilds(int[][] triangle, int sum = 0, int row = 0, int col = 0)
        {
            var rootValue = triangle[row][col];
            arrayNum[row] = rootValue;

            ResetArrayFromLevel(row);

            if (row + 1 >= triangle.Length) return;

            if (ParityHelper.AreNotEqual(rootValue, triangle[row + 1][col]))
                FindValidChilds(triangle, rootValue + sum, row + 1, col);

            if (ParityHelper.AreNotEqual(rootValue, triangle[row + 1][col + 1]))
                FindValidChilds(triangle, rootValue + sum, row + 1, col + 1);

            CachePathValidOnArrayOfNumbers();
        }

        private void CachePathValidOnArrayOfNumbers()
        {
            if (!_validNumbers.Any(c => c.SequenceEqual(arrayNum)))
                _validNumbers.Add(new List<int?>(arrayNum));
        }

        private void ResetArrayFromLevel(int row)
        {
            for (int i = row + 1; i < arrayNum.Length; i++)
                arrayNum[i] = null;
        }

        /// <summary>
        /// Cache variable called _validNumbers holds all paths 
        /// found after iteration. This method sumarize all items 
        /// from path and convert a path into string output legible
        /// </summary>
        private (int max, string path) GetPathNumbersFromCache()
        {
            return ((int max, string path))
                    _validNumbers
                    .Select(items => (
                        max: items.Sum(),
                        path: TriangleHelper.ConvertPathIntoLegibleOutput(items)))
                    .OrderByDescending(r => r.max)
                    .FirstOrDefault();
        }
    }
}