using System;

static class Menu
{
    public static (int width, int height) AskSize()
    {
        Console.WriteLine("--- Game of Life ---");
        Console.WriteLine("\nWidth: ");
        int width = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException("No width was entered."));
        Console.WriteLine("Height: ");
        int height = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException("No height was entered."));
        return (width, height);
    }
}