using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameOfLife;

// All the games that are running, plus the totals across them
public class Simulation
{
    // The most games that can exist at the same time
    public const int MaxGames = 1000;

    public int SessionId;   // the simple id of this run, used in the save file name
    public List<Game> Games = new List<Game>();
    public int GridWidth;
    public int GridHeight;
    int nextId = 0;

    public Simulation(int gridWidth, int gridHeight, int sessionId)
    {
        GridWidth = gridWidth;
        GridHeight = gridHeight;
        SessionId = sessionId;
    }

    // Adds new games filled with random cells, up to the limit.
    // Returns how many games were really added.
    public int AddRandomGames(int count)
    {
        int added = 0;

        for (int i = 0; i < count; i++)
        {
            if (Games.Count >= MaxGames) break;

            Grid grid = new RandomGrid(GridWidth, GridHeight).Grid;
            AddGame(new Game(nextId, grid, 0));
            added++;
        }

        return added;
    }

    // Adds an existing game (also used when loading a save)
    public void AddGame(Game game)
    {
        Games.Add(game);
        if (game.Id >= nextId) nextId = game.Id + 1;
    }

    // Removes a game by id. Returns false if there is no such game.
    public bool RemoveGame(int id)
    {
        Game? game = FindGame(id);
        if (game == null) return false;

        Games.Remove(game);
        return true;
    }

    public Game? FindGame(int id)
    {
        foreach (Game game in Games)
            if (game.Id == id) return game;
        return null;
    }

    // Moves every game on by one generation, all at the same time
    public void Step()
    {
        Parallel.ForEach(Games, game => game.Step());
    }

    // Games that still have at least one living cell
    public int LiveGames
    {
        get
        {
            int count = 0;
            foreach (Game game in Games)
                if (game.LivingCells > 0) count++;
            return count;
        }
    }

    // Live games where nothing has changed for the last few generations
    public int StillGames
    {
        get
        {
            int count = 0;
            foreach (Game game in Games)
                if (game.IsStill) count++;
            return count;
        }
    }

    public int TotalLivingCells
    {
        get
        {
            int total = 0;
            foreach (Game game in Games)
                total += game.LivingCells;
            return total;
        }
    }
}