using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bluescreen.BlorboTheCat
{
    public class MusicManager : MonoBehaviour
    {
        public AudioSource bgm;
        public static MusicManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            StartCoroutine(FadeIn());
        }

        public void ChangeBGM(AudioClip music)
        {
            if (bgm.clip.name == music.name) { return; }
            bgm.clip = music;
            bgm.Play();
        }

        public void StartTransition()
        {
            StartCoroutine(FadeOut());
        }

        IEnumerator FadeOut()
        {
            while (bgm.volume > 0)
            {
                bgm.volume -= Time.deltaTime;
                yield return null;
            }
        }

        IEnumerator FadeIn()
        {
            while (bgm.volume < 1)
            {
                bgm.volume += Time.deltaTime*2;
                yield return null;
            }
            bgm.volume = 1;
        }
    }
}
