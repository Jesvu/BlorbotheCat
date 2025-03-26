using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class FallingRockSensor : MonoBehaviour
    {
        public GameObject rock;
        public GameObject player;
        public Transform triggerArea;
        public GameObject spot;
        public Transform target;
        [SerializeField] private AudioSource rockThud;
        private bool isTriggered;

        void Awake()
        {  
            triggerArea.parent = null;
            rock.SetActive(false);
            target.parent = null;
            rockThud = GetComponent<AudioSource>();
            isTriggered = false;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //if (rock.transform.position == triggerArea.position)
            //{
            //    PlayThudSFX();
            //}

            //if(isTriggered == true)
            //{
                
            //}
        }
        public void OnTriggerEnter2D(Collider2D rockcollider)
        {
            if (rockcollider.gameObject.CompareTag("Player") && rock != null && isTriggered == false)
            {
                Activate();
            }
        }

        void Activate()
        {
            StartCoroutine(Wait());
        }

        IEnumerator Wait()
        {
            //yield on a new YieldInstruction that waits for 1 second.
            yield return new WaitForSeconds(1);
            rock.SetActive(true);
            isTriggered = true;
            Debug.Log("Works");
        }

        //private void PlayThudSFX()
        //{
        //    rockThud.Play();
        //}
    }
}
