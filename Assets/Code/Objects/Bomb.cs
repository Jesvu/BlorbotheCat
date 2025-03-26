using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionMid;
    public GameObject explosionWave;
    public LayerMask wallLayer;
    public LayerMask breakableLayer;
    [SerializeField] private float bombTimer = 2f;
    [SerializeField] private int explosionLength = 2;
    private bool exploded = false;
    private GameObject explosion;
    private Animator anim;
    public static int bombCount = 0;

    public GameObject pile;
    public GameObject destructionEffect;
    private AudioSource aS;
    private ParticleSystem particles;
    [SerializeField] private GameObject puffParticles;
    [SerializeField] private GameObject extra;

    void Start()
    {
        Invoke("Explode", bombTimer);
        bombCount++;
        aS = GetComponentInChildren<AudioSource>();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    void Explode()
    {
        // the explosion for the tile the bomb is on currently
        Instantiate(explosionMid, transform.position, Quaternion.identity);

        // explosion to each direction
        StartCoroutine(CreateExplosion(Vector2.up, "up"));
        StartCoroutine(CreateExplosion(Vector2.down, "down"));
        StartCoroutine(CreateExplosion(Vector2.right, "right"));
        StartCoroutine(CreateExplosion(Vector2.left, "left"));
        
        exploded = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        bombCount--;
        DetachParticles();
        Instantiate(puffParticles, transform.position, transform.rotation);
        Instantiate(extra, transform.position, transform.rotation);
        Destroy(gameObject, 0.5f);
        CameraShake.Instance.Shake(3f, 0.2f);
        aS.Play();

        // removing the light (lol)
        try {Destroy(gameObject.transform.GetChild(0).gameObject);}
        catch (UnityException e) {UnityException idkhowelsetogetridofthenotusedwarning = e;}
    }

    // spawns the explosion beams
    IEnumerator CreateExplosion(Vector2 direction, string dir) 
    {
        for (int i = 1; i <= explosionLength; i++)
        {
            // raycast check to see if there's a wall in the way
            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction, i, wallLayer | breakableLayer); 

            // if nothing unbreakable in the way, create an explosion beam with the right direction
            if (!hit.collider)
            {
                switch(dir)
                {
                    case "up":
                        explosion = (GameObject)Instantiate(explosionWave, (Vector2)transform.position + (i * direction), Quaternion.Euler(0, 0, 270));
                        break;
                    case "down":
                        explosion = (GameObject)Instantiate(explosionWave, (Vector2)transform.position + (i * direction), Quaternion.Euler(0, 0, 90));
                        break;
                    case "right":
                        explosion = (GameObject)Instantiate(explosionWave, (Vector2)transform.position + (i * direction), Quaternion.Euler(0, 0, 180));
                        break;
                    case "left":
                        explosion = (GameObject)Instantiate(explosionWave, (Vector2)transform.position + (i * direction), Quaternion.Euler(0, 0, 0));
                        break;
                    default:
                        break;
                }
                anim = explosion.GetComponent<Animator>();
                anim.Play("ExplosionWave");
            }

            // if breakable wall in the way, break it and stop the beam there
            // (i wish i didnt have to have all of this twice but unity was being difficult (probable skill issue))
            else if (hit.collider.tag == "Breakable")
            {
                switch(dir)
                {
                    case "up":
                        explosion = (GameObject)Instantiate(explosionWave, (Vector2)transform.position + (i * direction), Quaternion.Euler(0, 0, 270));
                        break;
                    case "down":
                        explosion = (GameObject)Instantiate(explosionWave, (Vector2)transform.position + (i * direction), Quaternion.Euler(0, 0, 90));
                        break;
                    case "right":
                        explosion = (GameObject)Instantiate(explosionWave, (Vector2)transform.position + (i * direction), Quaternion.Euler(0, 0, 180));
                        break;
                    case "left":
                        explosion = (GameObject)Instantiate(explosionWave, (Vector2)transform.position + (i * direction), Quaternion.Euler(0, 0, 0));
                        break;
                    default:
                        break;
                }
                explosion.GetComponent<BoxCollider2D>().enabled = false;
                anim = explosion.GetComponent<Animator>();
                anim.Play("ExplosionWave");

                Instantiate(pile, hit.transform.position, hit.transform.rotation);
                Instantiate(destructionEffect, hit.transform.position, hit.transform.rotation);
                Destroy(hit.transform.gameObject);
                break;
            }
            
            // if unbreakable wall in the way, stop the beam
            else
            {
                break;
            }
            
            // small delay here between each explosion tile to make the beam appear gradually
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
        {
            // if an explosion hits the bomb, cancel the countdown and explode it immediately
            if (!exploded && (other.CompareTag("Explosion")))
            {
                CancelInvoke("Explode");
                Explode();
            }
        }
    public void DetachParticles()
    {
        particles.transform.parent = null;
        particles.Stop();

    }
}
