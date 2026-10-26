using System;

namespace GameOfLife;

static class Menu
{
    // What AskSave returns when the user does not want to continue a save
    public const int NewGame = -1;

    // Asks "continue y/n". On y it lists the saves and asks which one.
    // Returns the id of the chosen save, or NewGame.
    public static int AskSave(SaveFinder finder)
    {
        Console.WriteLine("=== Game of Life ===");

        // Step 1: continue a saved game at all? (n starts a new game, it does not close the app)
        while (true)
        {
            Console.Write("Saved games found. Continue one? (y/n): ");
            string answer = (Console.ReadLine() ?? "").Trim().ToLower();

            if (answer == "n") return NewGame;
            if (answer == "y") break;

            Console.WriteLine("Please type y or n.");
        }

        // Step 2: which one?
        Console.WriteLine();
        Console.WriteLine("Saved games:");

        foreach (SaveFile save in finder.Saves)
        {
            string games = "unreadable";
            if (save.GameCount >= 0) games = $"{save.GameCount} games ({save.LiveGames} live games)";

            Console.WriteLine($"  {save.Id}) {save.FileName} - {games} - saved {save.Saved:yyyy-MM-dd HH:mm}");
        }

        Console.WriteLine();

        while (true)
        {
            Console.Write("Type a save number to continue it (or N for a new game): ");
            string input = (Console.ReadLine() ?? "").Trim().ToLower();

            if (input == "n") return NewGame;
            if (int.TryParse(input, out int id) && finder.Find(id) != null) return id;

            Console.WriteLine("That is not one of the saves listed.");
        }
    }

    public static int AskGameCount()
    {
        Console.WriteLine("=== Game of Life ===");

        // Keep asking until the number is allowed (Enter alone uses the default)
        while (true)
        {
            Console.Write($"How many games to start with (1-{Simulation.MaxGames}, default 100): ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input)) return 100;

            if (int.TryParse(input, out int count) && count >= 1 && count <= Simulation.MaxGames)
                return count;

            Console.WriteLine($"Please enter a number between 1 and {Simulation.MaxGames}.");
        }
    }
}