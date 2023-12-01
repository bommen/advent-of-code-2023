using System.Text.RegularExpressions;

// Setup
const string inputFilePath = "input.txt";
var fileLines = File.ReadAllLines(inputFilePath);

// Part 1
var partOne = fileLines.Aggregate(0, (acc, line) =>
{
    var numbers = Regex.Matches(line, @"(\d)").AsEnumerable()
        .Aggregate(string.Empty, (numbers, match) => numbers + match.Groups[1].Value);
    return acc + int.Parse($"{numbers.First()}{numbers.Last()}");
});
Console.WriteLine($"Part 1: {partOne}");

// Part 2
var partTwo = fileLines.Aggregate(0, (acc, line) =>
{
    var numbers = Regex.Matches(line, @"(?=(\d|one|two|three|four|five|six|seven|eight|nine))").AsEnumerable()
        .Aggregate(string.Empty, (numbers, match) =>
        {
            var value = match.Groups[1].Value;
            return numbers + value switch
            {
                "one" => 1,
                "two" => 2,
                "three" => 3,
                "four" => 4,
                "five" => 5,
                "six" => 6,
                "seven" => 7,
                "eight" => 8,
                "nine" => 9,
                _ => value
            };
        });
    return acc + int.Parse($"{numbers.First()}{numbers.Last()}");
});
Console.WriteLine($"Part 2: {partTwo}");