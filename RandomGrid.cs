using System;

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
