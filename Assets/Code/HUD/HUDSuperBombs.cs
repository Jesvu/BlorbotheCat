using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDSuperBombs : MonoBehaviour
{
    [SerializeField]private Animator ani;
    public Image img;

    void Update()
    {
        // change indicator based on superbombs available to use
        switch (SuperBomb.superbombsLeft)
        {
            case 3:
                img.enabled = true;
                ani.SetInteger("BombCount", 3);
                break;
            case 2:
                img.enabled = true;
                ani.SetInteger("BombCount", 2);
                break;
            case 1:
                img.enabled = true;
                ani.SetInteger("BombCount", 1);
                break;
            default:
                img.enabled = false;
                break;
        }
    }
}
