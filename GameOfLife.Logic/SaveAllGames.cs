using System.IO;
using System.Text;

namespace GameOfLife;

// Writes every game to one text file
public class SaveAllGames
{
    public SaveAllGames(Simulation simulation, SaveFile file)
    {
        var text = new StringBuilder();

        // First line: grid width, grid height, number of games
        text.AppendLine($"{simulation.GridWidth} {simulation.GridHeight} {simulation.Games.Count}");

        foreach (Game game in simulation.Games)
        {
            // Per game: a line with id and generation, then one line per row (1 = full, 0 = empty)
            text.AppendLine($"{game.Id} {game.Generation}");

            for (int y = 0; y < game.Grid.Height; y++)
            {
                for (int x = 0; x < game.Grid.Width; x++)
                {
                    if (new CellCheck(game.Grid, x, y).IsFull) text.Append('1');
                    else text.Append('0');
                }
                text.AppendLine();
            }
        }

        File.WriteAllText(file.FullPath, text.ToString());
    }
}