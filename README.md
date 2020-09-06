# TriangleMaxNumPathCalculator
Console C# .Net Core 3.1 Application to find the path that provides maximum sum of the numbers given a triangle of integers and following the next rules:

1. Start from the top and move downwards to the last possible node.
2. Operations must be performed by changing between even and odd numbers subsequently. 
ie.: Current number is odd, the next number choosen must be even and vice versa.
In other words, the final path would be Odd -> even -> odd -> even ...
3. Reach to the bottom of the pyramid.
4. Assume that there is at least one valid path to the bottom.
5. If there are multiple paths, which result in the same maximum amount, any
of them are valid.
6. Each node has only two children here (except the bottom row).

Sample Input:
1
8 9
1 5 9
4 5 2 3

Output:
Max sum: 16
Path: 1, 8, 5, 2


# How to execute

1. Starting a console application from Visual Studio 2019. Program.cs class contains an input, can be changed any time.
2. Unittest from Visual Studio 2019. The next values has been covered so far:

- All values positives.
- Empty input.
- Only 1 row (first element).
- 2 rows (root element and 2 childs).
- Combination negative, zero, positive values.
- FormatException when input contains invalid character.
- Array conversion given a string input.
- Verify parity (odd and even) given a integer value.


# Approach and strategies used

The rules suggest a top-bottom solution and since the amount of items increase each next step. Maximum possible combinations identified was 2^n where n = max rows on triangle.

Recursion was used in this case to go through each graph considering simplicity and extensibility (input extended to diferents levels like 20 or 50 rows). 
Also, in terms of performance each recursively call will depends on the child's opposite parity (if both values fetch odd=>odd / even=>even) then the recursion is not made.

This approach was selected by dividing the problem in small piece of work. For example, cases:

> 2^n = 2*1 = 2 combinations as maximun

1
8 9

Valid path: 1 => 8

1
8 10

Valid path: 1 => 8 | 1 => 10

1
9 9

Valid path: 1

Even, when having 1,2,3...n additional levels, the result must be the same.

List of interger list was needed to store all valid path. After transversal execution, some calculations are made returning a tuple (maxSum, stringPath) where maxSum is calculated on each item and the maximum of them is taken later including a path in string format.
