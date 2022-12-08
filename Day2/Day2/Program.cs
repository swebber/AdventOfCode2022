// a,x == rock     == 1
// b,y == paper    == 2
// c,z == scissors == 3

// Game Outcome
// x == lose
// y == draw
// z == win

// lose == 0
// draw == 3
// win  == 6

int totalScore = 0;

foreach (string line in File.ReadLines(@"C:\Users\WebberS\source\repos\AdventOfCode2022\Day2\Day2\data.txt"))
{
    var play = line.Split(" ");

    // switch the move but keep everything else the same
    string originalMove = play[1];
    string newMove = DetermineMove(play);
    play[1] = newMove;

    int score = Value(play[1]);
    int points = Points(play);
    int total = score + points;

    Console.WriteLine($"{score} + {points} = {total}");

    totalScore += total;
}

Console.WriteLine($"Total: {totalScore}");

public static partial class Program
{
    public static string DetermineMove(string[] play)
    {
        switch (play[1])
        {
            case "X": // lose
                switch (play[0])
                {
                    case "A":
                        return "Z";
                    case "B":
                        return "X";
                    case "C":
                        return "Y";
                }
                break;
            case "Y": // draw
                switch (play[0])
                {
                    case "A":
                        return "X";
                    case "B":
                        return "Y";
                    case "C":
                        return "Z";
                }
                break;
            default: // win
                switch (play[0])
                {
                    case "A":
                        return "Y";
                    case "B":
                        return "Z";
                    case "C":
                        return "X";
                }
                break;
        }

        throw new ArgumentOutOfRangeException(nameof(play));
    }

    public static int Points(string[] play)
    {
        string pair = $"{play[1]}{play[0]}";

        return pair switch
        {
            "XA" or "YB" or "ZC" => 3,
            "XC" or "YA" or "ZB" => 6,
            _ => 0
        };
    }

    public static int Value(string move)
    {
        switch (move)
        {
            case "A":
            case "X":
                return 1;
            case "B":
            case "Y":
                return 2;
            case "C":
            case "Z":
                return 3;
        }

        throw new ArgumentOutOfRangeException(nameof(move));
    }
}