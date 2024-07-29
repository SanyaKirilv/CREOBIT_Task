using System.IO;
using UnityEngine;

namespace Launcher
{
    [System.Serializable]
    public class GameData
    {
        [Header("Game data")]
        public string Name;
        public string Id;
        [Header("Asset data")]
        public bool IsLoaded;
        
        public bool IsDowloaded => File.Exists(FilePath);
        public string FilePath => Path.Combine(Application.persistentDataPath, FileName);
        public string FileName => $"{Name}";
    }
}