using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeInvisible : MonoBehaviour
    {
        // just makes the object invisible in game, idk if there is some other way to have it
        // visible in the editor but not in game, but this works anyways
        void Start()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
        }
    }