using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using blorbothecat.States;

namespace Bluescreen.BlorboTheCat
{
    public class PlayerHealth : MonoBehaviour
    {
        public static int playerHealth = 3;
        public bool invincible = false;
        [SerializeField] private GameObject hitflash;
        [SerializeField] private GameObject splatter;
        [SerializeField] private Material hitMaterial;
        private Material defaultMaterial;
        [SerializeField] private float deathResetWait=1f;
        private Animator deathExtraAnimation;
        [SerializeField] private HUDHealth hudHealth;

        [SerializeField] private AudioSource one;
        [SerializeField] private AudioSource two;
        [SerializeField] private AudioSource tre;
        [SerializeField] private AudioSource four;
        [SerializeField] private AudioSource ee;


        void Awake()
        {
            hudHealth = GameObject.Find("health").GetComponent<HUDHealth>();
            defaultMaterial = GetComponent<SpriteRenderer>().material;
            deathExtraAnimation = GameObject.Find("DeathBG Image").GetComponent<Animator>();
            LatestCompletionInfo.enemiesKilled = 0;
            LatestCompletionInfo.damageTaken = 0;
        }

        // this is called from the Damager script on contact
        public void TakeDamage(int damage)
        {
            if (!invincible) {
                invincible = true;
                LatestCompletionInfo.damageTaken += damage;
                playerHealth -= damage;
                CameraShake.Instance.Shake(3f, 0.2f);
                int soundchance = Random.Range(1, 2002);
                DamageSound(soundchance);
                if (playerHealth > 0)
                {
                    invincible = true;
                    Instantiate(hitflash, transform.position, transform.rotation, transform);
                    Instantiate(splatter, transform.position, transform.rotation);
                    hudHealth.DamageParticles();

                    StartCoroutine(IFrames());
                    StartCoroutine(Flashing());
                    
                }
                else
                {
                    invincible = true;
                    hudHealth.DamageParticles();
                    Instantiate(hitflash, transform.position, transform.rotation, transform);
                    Instantiate(splatter, transform.position, transform.rotation);
                    StartCoroutine(Death());
                    //GameStateManager.Instance.Go(StateType.GameOver);
                }
            }
        }

        void DamageSound(int roll)
        {

            
            if(roll == 2001) { ee.Play(); }
            else if (roll <= 2000 && roll > 1500) { four.Play(); }
            else if (roll <= 1500 && roll > 1000) { tre.Play(); }
            else if (roll <= 1000 && roll > 500) { two.Play(); }
            else if (roll <= 500 && roll > 0) { one.Play(); }

        }

        void Update()
        {
            //death test hotkey
            /*if(Input.GetKeyDown(KeyCode.End))
            {
                StartCoroutine(Death());
            }*/
        }
        IEnumerator Death()
        {
            // death animation or some **** right here
           
            PlayerController.movementFrozen = true;
            GetComponent<PlayerController>().enabled = false;
            PlayerActions.actionsFrozen = true;
            GetComponent<PlayerActions>().enabled = false;
            deathExtraAnimation.Play("DeathBG");
            GetComponent<SpriteRenderer>().sortingLayerName = "UI";
            GetComponent<SpriteRenderer>().sortingOrder = 201;
            GetComponent<Animator>().SetBool("isDead",true);
            GetComponent<Animator>().Play("Player_death");
            yield return new WaitForSeconds(deathResetWait);
            GameObject loader = GameObject.Find("LevelLoader");
            loader.GetComponent<LevelLoader>().ChangeScene(-1);
        }

        // makes player invincible for a second after taking damage
        IEnumerator IFrames()
        {
            yield return new WaitForSeconds(1);
            invincible = false;
            yield break;
        }

        // scuffed flash to visualize the i-frames
        IEnumerator Flashing()
        {
            for (int i = 0; i <= 10; i++)
            {
                yield return new WaitForSeconds(0.1f);
                gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
            }

            //Hannes testing flashing white instead of flashing visibility :P please don't mind

            /*gameObject.GetComponent<SpriteRenderer>().material = hitMaterial;
            yield return new WaitForSeconds(0.15f);
            gameObject.GetComponent<SpriteRenderer>().material = defaultMaterial;
            yield return new WaitForSeconds(0.15f);
            gameObject.GetComponent<SpriteRenderer>().material = hitMaterial;
            yield return new WaitForSeconds(0.15f);
            gameObject.GetComponent<SpriteRenderer>().material = defaultMaterial;*/
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield break;
        }
    }
}
