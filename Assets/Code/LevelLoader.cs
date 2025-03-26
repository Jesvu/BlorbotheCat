using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using TMPro;
using blorbothecat.States;

namespace Bluescreen.BlorboTheCat
{
    public class LevelLoader : MonoBehaviour
    {
        public Animator transition;
        public TMP_Text startTextObject;
        public AudioMixer mixer;
        private TextMeshProUGUI startText;
        public static bool firstActionDone = false;
        private static bool startWaitDone = false;

        [SerializeField] private bool nonMenu = true;

        void Start()
        {
            if (StateTrackerManager.Instance != null && nonMenu) { StateTrackerManager.Instance.inMainMenu = false; }
            else if(StateTrackerManager.Instance != null && !nonMenu) { StateTrackerManager.Instance.inMainMenu = true; }
            //else if (StateTrackerManager.Instance == null) { print("state tracking not possible - levelloader"); }

            SaveData.Load();
            SaveData.LoadSettings();
            SetToSettingsVolume();
            Fullscreen(SettingsData.settings.fullscreen);
            SetResolution(SettingsData.settings.resolutionIndex);

            if(nonMenu)
            {
                
                startText = startTextObject.GetComponent<TextMeshProUGUI>();
                startText.enabled = false;
                StartCoroutine(StartWait());

            }
            
            // if in a level
            if (GameObject.Find("Player") != null)
            {
                SuperBomb.superbombsLeft = TimeLists.levelInfo[SceneManager.GetActiveScene().buildIndex-3].superBombs;
                LatestCompletionInfo.levelNumber = SceneManager.GetActiveScene().buildIndex-2;
            }
        }

        void Update()
        {
            if (startWaitDone && nonMenu)
            {
                if (firstActionDone)
                {
                    startText.enabled = false;
                }
                else
                {
                    startText.enabled = true;
                }
            }
        }

        IEnumerator StartWait()
        {
            firstActionDone = false;
            PlayerController.movementFrozen = true;
            PlayerActions.actionsFrozen = true;

            yield return new WaitForSeconds(1f);
            if (!BezierFollow.introOngoing || GameObject.Find("Start camera route") == null)
            {
                startWaitDone = true;
                PlayerController.movementFrozen = false;
                PlayerActions.actionsFrozen = false;
            }
        }

        public void ChangeScene(int sceneIndex)
        {
            StartCoroutine(LoadLevel(sceneIndex));
        }

        IEnumerator LoadLevel(int sceneIndex)
        {
            transition.SetTrigger("Start");
            if(nonMenu)
            {
                PlayerController.movementFrozen = true;
                PlayerActions.actionsFrozen = true;
            }
            
            MusicManager mm = FindObjectOfType<MusicManager>();
            if (mm != null && sceneIndex != -1) { MusicManager.Instance.StartTransition(); }
            
            yield return new WaitForSeconds(1f);
            Bomb.bombCount = 0;
            SuperBomb.superbombCount = 0;
            SuperBomb.superbombsLeft = 3;
            PlayerHealth.playerHealth = 3;
            LatestCompletionInfo.enemiesKilled = 0;
            LatestCompletionInfo.damageTaken = 0;

            if (sceneIndex == -1) {sceneIndex = SceneManager.GetActiveScene().buildIndex;}
            else { BezierFollow.introOngoing = true; }
            
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
            if (nonMenu) { GameStateManager.Instance.Go(StateType.InGame); }
        }

        public void SetToSettingsVolume()
        {
            mixer.SetFloat("MusicVol", Mathf.Log10(SettingsData.settings.musicVolume)*20);
            mixer.SetFloat("SFXVol", Mathf.Log10(SettingsData.settings.sfxVolume)*20);
            mixer.SetFloat("AmbienceVol", Mathf.Log10(SettingsData.settings.ambVolume)*20);
        }

        public void Fullscreen(bool isFS)
        {
            Screen.fullScreen = isFS;
            SettingsData.settings.fullscreen = isFS;
            SaveData.SaveSettings();
        }

        public void SetResolution(int index)
        {
            switch(index)
            {
                case 0:
                    Screen.SetResolution(960, 540, Screen.fullScreen);
                    break;
                case 1:
                    Screen.SetResolution(1280, 720, Screen.fullScreen);
                    break;
                case 2:
                    Screen.SetResolution(1600, 900, Screen.fullScreen);
                    break;
                case 3:
                    Screen.SetResolution(1920, 1080, Screen.fullScreen);
                    break;
                case 4:
                    Screen.SetResolution(2560, 1440, Screen.fullScreen);
                    break;
                default:
                    break;
            }
            SettingsData.settings.resolutionIndex = index;
            SaveData.SaveSettings();
        }
    }
}