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
