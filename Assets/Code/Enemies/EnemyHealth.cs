using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private string type;
        [SerializeField] private int health = 3;
        private bool invincible = false;
        [SerializeField] private GameObject hitflash;
        [SerializeField] private GameObject shield;
        public Animator animator;
        private Damager dmg;
        [SerializeField] private GameObject puffParticles;
        [SerializeField] private GameObject bloodParticles;
        [SerializeField] private GameObject audiosourceObject;
        [SerializeField] private AudioSource damageAudioOne;
        [SerializeField] private AudioSource damageAudioTwo;
        [SerializeField] private AudioSource shieldAudioOne;
        [SerializeField] private AudioSource shieldAudioTwo;
        [SerializeField] private AudioSource deathAudio;
        private Rigidbody2D rb;


        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            dmg = GetComponent<Damager>();
            if (health < 2)
            {
                Destroy(shield);
            }
        }

        // this is called from the Damager script on contact
        public void TakeDamage(int damage)
        {
            if (!invincible) {
                health = health - damage;

                if (health > 0)
                {
                    int soundchanceDmg = Random.Range(1, 3);
                    if (soundchanceDmg == 1) { damageAudioOne.Play(); }
                    else { damageAudioTwo.Play(); }
                    
                    Instantiate(hitflash, transform.position, transform.rotation, transform);
                    Instantiate(bloodParticles, transform.position, transform.rotation);
                    if (health < 2)
                    {
                        int soundchanceShield = Random.Range(1, 3);
                        if (soundchanceShield == 1) { shieldAudioOne.Play(); }
                        else { shieldAudioTwo.Play(); }
                        Destroy(shield);
                    }
                    invincible = true;
                    StartCoroutine(IFrames());
                    StartCoroutine(Flashing());
                }
                else
                {
                    Death();
                }
            }
        }
        
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.End))
            {
                Death();
            }
        }
        // death stuff here later
        private void Death()
        {
            dmg.enabled=false;
            audiosourceObject.transform.parent = null;
            deathAudio.Play();
            Instantiate(hitflash, transform.position, transform.rotation);
            Instantiate(puffParticles, transform.position, transform.rotation);
            Instantiate(bloodParticles, transform.position, transform.rotation);
            LatestCompletionInfo.enemiesKilled += 1;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            
            //checks the enemytype and does stuff depending on that
            if(type == "Stationary") { animator.Play("StationaryEnemy_Death"); }
            else if (type == "Waypoint")
            {
                if (gameObject.GetComponent<WaypointEnemy>().facingRight == true)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                
                gameObject.GetComponent<WaypointEnemy>().enabled = false;
                animator.Play("WaypointEnemy_Death");
            }
            else if (type == "Turning")
            {
                if (gameObject.GetComponent<TurningEnemy>().facingRight == true)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                gameObject.GetComponent<TurningEnemy>().enabled = false;
                animator.Play("WaypointEnemy_Death");
            }
            else if (type == "Chaser")
            {
                if (gameObject.GetComponent<EnemyChase>().facingRight == true)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                gameObject.GetComponent<EnemyChase>().enabled = false;
                animator.Play("ChaseEnemy_Death");
            }
            rb.velocity = Vector2.zero;
            Destroy(gameObject, 0.6f);
        }

        // makes enemy invincible for a bit after taking damage
        IEnumerator IFrames()
        {
            yield return new WaitForSeconds(0.4f);
            invincible = false;
            yield break;
        }

        // scuffed flash to visualize the i-frames
        IEnumerator Flashing()
        {
            for (int i = 0; i <= 4; i++)
            {
                yield return new WaitForSeconds(0.1f);
                gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
            }
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield break;
        }
    }
}