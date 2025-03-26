using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class TurningEnemy : MonoBehaviour
    {
        [SerializeField] private float speed = 2;
        [SerializeField] private string startDirection = "right";
        private Vector3 direction;
        private Rigidbody2D rb;
        public bool facingRight;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            switch (startDirection)
            {
                case "right":
                    direction = new Vector3(1,0,0);
                    facingRight = true;
                    break;
                case "left":
                    direction = new Vector3(-1,0,0);
                    transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
                    facingRight = false;
                    break;
                case "up":
                    direction = new Vector3(0,1,0);
                    facingRight = true;
                    break;
                case "down":
                    direction = new Vector3(0,-1,0);
                    transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
                    facingRight = false;
                    break;
                default:
                    direction = Vector3.zero;
                    facingRight = true;
                    break;
            }
        }

        void FixedUpdate()
        {
            // move the enemy
            rb.velocity = direction * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // reverse direction when hitting a wall or a breakable block
            if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Breakable") || other.gameObject.CompareTag("Water"))
            {
                direction *= -1;
                transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
                facingRight = !facingRight;
            }
        }
    }
}