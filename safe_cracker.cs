using System;
using System.Collections.Generic;

class SafeCrackerGame
{
    private readonly List<int> safeNumbers;
    private readonly List<int> multipliers;
    private readonly Dictionary<int, int> grid;
    private int betAmount;

    public SafeCrackerGame()
    {
        safeNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        multipliers = new List<int> { 15, 16, 17, 18, 19, 20 };
        grid = new Dictionary<int, int>();

        InitializeGrid();
    }

    private void InitializeGrid()
    {
        foreach (var number in safeNumbers)
        {
            grid[number] = 0; // 0 represents an unrevealed safe
        }
    }

    private void DisplayGrid()
    {
        Console.WriteLine(" -------------------------------------");
        Console.WriteLine($" |{grid[1]}|{grid[2]}|{grid[3]}| ");
        Console.WriteLine(" -------------------------------------");
        Console.WriteLine($" |{grid[4]}|{grid[5]}|{grid[6]}| ");
        Console.WriteLine(" -------------------------------------");
        Console.WriteLine($" |{grid[7]}|{grid[8]}|{grid[9]}| ");
        Console.WriteLine(" -------------------------------------");
        Console.WriteLine("Press [SPACE] to spin");
    }

    private int SpinSafe()
    {
        Random random = new Random();
        int index = random.Next(safeNumbers.Count);
        int safeNumber = safeNumbers[index];
        safeNumbers.RemoveAt(index);
        return safeNumber;
    }

    private void RevealMultiplier(int safeNumber)
    {
        Random random = new Random();
        int multiplierIndex = random.Next(multipliers.Count);
        int multiplier = multipliers[multiplierIndex];
        multipliers.RemoveAt(multiplierIndex);
        grid[safeNumber] = multiplier;
    }

    public void PlayGame()
    {
        Console.WriteLine("Welcome to Safe Cracker!");
        Console.WriteLine("How much would you like to bet?");
        betAmount = int.Parse(Console.ReadLine());

        int spinCount = 0;
        while (spinCount < 4)
        {
            Console.Clear();
            DisplayGrid();

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Spacebar)
            {
                spinCount++;
                int safeNumber = SpinSafe();
                Console.WriteLine($"\nSpun safe number: {safeNumber}");
                RevealMultiplier(safeNumber);

                Console.Clear();
                DisplayGrid();
            }
        }

        Console.WriteLine("\nCongratulations! Game complete.");
        int matchedMultiplier = GetMatchedMultiplier();
        int winAmount = betAmount * matchedMultiplier;
        Console.WriteLine($"You have won {winAmount} (x{matchedMultiplier} your initial bet amount).");
    }

    private int GetMatchedMultiplier()
    {
        List<int> revealedMultipliers = new List<int>();
        foreach (var multiplier in grid.Values)
        {
            if (multiplier != 0 && !revealedMultipliers.Contains(multiplier))
            {
                revealedMultipliers.Add(multiplier);
            }
        }

        return revealedMultipliers.Count >= 2 ? revealedMultipliers[1] : 0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        SafeCrackerGame game = new SafeCrackerGame();
        game.PlayGame();
    }
}
