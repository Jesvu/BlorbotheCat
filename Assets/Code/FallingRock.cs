using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class FallingRock : MonoBehaviour
    {
        public GameObject rock;
        public Transform target;
        public float speed;
        public GameObject player;
        public GameObject spot;
        private Collider2D hitObject;
        [SerializeField] public int damage = 1;
        private bool hitting = false;

        void Awake()
        {

        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                hitting = true;
                hitObject = other;
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Rock's movement
            Vector2 a = rock.transform.position;
            Vector2 b = target.position;
            rock.transform.position = Vector2.MoveTowards(a, b, speed);

            //Check if hit player
            if (hitting)
            {
                // if hitting the player, call the playerhealth class and remove health. Also destroys the rock
                if (hitObject.CompareTag("Player") && player.transform.position == target.position && rock.transform.position == target.position)
                {

                    spot.SetActive(false);
                    hitObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                    Destroy();
                }


                //if (player.transform.position == target.position && rock.transform.position == target.position)
                //{
                //    spot.SetActive(false);
                //    hitObject.GetComponent<PlayerHealth>().TakeDamage(1);
                //    Destroy();
                //}

                //if (enemy.transform.position == target.position && rock.transform.position == target.position)
                //{
                //    spot.SetActive(false);
                //    hitObject.GetComponent<EnemyHealth>().TakeDamage(1);
                //    Destroy();
                //}
            }
        }

        void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
