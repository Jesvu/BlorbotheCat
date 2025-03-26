using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.WSA; //this stuff was making unity throw out errors when building :P, so I commented it out for now - Hannes

namespace Bluescreen.BlorboTheCat
{
    public class EnemyChase : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 1.5f;
        Rigidbody2D rb;
        Transform target;
        Vector2 moveDirection;
        private bool chasemessagesent = false;
        [SerializeField] private Color idleColor;
        [SerializeField] private Color huntColor;
        [SerializeField] private AudioSource alertOne;
        [SerializeField] private AudioSource alertTwo;
        [SerializeField] private AudioSource alertThree;
        [SerializeField] private AudioSource alertFour;
        [SerializeField] private Animator alert;
        public bool facingRight;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            GetComponent<SpriteRenderer>().color = idleColor;
        }

        void Start()
        {
            target = GameObject.Find("Player").transform;
            GetComponent<Animator>().Play("ChaseEnemy_Static");
        }

        private void Update()
        {
            if (target)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //rb.rotation = angle;
                moveDirection = direction;
                
                if (transform.position.x <= target.transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    facingRight = true;
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    facingRight = false;
                }
            }
        }

        private void FixedUpdate()
        {
            if (target)
            {
                if (Vector2.Distance(transform.position, target.transform.position) < 5) {
                    //GetComponent<SpriteRenderer>().enabled = true;

                    if (LevelLoader.firstActionDone)
                    {
                        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;

                        if (!chasemessagesent)
                        {
                            alert.Play("chasealert");
                            GetComponent<SpriteRenderer>().color = huntColor;
                            int soundChance = Random.Range(1, 5);
                            if (soundChance == 1) { alertOne.Play(); }
                            else if (soundChance == 2) { alertTwo.Play(); }
                            else if (soundChance == 3) { alertThree.Play(); }
                            else if (soundChance == 4) { alertFour.Play(); }

                            GetComponent<Animator>().Play("ChaseEnemy_MoveRight");
                            Debug.Log("chaser is on the move!!");
                            chasemessagesent = true;
                        }
                    }
                }
                else if (Vector2.Distance(transform.position, target.transform.position) > 15)
                {
                    rb.velocity = Vector2.zero;
                    //GetComponent<SpriteRenderer>().enabled = false;

                    if (chasemessagesent)
                    {
                        GetComponent<SpriteRenderer>().color = idleColor;
                        GetComponent<Animator>().Play("ChaseEnemy_Static");
                        Debug.Log("chaser has stopped...");
                        chasemessagesent = false;
                    }
                }
            }
        }

        public void HideSprite()
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
