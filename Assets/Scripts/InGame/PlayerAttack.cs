using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    bool _isAttack;

    GameObject collisionEnemy;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(_isAttack)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                collisionEnemy.GetComponent<EnemyControl>()._hp -= GetComponentInParent<PlayerControl>()._attackPower;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            _isAttack = true;

            collisionEnemy = collision.gameObject;
        }
    }
}
