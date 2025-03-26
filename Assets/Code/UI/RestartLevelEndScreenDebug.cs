using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class RestartLevelEndScreenDebug : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                GetComponent<Animator>().Play("endoflevelscreen");
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                GetComponent<Animator>().Play("endoflevelscreen_nohighscore");
            }
        }
    }
}
