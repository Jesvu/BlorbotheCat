using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    [System.Serializable]
    public class SettingsClass
    {
        public float musicVolume;
        public float sfxVolume;
        public float ambVolume;
        public int resolutionIndex;
        public bool fullscreen;

        public SettingsClass(float mv, float sv, float av, int res, bool fs)
        {
            musicVolume = mv;
            sfxVolume = sv;
            ambVolume = av;
            resolutionIndex = res;
            fullscreen = fs;
        }
    }
}
