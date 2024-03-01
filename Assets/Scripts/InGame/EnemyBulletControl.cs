using UnityEngine;

public class EnemyBulletControl : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private Animator _animator;
    public GameObject player;

    private int changeFireBullet;

    Vector2 vector2;

    float count;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        player = GameObject.Find("Player_Test");
        vector2 = this.player.transform.position - transform.position;
    }

    void Update()
    {
        changeFireBullet = FindObjectOfType<PlayerControl>()._modelNumber;
        _animator.SetInteger("ChangeFireBullet", changeFireBullet);

        _rb2d.velocity = vector2.normalized * 4;

        count += Time.deltaTime;

        if(count >= 5)
        {
            Destroy(gameObject);
        }
    }
}