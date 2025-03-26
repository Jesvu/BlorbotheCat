using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] float destroyDelay = 0.5f;
    [SerializeField] float colliderDelay = 0.05f;

    void Start()
    {
        // if object is an explosion beam, disable the collider after a small delay
        if (gameObject.tag == "Explosion")
        {
            StartCoroutine(DisableCollider());
        }

        Destroy(gameObject, destroyDelay);
    }

    IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(colliderDelay);
        GetComponent<BoxCollider2D>().enabled = false;
        yield break;
    }
}
