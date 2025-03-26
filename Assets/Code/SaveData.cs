using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    [System.Serializable]
    public class SaveData : MonoBehaviour
    {
        public List<LevelRecord> bestTimes;
        public List<int> stars;

        [System.Serializable]
        public struct LevelRecord
        {
            public LevelRecord(float t, int s)
            {
                time = t;
                stars = s;
            }

            public float time {get;}
            public int stars {get;}
        }

        [System.Serializable]
        public struct LevelInfo
        {
            public LevelInfo(float[] tt, int req, int sup)
            {
                targetTimes = tt;
                starsRequired = req;
                superBombs = sup;
            }

            public float[] targetTimes {get;}
            public int starsRequired {get;}
            public int superBombs {get;}
        }

        // saves the times and stars
        public static void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/records.wtf";

            List<LevelRecord> bestTimes = TimeLists.bestTimes;
            
            using(FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, bestTimes);
            }
        }
        
        // loads the times and stars
        public static void Load()
        {
            string path = Application.persistentDataPath + "/records.wtf";
            BinaryFormatter formatter = new BinaryFormatter();

            if (File.Exists(path))
            {
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    List<LevelRecord> bestTimes = formatter.Deserialize(stream) as List<LevelRecord>;
                    TimeLists.bestTimes = bestTimes;
                }
            }

            // if there are no records, create a new blank list
            // ADJUST TO FINAL LEVEL AMOUNT LATER
            else
            {
                Debug.Log("Save not found, creating");

                List<LevelRecord> blankTimes = new List<LevelRecord>();
                LevelRecord blankRecord = new LevelRecord(3600f, 0);

                for (int i = 0; i < 13; i++)
                {
                    blankTimes.Add(blankRecord);
                }
                TimeLists.bestTimes = blankTimes;

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    formatter.Serialize(stream, blankTimes);
                }
            }
        }

        public static void SaveSettings()
        {
            string path = Application.persistentDataPath + "/settings.wtf";
            
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, SettingsData.settings);
            }
        }

        public static void LoadSettings()
        {
            string path = Application.persistentDataPath + "/settings.wtf";
            BinaryFormatter formatter = new BinaryFormatter();

            if (File.Exists(path))
            {
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    SettingsClass savedSettings = formatter.Deserialize(stream) as SettingsClass;
                    SettingsData.settings = savedSettings;
                }
            }

            else
            {
                SettingsClass defaultSettings = new SettingsClass(0.7f, 0.7f, 0.7f, 0, true);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    formatter.Serialize(stream, defaultSettings);
                }
            }
        }
    }
}