using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2EnemyBullet : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    public GameObject player;

    Vector2 vector2;

    float count;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();

        player = GameObject.Find("Player_Test");
        vector2 = this.player.transform.position - transform.position;
    }

    void Update()
    {
        _rb2d.velocity = vector2.normalized * 3;

        count += Time.deltaTime;

        if (count >= 5)
        {
            Destroy(gameObject);
        }
    }
}
