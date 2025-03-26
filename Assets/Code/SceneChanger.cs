using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using blorbothecat.States;

namespace Bluescreen.BlorboTheCat
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private int nextSceneIndex = 0;
        private int currentLevelIndex;
        private bool clickable = false;
        private GameObject loader;
        [SerializeField] private AudioSource sfx;
        [SerializeField] private GameObject particles;

        void Start()
        {
            loader = GameObject.Find("LevelLoader");
            currentLevelIndex = SceneManager.GetActiveScene().buildIndex - 3;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                
                // stopping timer and getting finish time
                GameObject timerObj = GameObject.Find("Timer");
                Timer timerScr = timerObj.GetComponent<Timer>();
                timerScr.timerRunning = false;
                float finishTime = (float)Mathf.Round(timerScr.timer * 1000f) / 1000f;

                PlayerActions.actionsFrozen = true;
                PlayerController.movementFrozen = true;

                LatestCompletionInfo.completionTime = finishTime;
                LatestCompletionInfo.levelNumber = currentLevelIndex+1;

                StartCoroutine(EndWait(other.gameObject));
                //loader.GetComponent<LevelLoader>().ChangeScene(nextSceneIndex);
            }
        }

        IEnumerator EndWait(GameObject player)
        {
            player.GetComponent<PlayerHealth>().invincible = true;
            sfx.Play();
            Instantiate(particles, transform.position + new Vector3(0f,1f,0f), transform.rotation);
            yield return new WaitForSeconds(0.2f);

            for (float i = 1f; i > 0f; i -= Time.deltaTime)
            {
                //Color asd = new Color(1, 1, 1, Mathf.Lerp(player.GetComponent<SpriteRenderer>().color.a,1,i));
                Color asd = new Color(1, 1, 1, i);
                player.GetComponent<SpriteRenderer>().color = asd;
                yield return null;
            }
            player.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            GameStateManager.Instance.Go(StateType.LevelOver);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (clickable)
            {
                loader.GetComponent<LevelLoader>().ChangeScene(nextSceneIndex);
            }
        }
    }
}
