using System.Collections.Generic;
using System.IO;

namespace GameOfLife;

// Looks in a folder for save files and works out the id for the next new game
public class SaveFinder
{
    public List<SaveFile> Saves = new List<SaveFile>();
    public int NextId;

    public SaveFinder(string folder)
    {
        NextId = 1;

        foreach (string path in Directory.GetFiles(folder, SaveFile.Prefix + "*" + SaveFile.Extension))
        {
            // "savegame_3" -> "3"
            string name = Path.GetFileNameWithoutExtension(path);
            string idText = name.Substring(SaveFile.Prefix.Length);
            if (!int.TryParse(idText, out int id)) continue;

            var save = new SaveFile(folder, id);
            save.FileName = Path.GetFileName(path);
            save.FullPath = path;
            save.Saved = File.GetLastWriteTime(path);

            // Open the save to count its games and how many of them are live
            var load = new LoadAllGames(save);
            if (load.Found)
            {
                save.GameCount = load.Simulation.Games.Count;
                save.LiveGames = load.Simulation.LiveGames;
            }
            else
            {
                save.GameCount = -1;
                save.LiveGames = -1;
            }

            Saves.Add(save);

            if (id >= NextId) NextId = id + 1;
        }

        Saves.Sort((a, b) => a.Id.CompareTo(b.Id));
    }

    public SaveFile? Find(int id)
    {
        foreach (SaveFile save in Saves)
            if (save.Id == id) return save;
        return null;
    }
}