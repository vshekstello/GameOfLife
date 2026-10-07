using System;
using System.Threading;

int refresh = 200; // <--- Change the refresh rate

int generation = 0;
var (width, height) = Menu.AskSize();

// Creates a random starting grid, with roughly one in four cells alive
var rng = new Random();
var grid = new bool[height, width];
for (int y = 0; y < height; y++)
    for (int x = 0; x < width; x++)
        if (rng.Next(4) == 0)
            grid[y, x] = true;
Console.Clear();

// Generates the grid, adds the generation counter  
while (true)
{
    Draw(grid, generation);
    grid = Step(grid);
    generation++;
    Thread.Sleep(refresh);
}

// Returns true if the given cell is within the grid bounds and currently alive.
bool IsFull(bool[,] g, int x, int y)
{
    if (x < 0 || x >= width) return false;
    if (y < 0 || y >= height) return false;
    if (g[y, x]) return true;
    return false;
}

// Checks all surrounding cells (alive/dead) and counts them.
int CountFullNeighbors(bool[,] g, int x, int y)
{
    int count = 0;
    for (int dy = -1; dy <= 1; dy++)
    {
        for (int dx = -1; dx <= 1; dx++)
        {
            if (dx == 0 && dy == 0) continue; 
            if (IsFull(g, x + dx, y + dy)) count++;
        }
    }
    return count;
}

// Advances the game state by one generation, decides which cells die/survive
bool[,] Step(bool[,] g)
{
    var next = new bool[height, width];

    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            int n = CountFullNeighbors(g, x, y);

            if (IsFull(g, x, y))
            {
                if (n < 2) next[y, x] = false;                // dies - too lonely
                if (n > 3) next[y, x] = false;                // dies - overcrowded
                if (n == 2 || n == 3) next[y, x] = true;      // survives
            }
            else
            {
                if (n == 3) next[y, x] = true;                // new cell is born
            }
        }
    }

    return next;
}

// Dictates how dead and alive cells are visual displayed, displays them in console
void Draw(bool[,] g, int generation)
{
    Console.SetCursorPosition(0, 0);
    Console.WriteLine($"Generation: {generation}");
    Console.WriteLine($"┌{new string('─', width * 2)}┐");

    for (int y = 0; y < height; y++)
    {
        var row = new string[width];
        for (int x = 0; x < width; x++)
        {
            if (IsFull(g, x, y)) row[x] = "⬜";
            else row[x] = "  ";
        }

        Console.WriteLine($"│{string.Concat(row)}│");
    }

    Console.WriteLine($"└{new string('─', width * 2)}┘");
}