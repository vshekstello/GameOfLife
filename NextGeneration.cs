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
