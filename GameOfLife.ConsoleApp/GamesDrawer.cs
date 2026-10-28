using System;
using System.Text;

namespace GameOfLife;

// Draws the games in the slots (4 across), then the statistics and the message line
class GamesDrawer
{
    const int PerRow = 4;
    const int FooterWidth = 100;

    Simulation simulation;
    int[] slots;

    // The constructor only stores what the drawer needs
    public GamesDrawer(Simulation simulation, int[] slots)
    {
        this.simulation = simulation;
        this.slots = slots;
    }

    // Draws the whole screen once: the games, the statistics and the message line
    public void Draw(string message)
    {
        var frame = new StringBuilder();

        for (int rowStart = 0; rowStart < slots.Length; rowStart += PerRow)
        {
            // Build the lines of the blocks that sit next to each other in this row
            var blocks = new string[PerRow][];
            for (int i = 0; i < PerRow; i++)
            {
                int slot = rowStart + i;
                Game? game = simulation.FindGame(slots[slot]);
                var block = new GameBlock(game, slot, simulation.GridWidth, simulation.GridHeight);
                blocks[i] = block.BuildLines();
            }

            // Print them line by line so they line up side by side
            for (int line = 0; line < blocks[0].Length; line++)
            {
                for (int i = 0; i < PerRow; i++)
                    frame.Append(blocks[i][line]).Append(' ');
                frame.AppendLine();
            }
        }

        frame.AppendLine($"Game #{simulation.SessionId} | Live games: {simulation.LiveGames}/{simulation.Games.Count} | Still games: {simulation.StillGames} | Living cells: {simulation.TotalLivingCells:N0}".PadRight(FooterWidth));
        frame.AppendLine($"{ControlKeys.FirstSlot}-{ControlKeys.LastSlot} = pick slot | {ControlKeys.AddGames} = add games | {ControlKeys.DeleteGame} = delete game | {ControlKeys.Save} = save | {ControlKeys.Quit} = quit".PadRight(FooterWidth));
        frame.AppendLine(message.PadRight(FooterWidth));

        // One single write, so the screen doesn't flicker
        Console.Write(frame.ToString());
    }
}