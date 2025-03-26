using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class Projectile : MonoBehaviour
    {

        public float speed;

        private Transform player;
        private Vector2 target;
        public Rigidbody2D rb;

        [SerializeField] public int damage = 1;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

            target = new Vector2(player.position.x, player.position.y);

            rb.velocity = transform.right * speed;
        }

        // Update is called once per frame
        void Update()
        {
            //Some old stuff
            //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            //if(transform.position.x == target.x && transform.position.y == target.y)
            //{
            //    DestroyProjectile();
            //}
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //If hit player, do damage and destroy projectile
            if(other.CompareTag("Player"))
            {
                other.GetComponent<PlayerHealth>().TakeDamage(damage);
                DestroyProjectile();
            }
            if (other.CompareTag("Wall"))
            {
                DestroyProjectile();
            }
            if (other.CompareTag("Breakable"))
            {
                DestroyProjectile();
            }
        }
        void DestroyProjectile()
        {
            Destroy(gameObject);
        }
    }
}