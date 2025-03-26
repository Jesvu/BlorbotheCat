using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDBombCount : MonoBehaviour
{
    [SerializeField] private Animator ani;


    void Update()
    {
        // change indicator based on existing bombs
        switch (Bomb.bombCount + SuperBomb.superbombCount)
        {
            case 3:
                ani.SetInteger("BombCount", 0);
                break;
            case 2:
                ani.SetInteger("BombCount", 1);
                break;
            case 1:
                ani.SetInteger("BombCount", 2);
                break;
            default:
                ani.SetInteger("BombCount", 3);
                break;
        }
    }
}
