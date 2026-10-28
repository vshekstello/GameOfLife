namespace GameOfLife;

// One game: its id, its grid and how many generations it has been through
public class Game
{
    // A game counts as still after this many generations in a row with no changes
    public const int StillAfter = 3;

    public int Id;
    public Grid Grid;
    public int Generation;
    public int LivingCells;
    public int UnchangedFor;

    public Game(int id, Grid grid, int generation)
    {
        Id = id;
        Grid = grid;
        Generation = generation;
        LivingCells = CountLivingCells();
        UnchangedFor = 0;
    }

    // Still = alive, but nothing has changed for the last few generations
    public bool IsStill
    {
        get
        {
            if (LivingCells == 0) return false;
            if (UnchangedFor >= StillAfter) return true;
            return false;
        }
    }

    // Moves the game on by one generation (a game with no living cells stays as it is)
    public void Step()
    {
        if (LivingCells == 0) return;

        Grid before = Grid;
        Grid = new NextGeneration(Grid).Grid;
        Generation++;
        LivingCells = CountLivingCells();

        if (SameCells(before, Grid)) UnchangedFor++;
        else UnchangedFor = 0;
    }

    int CountLivingCells()
    {
        int count = 0;
        for (int y = 0; y < Grid.Height; y++)
            for (int x = 0; x < Grid.Width; x++)
                if (new CellCheck(Grid, x, y).IsFull) count++;
        return count;
    }

    bool SameCells(Grid a, Grid b)
    {
        for (int y = 0; y < a.Height; y++)
            for (int x = 0; x < a.Width; x++)
                if (a.Cells[y, x] != b.Cells[y, x]) return false;
        return true;
    }
}