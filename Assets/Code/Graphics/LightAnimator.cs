using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimator : MonoBehaviour
{
    [SerializeField] private bool hasLight;
    [Space(3)]

    [Header("Sprite")]
    [SerializeField] private Sprite[] lightSprites;
    [Space(3)]

    [Header("Mask")]
    [SerializeField] private Sprite[] maskSprites;
    [Space(3)]

    [SerializeField] private float frameWait = 0.5f;
    [SerializeField] private int extraFrames = 1;
    [SerializeField]private int modifier;
    [SerializeField] private int finalModifier;
    private float animationTimeCounter;
    

    private SpriteMask mask;
    private SpriteRenderer sr;



    void Start()
    {
        mask = GetComponent<SpriteMask>();
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = hasLight;
        
        extraFrames = maskSprites.Length - 2;

        //Random frame variation on start
        animationTimeCounter = Random.Range(0f, frameWait*maskSprites.Length+extraFrames);
    }

    
    void Update()
    {

        //reset clock when loop complete
        if (animationTimeCounter > frameWait * (maskSprites.Length+extraFrames))
        {
            finalModifier = 0;
            animationTimeCounter = 0f;
            
        }

        animationTimeCounter += Time.deltaTime;

        UpdateSprites();

    }

    void UpdateSprites()
    {
        for (int i = 0; i < maskSprites.Length+extraFrames; i++)
        {
            //duplicate frames for smooth back and forth
            if (extraFrames != 0 && animationTimeCounter >= frameWait * maskSprites.Length && animationTimeCounter <= frameWait * (maskSprites.Length + extraFrames))
            {
                
                
                if (i > maskSprites.Length-1 && animationTimeCounter > frameWait * i && animationTimeCounter <= frameWait * i+1) 
                {
                    
                    modifier = (i - (maskSprites.Length-1));
                    finalModifier = i - (modifier * 2);                   
                    mask.sprite = maskSprites[finalModifier];
                    if (hasLight && lightSprites != null) { sr.sprite = lightSprites[finalModifier]; }
                }
                

                
            }
            //original given frames before going backwards
            else if (animationTimeCounter >= frameWait * i && animationTimeCounter <= frameWait * (i+1))
            {
                mask.sprite = maskSprites[i];

                if(hasLight && lightSprites != null) { sr.sprite = lightSprites[i]; }
                
            }
        }

        
    }

    
}
