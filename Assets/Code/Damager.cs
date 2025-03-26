using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] public int damage = 1;
        private Collider2D hitObject;
        private bool hitting = false;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Player"))
            {
                hitting = true;
                hitObject = other;
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Player"))
            {
                hitting = false;
            }
        }

        void FixedUpdate()
        {
            if (hitting)
            {
                // if hitting the player, call the playerhealth class and remove health
                if (hitObject.CompareTag("Player"))
                {
                    hitObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                }
                // if hitting an enemy, you get the point
                if (hitObject.CompareTag("Enemy") && !gameObject.CompareTag("Enemy"))
                {
                    hitObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                }
            }
        }
    }
}
