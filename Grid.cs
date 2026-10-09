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
