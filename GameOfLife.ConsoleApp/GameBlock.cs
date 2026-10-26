namespace GameOfLife;

// The text lines for one game on screen: a title, a status line, a border, the grid and a border
class GameBlock
{
    public string[] Lines;

    public GameBlock(Game? game, int slot, int gridWidth, int gridHeight)
    {
        int blockWidth = gridWidth * 2 + 2;
        string border = new string('-', blockWidth);
        Lines = new string[gridHeight + 4];

        // An empty slot is just blank space with a label
        if (game == null)
        {
            Lines[0] = $"[{slot + 1}] empty".PadRight(blockWidth).Substring(0, blockWidth);
            for (int i = 1; i < Lines.Length; i++)
                Lines[i] = new string(' ', blockWidth);
            return;
        }

        Lines[0] = $"[{slot + 1}] #{game.Id} gen {game.Generation}".PadRight(blockWidth).Substring(0, blockWidth);

        // Status line: says if the game is dead or still (otherwise it stays blank)
        string status = "";
        if (game.LivingCells == 0) status = "dead";
        else if (game.IsStill) status = "still (unmoving)";
        Lines[1] = status.PadRight(blockWidth);

        Lines[2] = border;

        for (int y = 0; y < gridHeight; y++)
        {
            string row = "";
            for (int x = 0; x < gridWidth; x++)
            {
                if (new CellCheck(game.Grid, x, y).IsFull) row += "##";
                else row += "  ";
            }
            Lines[y + 3] = "|" + row + "|";
        }

        Lines[gridHeight + 3] = border;
    }
}