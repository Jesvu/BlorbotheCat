using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class ShootingPlant : MonoBehaviour
    {
        public GameObject player;
        public GameObject enemy;

        private float timer;
        public GameObject projectile;
        public Transform projectilePos;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            if (Vector2.Distance(player.transform.position, enemy.transform.position) < 5)
            {
                if (timer > 2)
                {
                    timer = 0;
                    Invoke("shoot", 2);
                }
            }


        }

        void shoot()
        {
            Instantiate(projectile, projectilePos.position, Quaternion.identity);
        }
    }
}
