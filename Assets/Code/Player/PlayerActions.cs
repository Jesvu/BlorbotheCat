using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using blorbothecat.States;

namespace Bluescreen.BlorboTheCat
{
    public class PlayerActions : MonoBehaviour
    {
        public LayerMask playerLayer;
        public GameObject bomb;
        public GameObject superbomb;
        public Animator animator;
        public static bool actionsFrozen = false;
        [SerializeField] private AudioSource bombdropAudioSource;
        
        void Update()
        {
            // dropping a bomb
            if ((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Mouse0)) && !actionsFrozen)
            {
                // making sure there's not a bomb already below the player and that there's 3 bombs in the scene max
                Collider2D groundClearCheck = Physics2D.OverlapPoint(transform.position, ~playerLayer);
                if (!groundClearCheck && (Bomb.bombCount + SuperBomb.superbombCount) <= 2)
                {
                    // the roundtoint function makes the bombs snap to tiles
                    Instantiate(bomb, new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)), Quaternion.identity);
                    //Vomit();
                    PlayDropSFX();
                }

                if (!LevelLoader.firstActionDone) {LevelLoader.firstActionDone = true;}
            }
            // dropping a superbomb
            if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Mouse1)) && !actionsFrozen)
            {
                // making sure there's not a bomb already below the player and that there's 3 bombs in the scene max
                Collider2D groundClearCheck = Physics2D.OverlapPoint(transform.position, ~playerLayer);
                if (!groundClearCheck && (Bomb.bombCount + SuperBomb.superbombCount) <= 2 && SuperBomb.superbombsLeft > 0)
                {
                    // the roundtoint function makes the bombs snap to tiles
                    Instantiate(superbomb, new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)), Quaternion.identity);
                    SuperBomb.superbombsLeft--;
                    //Vomit();
                    PlayDropSFX();
                }

                if (!LevelLoader.firstActionDone) {LevelLoader.firstActionDone = true;}
            }
            // quick level reset
            if (Input.GetKeyDown(KeyCode.R) && !actionsFrozen)
            {
                GameObject loader = GameObject.Find("LevelLoader");
                loader.GetComponent<LevelLoader>().ChangeScene(-1);
            }
            
            /*// !!! SAVE TESTING DEBUG STUFF !!!
            if (Input.GetKeyDown(KeyCode.Q))
            {
                string path = Application.persistentDataPath + "/records.wtf";
                File.Delete(path);
                Debug.Log("Save deleted");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                string path = Application.persistentDataPath + "/settings.wtf";
                File.Delete(path);
                Debug.Log("Settings deleted");
            }*/
        }

        /*void Vomit()
        {
            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
            animator.SetTrigger("Vomit");
        }
        */

        private void PlayDropSFX()
        {
            bombdropAudioSource.Play();
        }
    }
    }
