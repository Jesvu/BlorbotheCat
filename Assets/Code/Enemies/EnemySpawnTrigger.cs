using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluescreen.BlorboTheCat
{
    public class EnemySpawnTrigger : MonoBehaviour
    {

        public Transform triggerArea;
        public GameObject enemy;


        // Start is called before the first frame update
        void Start()
        {

        }

        private void Awake()
        {
            triggerArea.parent = null;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnTriggerEnter2D(Collider2D triggercollider)
        {
            if (triggercollider.gameObject.CompareTag("Player"))
            {
                Invoke("Spawn", 0);

            }
        }

        public void Spawn()
        {
            Instantiate(enemy, triggerArea);

        }
    }
}
