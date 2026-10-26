using System;
using System.Diagnostics;
using System.Threading;
using GameOfLife;

// Every game uses this size. Bigger grids need a wider terminal.
const int GridWidth = 12;
const int GridHeight = 8;

// Saves live next to the exe, one file per run: savegame_1.txt, savegame_2.txt, ...
string saveFolder = AppContext.BaseDirectory;
var finder = new SaveFinder(saveFolder);

Simulation? simulation = null;

while (simulation == null)
{
    // With no saves there is nothing to choose, so it goes straight to a new game
    int choice = Menu.NewGame;
    if (finder.Saves.Count > 0) choice = Menu.AskSave(finder);

    if (choice == Menu.NewGame)
    {
        // A new run gets the next free id and does not touch any existing save
        int count = Menu.AskGameCount();
        simulation = new Simulation(GridWidth, GridHeight, finder.NextId);
        simulation.AddRandomGames(count);
    }
    else
    {
        SaveFile? save = finder.Find(choice);
        if (save == null) continue;

        var load = new LoadAllGames(save);
        if (load.Found) simulation = load.Simulation;
        else Console.WriteLine("That save file could not be read. Pick another one.");
    }
}

var slots = new int[8]; // the game id shown in each of the 8 slots
var controls = new Controls(simulation, slots, saveFolder);

Console.Clear();

while (!controls.Quit)
{
    var timer = Stopwatch.StartNew();

    Console.SetCursorPosition(0, 0);
    new GamesDrawer(simulation, slots, controls.Message);

    while (Console.KeyAvailable)
        controls.Handle(Console.ReadKey(true));

    simulation.Step();

    // Wait out whatever is left of the 100 ms
    int wait = 100 - (int)timer.ElapsedMilliseconds;
    if (wait > 0) Thread.Sleep(wait);
}