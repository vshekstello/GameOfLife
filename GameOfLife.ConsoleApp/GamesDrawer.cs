using System;
using System.Text;

namespace GameOfLife;

// Draws the games in the slots (4 across), then the statistics and the message line
class GamesDrawer
{
    const int PerRow = 4;
    const int FooterWidth = 100;

    public GamesDrawer(Simulation simulation, int[] slots, string message)
    {
        var frame = new StringBuilder();

        for (int rowStart = 0; rowStart < slots.Length; rowStart += PerRow)
        {
            // Build the blocks that sit next to each other in this row
            var blocks = new GameBlock[PerRow];
            for (int i = 0; i < PerRow; i++)
            {
                int slot = rowStart + i;
                Game? game = simulation.FindGame(slots[slot]);
                blocks[i] = new GameBlock(game, slot, simulation.GridWidth, simulation.GridHeight);
            }

            // Print them line by line so they line up side by side
            for (int line = 0; line < blocks[0].Lines.Length; line++)
            {
                for (int i = 0; i < PerRow; i++)
                    frame.Append(blocks[i].Lines[line]).Append(' ');
                frame.AppendLine();
            }
        }

        frame.AppendLine($"Game #{simulation.SessionId} | Live games: {simulation.LiveGames}/{simulation.Games.Count} | Still games: {simulation.StillGames} | Living cells: {simulation.TotalLivingCells:N0}".PadRight(FooterWidth));
        frame.AppendLine("1-8 = pick slot | A = add games | D = delete game | S = save | Q = quit".PadRight(FooterWidth));
        frame.AppendLine(message.PadRight(FooterWidth));

        // One single write, so the screen doesn't flicker
        Console.Write(frame.ToString());
    }
}