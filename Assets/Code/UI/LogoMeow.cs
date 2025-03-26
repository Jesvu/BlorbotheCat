using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class LogoMeow : MonoBehaviour
    {
        [SerializeField] private AudioSource one;
        [SerializeField] private AudioSource two;
        [SerializeField] private AudioSource tre;
        [SerializeField] private AudioSource four;
        public void Meow()
        {
            int soundchance = Random.Range(1, 5);
            
            if (soundchance == 1) { four.Play(); }
            else if (soundchance == 2) { tre.Play(); }
            else if (soundchance == 3) { two.Play(); }
            else if (soundchance == 4) { one.Play(); }
        }
        
    }
}
