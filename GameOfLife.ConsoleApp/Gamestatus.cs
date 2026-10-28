namespace GameOfLife;

// The status texts shown on screen for a game
static class GameStatus
{
    public const string Active = "";                  // a running game shows no status text
    public const string Dead = "dead";
    public const string Still = "still (unmoving)";
    public const string EmptySlot = "empty";          // label for a slot with no game in it
}