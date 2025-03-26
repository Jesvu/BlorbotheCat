using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using blorbothecat.States;

namespace Bluescreen.BlorboTheCat
{
    public class UIOptions : MonoBehaviour
    {
        public TMPro.TMP_Dropdown resolutionDropdown;
        public Toggle fstoggle;
        LevelLoader loader;
        private bool inMenu=false;
        [SerializeField] private Transform boxTf;
        [SerializeField] private GameObject mainMenuButton;
        [SerializeField] private GameObject dataClearButton;
        [SerializeField] private Canvas options;
        [SerializeField] private Canvas confirmation;
        [SerializeField] private Canvas forcequit;

        public void Start()
        {
            GameObject lol = GameObject.Find("LevelLoader");
            loader = lol.GetComponent<LevelLoader>();
            SaveData.LoadSettings();
            fstoggle.isOn = SettingsData.settings.fullscreen;
            resolutionDropdown.value = SettingsData.settings.resolutionIndex;

            if (StateTrackerManager.Instance != null && StateTrackerManager.Instance.inMainMenu == true)
            {
                inMenu = true;
                mainMenuButton.SetActive(false);
                dataClearButton.SetActive(true);
            }
            //else if (StateTrackerManager.Instance != null && StateTrackerManager.Instance.inMainMenu == false) { mainMenuButton.SetActive(true); }
            //else if (StateTrackerManager.Instance == null) { print("state tracking not possible - ui options"); }
        }

        public void Close()
        {
            GameStateManager.Instance.GoBack();
            print("unpaused");
            if (!inMenu) { GameObject.Find("Pause Menu Loader").GetComponent<PauseMenuLoader>().paused = false; }
            
        }

        public void ExitToMenu()
        {
            if (StateTrackerManager.Instance != null)
            {
                StateTrackerManager.Instance.loadToLevelSelect = true;
            }
            GameStateManager.Instance.Go(StateType.MainMenu);
        }

        public void Fullscreen(bool isFS)
        {
            loader.Fullscreen(isFS);
        }

        public void SetResolution(int index)
        {
            loader.SetResolution(index);
        }

        public void DeleteSaveData()
        {
            string path = Application.persistentDataPath + "/records.wtf";
            File.Delete(path);
            Debug.Log("Save deleted");
            confirmation.enabled = false;
            forcequit.enabled = true;
        }

        public void SaveDataConfirmation()
        {
            options.enabled = false;
            confirmation.enabled = true;
        }
        public void SaveDataConfirmationClose()
        {
            confirmation.enabled = false;
            options.enabled = true;
        }

        

        public void QuitGame()
        {
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().ChangeScene(0);
            StateTrackerManager.Instance.loadToLevelSelect = false;
            GameStateManager.Instance.GoBack();
            print("reloading menu for save reset");
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
                
            }
        }
    }
}