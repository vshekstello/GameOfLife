using System;

// Visually draws the grid
class GridDrawer
{
    public GridDrawer(Grid grid, int generation)
    {
        Console.WriteLine($"Generation: {generation}");

        string border = new string('-', grid.Width * 2);
        Console.WriteLine(border);

        for (int y = 0; y < grid.Height; y++)
        {
            var row = new string[grid.Width];
            for (int x = 0; x < grid.Width; x++)
            {
                if (new CellCheck(grid, x, y).IsFull) row[x] = "⬜";
                else row[x] = "  ";
            }
            Console.WriteLine("|" + string.Concat(row) + "|");
        }

        Console.WriteLine(border);
    }
}
