using System;
using System.Collections.Generic;

namespace Assets.Data
{
    [Serializable]
    public class LevelData
    {
        public int Deaths;
        public int Level;
        public float TimeInSeconds;
    }

    public class Data
    {
        public List<LevelData> LevelData;
    }
}