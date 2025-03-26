using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bluescreen.BlorboTheCat
{
    public class PointsEarnedGraphic : MonoBehaviour
    {
        private LevelSelectScreen ls;
        public Sprite one;
        public Sprite two;
        public Sprite three;

        void Start()
        {
            GameObject levelselect = GameObject.Find("Level select Canvas");
            ls = levelselect.GetComponent<LevelSelectScreen>();
        }

        void Update()
        {
            switch (TimeLists.bestTimes[ls.currentSelectedLevel - 1].stars)
            {
                case 3:
                    GetComponent<Image>().enabled = true;
                    GetComponent<Image>().sprite = three;
                    break;
                case 2:
                    GetComponent<Image>().enabled = true;
                    GetComponent<Image>().sprite = two;
                    break;
                case 1:
                    GetComponent<Image>().enabled = true;
                    GetComponent<Image>().sprite = one;
                    break;
                default:
                    GetComponent<Image>().enabled = false;
                    break;
            }
        }
    }
}
