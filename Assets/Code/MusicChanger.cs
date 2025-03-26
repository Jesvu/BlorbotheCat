using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class MusicChanger : MonoBehaviour
    {
        public AudioClip bgm;
        private MusicManager mm;

        void Start()
        {
            mm = FindObjectOfType<MusicManager>();

            if (mm == null) { return; }
            if (bgm != null) { mm.ChangeBGM(bgm); }
        }
    }
}
