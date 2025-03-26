using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using blorbothecat.States;

namespace Bluescreen.BlorboTheCat
{
    public class LevelEndScreen : MonoBehaviour
    {
        [SerializeField] private GameObject newHighscoreText;
        [SerializeField] private GameObject pbTitle;
        [SerializeField] private GameObject pb;
        [SerializeField] private GameObject medal;
        [SerializeField] private GameObject healthModifierText;
        [SerializeField] private GameObject healthTimeText;
        [SerializeField] private GameObject enemyModifierText;
        [SerializeField] private GameObject enemyTimeText;
        [SerializeField] private GameObject levelNumberText;
        [SerializeField] private GameObject originalTimeText;
        [SerializeField] private GameObject finalTimeText;
        [SerializeField] private Sprite moon1;
        [SerializeField] private Sprite moon2;
        [SerializeField] private Sprite moon3;

        [SerializeField] private AudioSource modifierSfx;
        [SerializeField] private AudioSource fullTimeSfx;
        [SerializeField] private AudioSource moonSfx;
        [SerializeField] private ParticleSystem particles;

        private int currentLevelIndex;
        private float finishTime;
        private int damageTaken;
        private int enemiesKilled;
        private int stars;
        private float realFinishTime;
 
        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1;

            currentLevelIndex = LatestCompletionInfo.levelNumber-1;
            finishTime = LatestCompletionInfo.completionTime;
            damageTaken = LatestCompletionInfo.damageTaken;
            enemiesKilled = LatestCompletionInfo.enemiesKilled;

            UpdateMedal();
            TimeSpan pbTime = TimeSpan.FromSeconds(TimeLists.bestTimes[currentLevelIndex].time);
            pb.GetComponent<TextMeshProUGUI>().text = pbTime.ToString(@"m\:ss\.fff");
            
            stars = 0;
            float[] blankTargetTimes = new float[] {0f, 0f, 0f};

            realFinishTime = finishTime + damageTaken*5 - enemiesKilled*2;
            if (realFinishTime < 0.001f)
            {
                realFinishTime = 0.001f;
                Debug.Log("lol");
            }

            if (TimeLists.bestTimes[currentLevelIndex].time > realFinishTime)
            {
                // if target times exist for current level, give the correct stars
                if (!TimeLists.levelInfo[currentLevelIndex].targetTimes.SequenceEqual(blankTargetTimes))
                {
                    if (finishTime <= TimeLists.levelInfo[currentLevelIndex].targetTimes[2] && TimeLists.bestTimes[currentLevelIndex].stars < 3)
                    {
                        stars = 3;
                    }
                    else if (finishTime <= TimeLists.levelInfo[currentLevelIndex].targetTimes[1] && TimeLists.bestTimes[currentLevelIndex].stars < 2)
                    {
                        stars = 2;
                    }
                    else if (finishTime <= TimeLists.levelInfo[currentLevelIndex].targetTimes[0] && TimeLists.bestTimes[currentLevelIndex].stars < 1)
                    {
                        stars = 1;
                    }
                    else if (TimeLists.bestTimes[currentLevelIndex].stars > 0)
                    {
                        stars = TimeLists.bestTimes[currentLevelIndex].stars;
                    }
                }
                SaveData.LevelRecord finishInfo = new SaveData.LevelRecord(realFinishTime, stars);
                TimeLists.bestTimes[currentLevelIndex] = finishInfo;
                GetComponent<Animator>().Play("endoflevelscreen");
            }
            else
            {
                GetComponent<Animator>().Play("endoflevelscreen_nohighscore");
            }

            SaveData.Save();

            levelNumberText.GetComponent<TextMeshProUGUI>().text = $"{currentLevelIndex+1}";

            TimeSpan ogTime = TimeSpan.FromSeconds(finishTime);
            originalTimeText.GetComponent<TextMeshProUGUI>().text = ogTime.ToString(@"m\:ss\.fff");

            healthModifierText.GetComponent<TextMeshProUGUI>().text = $"{damageTaken}x";
            TimeSpan healthTime = TimeSpan.FromSeconds(damageTaken * 5);
            healthTimeText.GetComponent<TextMeshProUGUI>().text = $"+ {healthTime.ToString(@"m\:ss\.fff")}";

            enemyModifierText.GetComponent<TextMeshProUGUI>().text = $"{enemiesKilled}x";
            TimeSpan enemyTime = TimeSpan.FromSeconds(enemiesKilled * 2);
            enemyTimeText.GetComponent<TextMeshProUGUI>().text = $"- {enemyTime.ToString(@"m\:ss\.fff")}";

            TimeSpan finTime = TimeSpan.FromSeconds(realFinishTime);
            finalTimeText.GetComponent<TextMeshProUGUI>().text = finTime.ToString(@"m\:ss\.fff");
        }

        public void ShowNewRecord() // called from animation if new record :P
        {
            //update personal best to new 
            newHighscoreText.SetActive(true);
            pbTitle.SetActive(false);
            particles.Play();

            TimeSpan finTime = TimeSpan.FromSeconds(realFinishTime);
            pb.GetComponent<TextMeshProUGUI>().text = finTime.ToString(@"m\:ss\.fff");

            UpdateMedal();
        }

        private void UpdateMedal()
        {
            switch(TimeLists.bestTimes[currentLevelIndex].stars)
            {
                case 3:
                    medal.GetComponent<Image>().enabled = true;
                    medal.GetComponent<Image>().sprite = moon3;
                    break;
                case 2:
                    medal.GetComponent<Image>().enabled = true;
                    medal.GetComponent<Image>().sprite = moon2;
                    break;
                case 1:
                    medal.GetComponent<Image>().enabled = true;
                    medal.GetComponent<Image>().sprite = moon1;
                    break;
                default:
                    medal.GetComponent<Image>().enabled = false;
                    break;
            }
        }

        public void ExitToMenu()
        {
            if (StateTrackerManager.Instance != null)
            {
                StateTrackerManager.Instance.loadToLevelSelect = true;
            }
            GameStateManager.Instance.Go(StateType.MainMenu);
        }

        public void RestartLevel()
        {
            GameStateManager.Instance.Go(StateType.InGame);
            GameObject loader = GameObject.Find("LevelLoader");
            loader.GetComponent<LevelLoader>().ChangeScene(-1);
        }

        public void PlaySFX(int index)
        {
            if(index == 1)
            {
                modifierSfx.Play();
            }
            else if(index == 2)
            {
                fullTimeSfx.Play();
            }
            else if(index == 3)
            {
                moonSfx.Play();
            }
                
        }
    }
}
