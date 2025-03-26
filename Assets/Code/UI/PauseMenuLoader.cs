using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using blorbothecat.States;

namespace Bluescreen.BlorboTheCat
{
    public class PauseMenuLoader : MonoBehaviour
    {
        [SerializeField] private bool useStateInstead = false;
        public bool paused;
        [SerializeField] private GameObject pausemenu;


        void Awake() { paused = false; }
        // Update is called once per frame
        void Update()
        {
            if (!paused && Input.GetKeyDown(KeyCode.Escape) && ((!BezierFollow.introOngoing && GameObject.Find("Start camera route") != null) || (GameObject.Find("Start camera route") == null)))
            {
                print("pausing game");
                PauseGame();
            }
            if(paused || (BezierFollow.introOngoing && GameObject.Find("Start camera route") != null))
            {
                PlayerActions.actionsFrozen = true;
                PlayerController.movementFrozen = true;
            }
            else
            {
                PlayerActions.actionsFrozen = false;
                PlayerController.movementFrozen = false;
            }
        }

        public void PauseGame()
        {
            if(useStateInstead)
            {
                GameStateManager.Instance.Go(StateType.Options);
                paused = true;
            }
            else
            {
                if (paused)
                {
                    paused = false;
                    pausemenu.SetActive(false);
                    
                    Time.timeScale = 1f;
                }
                else
                {
                    paused = true;
                    pausemenu.SetActive(true);
                    
                    Time.timeScale = 0f;
                }
            }
        }
    }
}
