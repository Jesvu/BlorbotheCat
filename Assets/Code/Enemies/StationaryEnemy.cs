using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bluescreen.BlorboTheCat
{
    public class StationaryEnemy : MonoBehaviour
    {
        public LayerMask ignoreMe;
        private GameObject player;
        public GameObject enemy;
        private Animator animator;

        private float shootTimer;
        public GameObject projectile;
        public Transform projectilePos;
        public bool isDead = false;
        private bool facingRight = true;

        [SerializeField] private AudioSource shootingAudio;

        //private int shooting = 0;
        //private int alert = 0;
        //private int activate = 0;

        [SerializeField] float distance;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        
        // Start is called before the first frame update
        void Start()
        {
            Physics2D.queriesStartInColliders = false;
            player = GameObject.Find("Player");
            //enemy.GetComponent<EnemyHealth>().TakeDamage();
        }

        // Update is called once per frame
        void Update()
        {
            //Flipping the enemy
            if (player.transform.position.x < transform.position.x)
            {
                facingRight = !facingRight;
                transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            else
            {
                facingRight = true;
                transform.rotation = Quaternion.Euler(0, 0f, 0);
            }


            int layerMask = 11;

            layerMask = ~layerMask;

            //Raycast
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, distance, layerMask);
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.left, distance, layerMask);

                //Check, if raycast hit the player
                if ((hit.collider != null && hit.collider.tag == "Player") || (hit2.collider != null && hit2.collider.tag == "Player"))
                {
                    //Go into alert mode
                    shootTimer += Time.deltaTime;
                    Debug.DrawRay(transform.position, Vector2.left * distance, Color.red);
                    Debug.DrawRay(transform.position, Vector2.right * distance, Color.red);
                    //activate = 1;
                    animator.SetInteger("Activate", 1);
                    //alert = 1;
                    animator.SetInteger("Alert", 1);

                    //If player is in raycast for 1,5 second, shoot
                    if ((hit.collider != null && hit.collider.tag == "Player" && shootTimer > 0.7) || (hit2.collider != null && hit2.collider.tag == "Player" && shootTimer > 0.7))
                    {
                        shootingAudio.Play();
                        shoot();
                        shootTimer = 0;

                    }
                }
                //If no player on raycast, go to idle mode
                if ((hit.collider == null || hit.collider.tag != "Player") && (hit2.collider == null || hit2.collider.tag != "Player"))
                {
                    shootTimer = 0;
                    //alert = 0;
                    animator.SetInteger("Alert", 0);
                    //shooting = 0;
                    animator.SetInteger("Shooting", 0);
                    //activate = 0;
                    animator.SetInteger("Activate", 0);
                }

            

                //if()
                //{
                //    animator.SetBool("Dead", true);
                //}

        }

        //void spriteFlip()
        //{

            //Vector2 scale = transform.localScale;

            //if (player.transform.position.x > transform.position.x)
            //    scale.x = Mathf.Abs(scale.x);
            //else
            //{
            //    scale.x = Mathf.Abs(scale.x) * -1;
            //}

            //transform.localScale = scale;
        //}

        void shoot()
        {
            //shooting = 1;
            Instantiate(projectile, projectilePos.position, projectilePos.rotation);
            animator.SetInteger("Shooting", 1);
        }

    }
}
