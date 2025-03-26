using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class StateTrackerManager : MonoBehaviour
    {
        public static StateTrackerManager Instance;
        
        public bool inMainMenu;
        public bool loadToLevelSelect;
        
        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        
        
    }
}
