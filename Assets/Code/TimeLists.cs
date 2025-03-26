using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public static class TimeLists
    {
        // list of best times
        public static List<SaveData.LevelRecord> bestTimes = new List<SaveData.LevelRecord>();

        // list of target times, required stars for unlocking and available superbombs, for each level
        public static List<SaveData.LevelInfo> levelInfo = new List<SaveData.LevelInfo>
        {
            
            // level 1
            new SaveData.LevelInfo(new float[] {0f, 0f, 0f}, 0, 0),
            // level 2
            new SaveData.LevelInfo(new float[] {0f, 0f, 0f}, 1, 0),
            // level 3
            new SaveData.LevelInfo(new float[] {0f, 0f, 0f}, 3, 0),
            // level 4
            new SaveData.LevelInfo(new float[] {6f, 4f, 1f}, 5, 0),
            // level 5
            new SaveData.LevelInfo(new float[] {30f, 24f, 18f}, 5, 0),
            // level 6
            new SaveData.LevelInfo(new float[] {14f, 8f, 3f}, 8, 0),
            // level 7
            new SaveData.LevelInfo(new float[] {30f, 25f, 20f}, 8, 0),
            // level 8
            new SaveData.LevelInfo(new float[] {30f, 22f, 15f}, 12, 0),
            // level 9
            new SaveData.LevelInfo(new float[] {30f, 20f, 12f}, 12, 1),
            // level 10
            new SaveData.LevelInfo(new float[] {50f, 40f, 32f}, 15, 1),
            // level 11
            new SaveData.LevelInfo(new float[] {45f, 30f, 25f}, 15, 0),
            // level 12
            new SaveData.LevelInfo(new float[] {60f, 45f, 35f}, 20, 1),
            // level 13
            new SaveData.LevelInfo(new float[] {0f, 0f, 0f}, 25, 0)
        };
    }
}