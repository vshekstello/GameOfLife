using System.IO;

namespace GameOfLife;

// Reads all games from a save file. Found is false if the file is missing or damaged.
public class LoadAllGames
{
    public bool Found;
    public Simulation Simulation;

    public LoadAllGames(SaveFile save)
    {
        Found = false;
        Simulation = new Simulation(1, 1, 0); // placeholder, only used when Found is true

        if (!File.Exists(save.FullPath)) return;

        try
        {
            string[] lines = File.ReadAllLines(save.FullPath);
            string[] header = lines[0].Split(' ');
            int width = int.Parse(header[0]);
            int height = int.Parse(header[1]);
            int count = int.Parse(header[2]);

            // The loaded run keeps the id of the save it came from
            var simulation = new Simulation(width, height, save.Id);
            int line = 1;

            for (int i = 0; i < count; i++)
            {
                string[] gameHeader = lines[line].Split(' ');
                int id = int.Parse(gameHeader[0]);
                int generation = int.Parse(gameHeader[1]);
                line++;

                var grid = new Grid(width, height);
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                        if (lines[line][x] == '1')
                            grid.Cells[y, x] = true;
                    line++;
                }

                simulation.AddGame(new Game(id, grid, generation));
            }

            Simulation = simulation;
            Found = true;
        }
        catch
        {
            Found = false; // the file is damaged or incomplete
        }
    }
}