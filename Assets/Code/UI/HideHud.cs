using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

namespace Bluescreen.BlorboTheCat
{
    public class HideHud : MonoBehaviour
    {

        private float hudShowTimer;
        [SerializeField] private float hudShowTime=1f;
        private float skipShowTimer;
        [SerializeField] private float skipShowTime = 1f;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private float animationSpeedModifier = 1f;
        [SerializeField] private GameObject skiptext;
        private bool introExists;
        [SerializeField] private GameObject vignette;
        

        void Awake()
        {
            skipShowTimer = -5f;
            skiptext.GetComponent<CanvasGroup>().alpha = 0f;
            if(GameObject.Find("Start camera route") != null) { introExists = true; vignette.SetActive(true); }
            else { introExists = false; hudShowTimer = hudShowTime*3; vignette.SetActive(false); }
   
        }
        // Update is called once per frame
        void Update()
        {
            if(introExists)
            {
                if (BezierFollow.introOngoing)
                {
                    if (skipShowTimer < skipShowTime)
                    {
                        skipShowTimer += Time.deltaTime;
                        if (skipShowTimer >= 0f) { skiptext.GetComponent<CanvasGroup>().alpha = skipShowTimer / skipShowTime; }
                    }
                    else
                    {
                        skiptext.GetComponent<CanvasGroup>().alpha = 1f;
                        skipShowTimer = skipShowTime;
                    }

                    group.alpha = 0f;
                    hudShowTimer = hudShowTime;
                }
                else
                {
                    skipShowTimer -= 2 * animationSpeedModifier * Time.deltaTime;
                    hudShowTimer -= animationSpeedModifier * Time.deltaTime;
                }

                if (hudShowTimer > 0f && !BezierFollow.introOngoing)
                {
                    group.alpha = 1 - (hudShowTimer / hudShowTime);
                    skiptext.GetComponent<CanvasGroup>().alpha = skipShowTimer / skipShowTime;
                    vignette.GetComponent<Image>().color = new Color(255f, 255f, 255f, skiptext.GetComponent<CanvasGroup>().alpha);
                }
                else if (hudShowTimer <= 0f)
                {
                    group.alpha = 1f;
                    skiptext.SetActive(false);
                    vignette.SetActive(false);
                    this.enabled = false;
                }
            }
            else
            {
                hudShowTimer -= animationSpeedModifier * Time.deltaTime;
                if (hudShowTimer > 0f)
                {
                    group.alpha = 1 - (hudShowTimer / hudShowTime);
                }
                else if (hudShowTimer <= 0f)
                {
                    group.alpha = 1f;
                    skiptext.SetActive(false);
                    this.enabled = false;
                }
            }
            




        }
    }
}
