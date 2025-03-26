using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBlock : MonoBehaviour
{
    public GameObject enemyAmount;
    private TextMesh enemyAmountText;
    [SerializeField] private GameObject puff;

    void Start()
    {
        enemyAmountText = enemyAmount.GetComponent<TextMesh>();
    }

    void Update()
    {
        int enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemies == 0)
        {
            Instantiate(puff, transform.position, transform.rotation);
             Destroy(gameObject);
        }

        enemyAmountText.text = enemies.ToString();
    }
}