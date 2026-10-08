using System;
using System.Text;
using System.Threading;

int refresh = 400; // <---- Set the refresh rate (in ms)

Console.OutputEncoding = Encoding.UTF8;

var (width, height) = Menu.AskSize();

var grid = new RandomGrid(width, height).Grid;
int generation = 0;

Console.Clear();

while (true)
{
    Console.SetCursorPosition(0, 0);
    new GridDrawer(grid, generation);
    grid = new NextGeneration(grid).Grid;
    generation++;
    Thread.Sleep(refresh);
}