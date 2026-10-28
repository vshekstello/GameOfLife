using System;

namespace GameOfLife;

enum InputMode { None, PickGame, AddGames, DeleteGame }

// Reads the keys the user presses and changes the games, the 8 slots and the message line
class Controls
{
    public bool Quit;
    public string Message;

    Simulation simulation;
    int[] slots;
    string saveFolder;
    InputMode mode;
    string prompt;
    string typed;
    int pickedSlot;

    public Controls(Simulation simulation, int[] slots, string saveFolder)
    {
        this.simulation = simulation;
        this.slots = slots;
        this.saveFolder = saveFolder;

        Quit = false;
        Message = "";
        mode = InputMode.None;
        prompt = "";
        typed = "";
        pickedSlot = 0;

        // Start by showing the first games that exist
        for (int i = 0; i < slots.Length; i++) slots[i] = -1;
        FillEmptySlots();
    }

    // Call this once for every key the user presses
    public void Handle(ConsoleKeyInfo key)
    {
        if (mode == InputMode.None) StartCommand(key);
        else TypeNumber(key);
    }

    void StartCommand(ConsoleKeyInfo key)
    {
        char c = char.ToUpper(key.KeyChar);

        switch (c)
        {
            case >= ControlKeys.FirstSlot and <= ControlKeys.LastSlot:
                pickedSlot = c - ControlKeys.FirstSlot;
                Ask(InputMode.PickGame, $"Show which game id in slot {pickedSlot + 1}? ");
                break;

            case ControlKeys.AddGames:
            {
                int room = Simulation.MaxGames - simulation.Games.Count;
                if (room <= 0) Message = $"Cannot add more: the limit is {Simulation.MaxGames} games.";
                else Ask(InputMode.AddGames, $"How many games to add (up to {room})? ");
                break;
            }

            case ControlKeys.DeleteGame:
                Ask(InputMode.DeleteGame, "Which game id to delete? ");
                break;

            case ControlKeys.Save:
            {
                // The file name contains the id of this run, e.g. savegame_3.txt
                var file = new SaveFile(saveFolder, simulation.SessionId);
                new SaveAllGames(simulation, file);
                Message = $"All games saved as {file.FileName}.";
                break;
            }

            case ControlKeys.Quit:
                Quit = true;
                break;
        }
    }

    void Ask(InputMode newMode, string question)
    {
        mode = newMode;
        prompt = question;
        typed = "";
        Message = prompt;
    }

    // Collects the digits of a number until Enter (Esc cancels)
    void TypeNumber(ConsoleKeyInfo key)
    {
        switch (key.Key)
        {
            case ConsoleKey.Escape:
                mode = InputMode.None;
                Message = "Cancelled.";
                return;

            case ConsoleKey.Enter:
                Finish();
                return;

            case ConsoleKey.Backspace:
                if (typed.Length > 0) typed = typed.Substring(0, typed.Length - 1);
                break;

            default:
                if (char.IsDigit(key.KeyChar) && typed.Length < 6) typed += key.KeyChar;
                break;
        }

        Message = prompt + typed;
    }

    // The user pressed Enter: do what they asked with the number they typed
    void Finish()
    {
        InputMode finished = mode;
        mode = InputMode.None;

        if (!int.TryParse(typed, out int number))
        {
            Message = "Please type a number.";
            return;
        }

        switch (finished)
        {
            case InputMode.PickGame:
                if (simulation.FindGame(number) == null)
                {
                    Message = $"There is no game with id {number}.";
                }
                else
                {
                    slots[pickedSlot] = number;
                    Message = $"Slot {pickedSlot + 1} now shows game {number}.";
                }
                break;

            case InputMode.AddGames:
            {
                int added = simulation.AddRandomGames(number);
                FillEmptySlots();

                if (added == number) Message = $"Added {added} games.";
                else Message = $"Added {added} games (limit of {Simulation.MaxGames} reached).";
                break;
            }

            case InputMode.DeleteGame:
                if (simulation.RemoveGame(number))
                {
                    FillEmptySlots();
                    Message = $"Deleted game {number}.";
                }
                else
                {
                    Message = $"There is no game with id {number}.";
                }
                break;
        }
    }

    // Puts a game that isn't on screen yet into every empty slot
    void FillEmptySlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (simulation.FindGame(slots[i]) != null) continue;

            slots[i] = -1;
            foreach (Game game in simulation.Games)
            {
                if (IsShown(game.Id)) continue;
                slots[i] = game.Id;
                break;
            }
        }
    }

    bool IsShown(int id)
    {
        for (int i = 0; i < slots.Length; i++)
            if (slots[i] == id) return true;
        return false;
    }
}