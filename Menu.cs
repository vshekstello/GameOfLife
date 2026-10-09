using System;

static class Menu
{
    public static (int width, int height) AskSize()
    {
        Console.WriteLine("=== Game of Life ===");
        int width = Ask("Width: ");
        int height = Ask("Height: ");
        return (width, height);
    }

    private static int Ask(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
                return value;

            Console.WriteLine("Please enter a positive number.");
        }
    }
}