// Setup

const string inputFilePath = "input.txt";
var games = File.ReadAllLines(inputFilePath);

// Part 1 & 2
var gameNumber = 1;
var partOne = 0;
var partTwo = 0;
foreach (var game in games)
{
    var maxRed = 0;
    var maxGreen = 0;
    var maxBlue = 0;
    var gameIsPossible = true;
    var rolls = game[(game.IndexOf(':') + 1)..].Split(";");
    foreach (var roll in rolls)
    {
        var red = 0;
        var green = 0;
        var blue = 0;
        foreach (var color in roll.Split(','))
        {
            var dices = color.Trim().Split(" ");
            switch (dices[1])
            {
                case "red":
                    red = int.Parse(dices[0]);
                    maxRed = red > maxRed ? red : maxRed;
                    break;
                case "green":
                    green = int.Parse(dices[0]);
                    maxGreen = green > maxGreen ? green : maxGreen;
                    break;
                case "blue":
                    blue = int.Parse(dices[0]);
                    maxBlue = blue > maxBlue ? blue : maxBlue;
                    break;
            }
        }

        gameIsPossible = gameIsPossible && !(red > 12 || green > 13 || blue > 14);
    }

    var gamePower = maxRed * maxGreen * maxBlue;
    partTwo += gamePower;
    if (gameIsPossible) partOne += gameNumber;
    gameNumber++;
}

Console.WriteLine($"Part 1: {partOne}");
Console.WriteLine($"Part 1: {partTwo}");