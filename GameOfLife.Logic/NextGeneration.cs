namespace GameOfLife;

public class NextGeneration
{
    public Grid Grid;

    int MinNeighbors = 2;
    int MaxNeighbors = 3;
    int NeighborsBorn = 3;

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
                    if (n < MinNeighbors) Grid.Cells[y, x] = false;   // dies - too lonely
                    if (n > MaxNeighbors) Grid.Cells[y, x] = false;   // dies - overcrowded
                    if (n >= MinNeighbors && n <= MaxNeighbors) Grid.Cells[y, x] = true;  // survives
                }
                else
                {
                    if (n == NeighborsBorn) Grid.Cells[y, x] = true;       // a cell is born
                }
            }
        }
    }
}