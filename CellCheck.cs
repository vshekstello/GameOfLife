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
