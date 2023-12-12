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
for (var col = 0; col < totalCols; col++)
{
    var currentChar = input[row][col];

    // Check if the current character is a part number
    if (char.IsDigit(currentChar))
    {
        // Find the end of the part number
        var endOfPartNumber = col + 1;
        while (endOfPartNumber < totalCols && char.IsDigit(input[row][endOfPartNumber])) endOfPartNumber++;

        // Parse the part number
        var partNumber = int.Parse(input[row].Substring(col, endOfPartNumber - col));

        // Check all adjacent positions (including diagonals)
        for (var x = row - 1; x <= row + 1; x++)
        for (var y = col - 1; y <= endOfPartNumber; y++)
            if (x >= 0 && x < totalRows && y >= 0 && y < totalCols)
                if (IsSymbol(input[x][y]))
                    totalSum += partNumber;

        // Move the outer loop index to the end of the part number
        col = endOfPartNumber - 1;
    }
}

Console.WriteLine($"Part 1: {totalSum}");

// Part 2
var possibleGears = new List<(int row, int col)>();
for (var row = 0; row < totalRows; row++)
for (var col = 0; col < totalCols; col++)
{
    var currentChar = input[row][col];
    if (currentChar == '*') possibleGears.Add((row, col));
}

var totalRatio = possibleGears.Aggregate(0, (acc, gear) =>
{
    var partNumbers = new List<int>();
    // Check line above
    if (gear.row > 0) partNumbers.AddRange(GetPartNumbersFromLine(input, gear.row -1, gear));
    // Check current line
    partNumbers.AddRange(GetPartNumbersFromLine(input, gear.row, gear));
    // Check line below
    if(gear.row < totalRows)
        partNumbers.AddRange(GetPartNumbersFromLine(input, gear.row + 1, gear));

    // If there are exactly two adjacent part number, we calculate the gear ratio
    var gearRatio = partNumbers.Count == 2 ? partNumbers.Aggregate(1, (ratioAcc, partNumber) => ratioAcc * partNumber) : 0;
    return acc + gearRatio;
});

Console.WriteLine($"Part 2: {totalRatio}");
return;

IEnumerable<int> GetPartNumbersFromLine(string[] input, int row, (int row, int col) gear)
{
    var partNumbers = new List<int>();
    var matches = Regex.Matches(input[row], @"[\d]{1,}");
    foreach (Match match in matches)
    {
        var startCol = match.Index;
        var endCol = match.Index + match.Value.Length - 1;
        var startsWithinBox = startCol >= gear.col - 1 && startCol <= gear.col + 1;
        var endsWithinBox = endCol >= gear.col - 1 && endCol <= gear.col + 1;
        var overlapsBox = startCol < gear.col && endCol > gear.col;
        // If the partnumber starts in, ends in or overlaps the box around the gear, we consider it a valid part number
        if (startsWithinBox || endsWithinBox || (gear.row != row && overlapsBox))
            partNumbers.Add(int.Parse(match.Value));
    }

    return partNumbers;
}

//
//     ...
//     .*.
//     ...
//
