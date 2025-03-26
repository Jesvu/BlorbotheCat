using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Bluescreen.BlorboTheCat
{
    public class Timer : MonoBehaviour
    {
        public float timer = 0.0f;
        public bool timerRunning = true;
        private TimeSpan timeObj;
        private string timeStr;
        public TMP_Text timerText;

        void Update()
        {
            // one hour maximum limit just to be sure lol
            if (timerRunning && timer < 3599.9f && LevelLoader.firstActionDone)
            {
                timer += Time.deltaTime;
                timeObj = TimeSpan.FromSeconds(timer);
                timeStr = timeObj.ToString(@"m\:ss\.f");
                timerText.text = timeStr;
            }
        }
    }
}
