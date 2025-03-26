using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class spriteAnimatorScript : MonoBehaviour
    {
        
        [SerializeField] private Sprite[] frames;
        [SerializeField] private Sprite[] frames2;
        [Space(3)]

        private SpriteRenderer sr;

        [SerializeField] private bool randomStart = false;
        [SerializeField] private bool multipleAnimations = false;
        private int animationRandomizer;
        [SerializeField] private float frameWait = 0.5f;
        private float animationTimeCounter;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            if(randomStart)
            {
                animationTimeCounter = Random.Range(0f, frames.Length*frameWait + 1);
            }

            if(multipleAnimations)
            {
                animationRandomizer = Random.Range(0, 2);
            }
            else
            {
                animationRandomizer = 0;
            }
            
        }

        void Update()
        {
            if (animationTimeCounter > frameWait * frames.Length)
            {
                
                animationTimeCounter = 0f;

            }
            animationTimeCounter += Time.deltaTime;

            UpdateSprites();
        }

        void UpdateSprites()
        {
            for (int i = 0; i < frames.Length; i++)
            {
                
                if (animationTimeCounter >= frameWait * i && animationTimeCounter <= frameWait * (i + 1))
                {
                    if(animationRandomizer == 0) { sr.sprite = frames[i]; }
                    else if(animationRandomizer == 1) { sr.sprite = frames2[i]; }
                    


                }
            }


        }
    }

    
}
