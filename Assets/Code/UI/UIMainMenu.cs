using blorbothecat.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bluescreen.BlorboTheCat
{
    public class UIMainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuCanvas;
        [SerializeField] private GameObject levelSelectCanvas;
        [SerializeField] private GameObject exitConfirmationCanvas;
        [SerializeField] private GameObject creditsCanvas;
        private bool inCredits = false;
        [HideInInspector] public bool inSelect = false;
        [SerializeField] private GameObject backbutton;
        
        void Awake()
        {
            //timescale was broken and remained at 0 when going back to the menu from options, this is just a very bodged fix
            Time.timeScale = 1;
        }
        public void Start()
        {
            //Checks whether you've gone through the menus during the time you've had the game open & opens up level select if that is the case :P
            if(!StateTrackerManager.Instance.loadToLevelSelect)
            {    
                //whatever main menu "intro" esque thing - press any key sorta deal, maybe :P
            }
            else
            {
                OpenLevelSelect();
            }
        }
        
        public void StartGame()
        {
            GameStateManager.Instance.Go(StateType.InGame);
        }

        public void OpenLevelSelect()
        {
            if(!inSelect)
            {
                mainMenuCanvas.GetComponent<Canvas>().enabled = false;
                levelSelectCanvas.GetComponent<Canvas>().enabled = true;
            }

        }
        public void OpenMainMenu()
        {
            if(inSelect)
            {
                levelSelectCanvas.GetComponent<Canvas>().enabled = false;
                mainMenuCanvas.GetComponent<Canvas>().enabled = true;
            }
            
        }

        public void OpenOptions()
        {
            if (!inSelect && !inCredits) { GameStateManager.Instance.Go(StateType.Options); }
            
        }


        public void ConfirmQuit()
        {
            //exits the game
            //wont do anything on editor
            print("Quitting game...");
            Application.Quit();
        }
        public void DenyQuit()
        {
            mainMenuCanvas.GetComponent<Canvas>().enabled = true;
            exitConfirmationCanvas.GetComponent<Canvas>().enabled = false;
        }
        public void Quit()
        {
            mainMenuCanvas.GetComponent<Canvas>().enabled = false;
            exitConfirmationCanvas.GetComponent<Canvas>().enabled = true;
        }
        public void OpenCredits()
        {
            if(!inCredits)
            {
                inCredits = true;
                mainMenuCanvas.GetComponent<Canvas>().enabled = false;
                creditsCanvas.GetComponent<Canvas>().enabled = true;
            }
            
        }
        public void CloseCredits()
        {
            if(inCredits)
            {
                inCredits = false;
                creditsCanvas.GetComponent<Canvas>().enabled = false;
                mainMenuCanvas.GetComponent<Canvas>().enabled = true;
            }
            
        }
        public void Update()
        {

            if (levelSelectCanvas.GetComponent<Canvas>().enabled == true)
            {
                inSelect = true;
            }
            else { inSelect = false; }

            if (Input.GetKeyDown(KeyCode.Escape) && levelSelectCanvas.GetComponent<Canvas>().enabled == true)
            {
                backbutton.GetComponent<ButtonSfx>().OnClick();
                OpenMainMenu();
            }
            if (Input.GetKeyDown(KeyCode.Escape) && inCredits)
            {
                backbutton.GetComponent<ButtonSfx>().OnClick();
                CloseCredits();
            }
        }
    }
}
