using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Data
{
    public static class DataWriter
    {
        private static string PersistentDataPath => Application.persistentDataPath + "GameData.json";

        public static bool Write(int deaths, float timeRemaining, int level)
        {
            try
            {
                var levelData = new LevelData
                {
                    Deaths = deaths,
                    TimeInSeconds = timeRemaining,
                    Level = level
                };
                
                var existingData = Read();
                existingData.Add(levelData);

                var newData = new Data { LevelData = existingData };

                var json = JsonUtility.ToJson(newData);
                File.WriteAllText(PersistentDataPath, json);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<LevelData> Read()
        {
            try
            {
                var fileContents = File.ReadAllText(PersistentDataPath);
                var levelData = JsonUtility.FromJson<Data>(fileContents);
                return levelData.LevelData;
            }
            catch (Exception)
            {
                return new List<LevelData>();
            }
        }

        public static void Clear()
        {
            File.WriteAllText(PersistentDataPath, "[]");
        }
    }
}