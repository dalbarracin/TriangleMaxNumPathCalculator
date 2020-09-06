using System;
using System.Collections.Generic;
using System.Linq;

namespace TrianglePathCalculator.Helpers
{
    public static class TriangleHelper
    {
        public static int[][] ConvertStringToArrayOfArray(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new int[][] { };

            var stringArray = input.Split('\n');

            var list = new List<int[]>();
            List<int> rowTemp;

            for (int i = 0; i < stringArray.Length; i++)
            {
                var row = stringArray[i].Trim().Split(' ');
                rowTemp = new List<int>();

                for (int j = 0; j < row.Length; j++)
                {
                    if (!int.TryParse(row[j], out var number))
                        throw new FormatException($"Element {row[j]} Pos. [{i},{j}] is not a valid number.");
                    
                    rowTemp.Add(number);
                }

                list.Add(rowTemp.ToArray());
            }

            return list.ToArray();
        }

        public static string ConvertPathIntoLegibleOutput(List<int?> items)
            => string.Join(" => ", items.Where(r => r.HasValue));
    }
}
