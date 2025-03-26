using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bluescreen.BlorboTheCat
{
    public class HUDHealth : MonoBehaviour
    {
        public Sprite health0;
        public Sprite health1;
        public Sprite health2;
        public Sprite health3;
        [SerializeField] private GameObject particles;
        [SerializeField] private GameObject explosion;
        [SerializeField] private Transform one;
        [SerializeField] private Transform two;
        [SerializeField] private Transform three;

        void Update()
        {
            // change indicator based on player health
            switch (PlayerHealth.playerHealth)
            {
                case 3:
                    GetComponent<Image>().sprite = health3;
                    break;
                case 2:
                    GetComponent<Image>().sprite = health2;
                    break;
                case 1:
                    GetComponent<Image>().sprite = health1;
                    break;
                default:
                    GetComponent<Image>().sprite = health0;
                    break;
            }
        }

        public void DamageParticles()
        {
            switch (PlayerHealth.playerHealth)
            {
                case 2:
                    Instantiate(particles, three.position, transform.rotation, transform);
                    Instantiate(explosion, three.position, transform.rotation, transform);
                    break;
                case 1:
                    Instantiate(particles, two.position, transform.rotation, transform);
                    Instantiate(explosion, two.position, transform.rotation, transform);
                    break;
                case 0:
                    Instantiate(particles, one.position, transform.rotation, transform);
                    Instantiate(explosion, one.position, transform.rotation, transform);
                    break;
                default:
                    break;
            }
        }
    }
}
