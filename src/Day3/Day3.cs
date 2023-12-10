using System.Text.RegularExpressions;

const string inputFilePath = "input.txt";
var input = File.ReadAllLines(inputFilePath);

var totalRows = input.Length;
var totalCols = input[0].Length;

var totalSum = 0;

bool IsSymbol(char c)
{
    // Define the symbols that represent engine parts
    return !char.IsDigit(c) && c != '.';
}

for (var row = 0; row < totalRows; row++)
{
    for (var col = 0; col < totalCols; col++)
    {
        var currentChar = input[row][col];

        // Check if the current character is a part number
        if (char.IsDigit(currentChar))
        {
            // Find the end of the part number
            var endOfPartNumber = col + 1;
            while (endOfPartNumber < totalCols && char.IsDigit(input[row][endOfPartNumber]))
            {
                endOfPartNumber++;
            }

            // Parse the part number
            var partNumber = int.Parse(input[row].Substring(col, endOfPartNumber - col));

            // Check all adjacent positions (including diagonals)
            for (var x = row - 1; x <= row + 1; x++)
            for (var y = col - 1; y <= endOfPartNumber; y++)
                    if (x >= 0 && x < totalRows && y >= 0 && y < totalCols)
                        if (IsSymbol(input[x][y])) totalSum += partNumber;

            // Move the outer loop index to the end of the part number
            col = endOfPartNumber - 1;
        }
    }
}

Console.WriteLine($"Part 1: {totalSum}");

// Part 2
