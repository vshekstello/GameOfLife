namespace GameOfLife;

// One game (or an empty slot) as it appears on screen
class GameBlock
{
    Game? game;
    int slot;
    int gridWidth;
    int gridHeight;

    // The constructor only stores what the block needs
    public GameBlock(Game? game, int slot, int gridWidth, int gridHeight)
    {
        this.game = game;
        this.slot = slot;
        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;
    }

    // Builds the text lines: a title, a status line, a border, the grid and a border
    public string[] BuildLines()
    {
        int blockWidth = gridWidth * 2 + 2;
        string border = new string('-', blockWidth);
        var lines = new string[gridHeight + 4];

        // An empty slot is just blank space with a label
        if (game == null)
        {
            lines[0] = $"[{slot + 1}] {GameStatus.EmptySlot}".PadRight(blockWidth).Substring(0, blockWidth);
            for (int i = 1; i < lines.Length; i++)
                lines[i] = new string(' ', blockWidth);
            return lines;
        }

        lines[0] = $"[{slot + 1}] #{game.Id} gen {game.Generation}".PadRight(blockWidth).Substring(0, blockWidth);

        // Status line: says if the game is dead or still (otherwise it shows the active status, which is blank)
        string status = GameStatus.Active;
        if (game.LivingCells == 0) status = GameStatus.Dead;
        else if (game.IsStill) status = GameStatus.Still;
        lines[1] = status.PadRight(blockWidth);

        lines[2] = border;

        for (int y = 0; y < gridHeight; y++)
        {
            string row = "";
            for (int x = 0; x < gridWidth; x++)
            {
                if (new CellCheck(game.Grid, x, y).IsFull) row += "##";
                else row += "  ";
            }
            lines[y + 3] = "|" + row + "|";
        }

        lines[gridHeight + 3] = border;
        return lines;
    }
}