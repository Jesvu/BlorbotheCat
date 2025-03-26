using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bluescreen.BlorboTheCat
{
    public class MovementPrompt : MonoBehaviour
    {
        private float count;
        [SerializeField] private float time;
        [SerializeField] private CanvasGroup group; 


        void Awake()
        {
            
            GameObject.Find("Player").GetComponent<PlayerActions>().enabled = false;
            GameObject.Find("bomb").SetActive(false);
            GameObject.Find("Superbomb counter").SetActive(false);
        }
        void Update()
        {
            if(BezierFollow.introOngoing)
            {
                count = 0f;
            }
            else
            {

                if (!LevelLoader.firstActionDone && count < time)
                {
                    count += Time.deltaTime;
                }
                else if (!LevelLoader.firstActionDone && count >= time) { count = time; }
                else if(LevelLoader.firstActionDone) { count -= Time.deltaTime; }

            }
            group.alpha = count / time;
            if (count < 0)
            {
                gameObject.SetActive(false);
                this.enabled = false;
            }

        }
    }
}
