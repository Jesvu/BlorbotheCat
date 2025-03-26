using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class ButtonSfx : MonoBehaviour
    {
        [SerializeField] private AudioClip click;
        [SerializeField] private AudioClip hover;
        [SerializeField] private float clickVolume = 0.25f;
        [SerializeField] private float hoverVolume = 0.5f;
        private AudioSource aSource;


        private void Awake()
        {
            aSource = GetComponent<AudioSource>();
        }

        public void OnClick()
        {
            aSource.PlayOneShot(click, clickVolume);
        }
        public void OnHover()
        {
            aSource.PlayOneShot(hover, hoverVolume);
        }
    }
    
}
