using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Bluescreen.BlorboTheCat
{
    public class TotalStars : MonoBehaviour
    {
        public int totalStars;
        void Start()
        {
            SaveData.Load();
            int stars = 0;
            for (int i = 0; i < TimeLists.bestTimes.Count; i++)
            {
                stars = stars + TimeLists.bestTimes[i].stars;
            }

            TMP_Text textComponent = GetComponent<TMP_Text>();
            totalStars = stars;
            textComponent.text = $"x {stars}";
        }
    }
}