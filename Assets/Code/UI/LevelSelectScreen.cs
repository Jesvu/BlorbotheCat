using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using blorbothecat.States;

namespace Bluescreen.BlorboTheCat
{
    public class LevelSelectScreen : MonoBehaviour
    {
        [SerializeField] private UIMainMenu menu;
        public int amountOfLevels;
        public int currentSelectedLevel;
        [SerializeField] private Sprite selected;
        [SerializeField] private Sprite notSelected;
        [SerializeField] private Image levelImage;
        [SerializeField] private GameObject[] buttons;
        [SerializeField] private Sprite[] levelSprites;
        [SerializeField] private GameObject[] levelCounters;
        [SerializeField] private int requiredPoints;
        [SerializeField] private TotalStars starCounter;
        [SerializeField] private GameObject info;
        [SerializeField] private GameObject lockIcon;
        [SerializeField] private GameObject playButton;
        [SerializeField] private GameObject personalBest;
        [SerializeField] private GameObject targetTime1;
        [SerializeField] private GameObject targetTime2;
        [SerializeField] private GameObject targetTime3;
        
        private ButtonSfx sfx;
        
        public void Awake()
        {
            sfx = GetComponent<ButtonSfx>();
        }

        //bottom buttons
        public void SetLevelInfoScreen(int levelCountModifier)
        {
            currentSelectedLevel = levelCountModifier;
            LevelSelectChange();
        }
        //info box side buttons
        public void ScrollButtons(int levelCountModifier)
        {
            currentSelectedLevel += levelCountModifier;
            LevelSelectChange();
        }

        private void LevelSelectChange()
        {
            if (currentSelectedLevel <= 0)
            { currentSelectedLevel += amountOfLevels; }
            else if (currentSelectedLevel > amountOfLevels)
            { currentSelectedLevel -= amountOfLevels; }

            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].GetComponent<LevelSelectScreenButtonValue>().setLevel == currentSelectedLevel)
                {
                    buttons[i].GetComponent<Image>().sprite = selected;
                    buttons[i].GetComponent<Button>().interactable = false;
                }
                else 
                { 
                    buttons[i].GetComponent<Image>().sprite = notSelected;
                    buttons[i].GetComponent<Button>().interactable = true; 
                }
            }
            for (int i = 0; i < levelCounters.Length; i++)
            {
                levelCounters[i].GetComponentInChildren<TMP_Text>().text = currentSelectedLevel.ToString();
            }

            levelImage.sprite = levelSprites[currentSelectedLevel - 1];
            requiredPoints = TimeLists.levelInfo[currentSelectedLevel].starsRequired;

            TimeSpan pbTime = TimeSpan.FromSeconds(TimeLists.bestTimes[currentSelectedLevel - 1].time);
            personalBest.GetComponent<TMP_Text>().text = pbTime.ToString(@"m\:ss\.fff");
            TimeSpan tt1 = TimeSpan.FromSeconds(TimeLists.levelInfo[currentSelectedLevel - 1].targetTimes[0]);
            targetTime1.GetComponent<TMP_Text>().text = tt1.ToString(@"m\:ss\.fff");
            TimeSpan tt2 = TimeSpan.FromSeconds(TimeLists.levelInfo[currentSelectedLevel - 1].targetTimes[1]);
            targetTime2.GetComponent<TMP_Text>().text = tt2.ToString(@"m\:ss\.fff");
            TimeSpan tt3 = TimeSpan.FromSeconds(TimeLists.levelInfo[currentSelectedLevel - 1].targetTimes[2]);
            targetTime3.GetComponent<TMP_Text>().text = tt3.ToString(@"m\:ss\.fff");
        }

        public void PlaySelectedLevel()
        {
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().ChangeScene(currentSelectedLevel+2);
            
        }

        private void Start()
        {
            LevelSelectChange();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A) && menu.inSelect)
            {
                sfx.OnClick();
                ScrollButtons(-1);
            }
            if(Input.GetKeyDown(KeyCode.D) && menu.inSelect)
            {
                sfx.OnClick();
                ScrollButtons(1);
            }

            if(Input.GetKeyDown(KeyCode.Space) && menu.inSelect)
            {
                PlaySelectedLevel();
            }
            


            if(requiredPoints > starCounter.totalStars)
            {
                info.GetComponent<CanvasGroup>().alpha = 0.3f;
                lockIcon.SetActive(true);
                playButton.GetComponent<Button>().interactable = false;
                playButton.GetComponent<AudioSource>().enabled = false;
            }
            else
            {
                playButton.GetComponent<Button>().interactable = true;
                playButton.GetComponent<AudioSource>().enabled = true;
                info.GetComponent<CanvasGroup>().alpha = 1f;
                lockIcon.SetActive(false);
            }
        }
    }
}
