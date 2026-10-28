using System;

namespace GameOfLife;

// draws the grid
class GridDrawer
{
    public GridDrawer(Grid grid, int generation)
    {
        Console.WriteLine($"Generation: {generation}");

        string border = new string('-', grid.Width * 2);
        Console.WriteLine(border);

        int living = 0;
        int dead = 0;

        for (int y = 0; y < grid.Height; y++)
        {
            var row = new string[grid.Width];
            for (int x = 0; x < grid.Width; x++)
            {
                if (new CellCheck(grid, x, y).IsFull)
                {
                    row[x] = "##";
                    living++;
                }
                else
                {
                    row[x] = "  ";
                    dead++;
                }
            }
            Console.WriteLine("|" + string.Concat(row) + "|");
        }
        Console.WriteLine(border); // Additional instructions
        Console.WriteLine($"Living/dead cells: {living}/{dead}");
        Console.WriteLine("Ctrl + C to quit");
    }
}