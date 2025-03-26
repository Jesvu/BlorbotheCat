using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickuppableSBomb : MonoBehaviour
{
    private ParticleSystem particles;
    private AudioSource audioS;

    private void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        audioS = GetComponentInChildren<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && SuperBomb.superbombsLeft < 3)
        {
            SuperBomb.superbombsLeft++;
            DetachParticles();
            Destroy(gameObject);
        }


    }
    public void DetachParticles()
    {
        audioS.Play();
        particles.transform.parent = null;
        particles.Stop();

    }
}