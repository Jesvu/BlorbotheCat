using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bluescreen.BlorboTheCat
{
    public class CanvasCameraFinder : MonoBehaviour
    {
        private Canvas cv;

        void Awake()
        {
            cv = GetComponent<Canvas>();

            cv.worldCamera = Camera.main;
        }
    }
}
