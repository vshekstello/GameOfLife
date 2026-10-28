namespace GameOfLife;

// The status texts shown on screen for a game
static class GameStatus
{
    public const string Active = "";                  // a running game shows no status text
    public const string Dead = "dead";                  // Label for a game that contains no alive cells
    public const string Still = "still (unmoving)";     // Label for a game that hasn't had any updates to its cells in a few generations
    public const string EmptySlot = "empty";          // label for a slot with no game in it
}