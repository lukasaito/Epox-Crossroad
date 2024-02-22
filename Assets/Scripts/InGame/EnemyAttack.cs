using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject enemyBullet;
    float count = 5;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            count += Time.deltaTime;

            if (count >= 5)
            {
                Instantiate(enemyBullet, GetComponentInParent<Transform>().position, Quaternion.identity);

                count = 0;
            }
        }
    }
}
