using System;

// Holds the size and the cells
class Grid
{
    public int Width;
    public int Height;
    public bool[,] Cells;

    // Creates the empty grid
    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
        Cells = new bool[height, width];
    }
}

// Creates a grid filled with a randomized assortment of cells
class RandomGrid
{
    public Grid Grid;

    public RandomGrid(int width, int height)
    {
        Grid = new Grid(width, height);
        var rng = new Random();
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                if (rng.Next(4) == 0)
                    Grid.Cells[y, x] = true;
    }
}

// Checks if the cell is alive or dead, ignores everything outside of the grid
class CellCheck
{
    public bool IsFull;

    public CellCheck(Grid grid, int x, int y)
    {
        IsFull = false;
        if (x < 0 || x >= grid.Width) return;
        if (y < 0 || y >= grid.Height) return;
        if (grid.Cells[y, x]) IsFull = true;
    }
}

// Counts total neighbors
class NeighborCount
{
    public int Count;

    public NeighborCount(Grid grid, int x, int y)
    {
        Count = 0;
        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                if (dx == 0 && dy == 0) continue;
                if (new CellCheck(grid, x + dx, y + dy).IsFull) Count++;
            }
        }
    }
}

// Does the logic for the next grid
class NextGeneration
{
    public Grid Grid;

    public NextGeneration(Grid current)
    {
        Grid = new Grid(current.Width, current.Height);

        for (int y = 0; y < current.Height; y++)
        {
            for (int x = 0; x < current.Width; x++)
            {
                int n = new NeighborCount(current, x, y).Count;

                if (new CellCheck(current, x, y).IsFull)
                {
                    if (n < 2) Grid.Cells[y, x] = false;            // dies - too lonely
                    if (n > 3) Grid.Cells[y, x] = false;            // dies - overcrowded
                    if (n == 2 || n == 3) Grid.Cells[y, x] = true;  // survives
                }
                else
                {
                    if (n == 3) Grid.Cells[y, x] = true;            // a cell is born
                }
            }
        }
    }
}

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