using System;
using System.IO;

namespace GameOfLife;

// One save file on disk, named savegame_<id>.txt
public class SaveFile
{
    public const string Prefix = "savegame_";
    public const string Extension = ".txt";

    public int Id;
    public string FileName;
    public string FullPath;
    public int GameCount;
    public int LiveGames;   // games with at least one living cell, -1 if the file could not be read
    public DateTime Saved;

    public SaveFile(string folder, int id)
    {
        Id = id;
        FileName = Prefix + id + Extension;
        FullPath = Path.Combine(folder, FileName);
        GameCount = 0;
        LiveGames = 0;
        Saved = DateTime.MinValue;
    }
}