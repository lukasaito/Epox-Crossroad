using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossControl : MonoBehaviour
{
    SpriteRenderer _sr;

    private bool _isAttack;
    private float _attackTime;
    [SerializeField]
    private float attackMaxTime;
    private float _coolTime;

    public GameObject player;

    public GameObject bullet;

    public float _hp;
    float _hpMAX;

    public GameObject attackP;

    bool _invincible;
    float _invincibleCoolTime;
    float _colorTime;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();

        if (_invincible)
        {
            _invincibleCoolTime += Time.deltaTime;
            _colorTime += Time.deltaTime;

            if (_colorTime >= 0.1f)
            {
                _sr.color = new Color(255f, 255f, 255f);
            }
            else
            {
                _sr.color = new Color(255f, 0, 0);
            }

            if (_invincibleCoolTime >= 0.3f)
            {
                _invincible = false;
                _invincibleCoolTime = 0;
                _colorTime = 0;
            }
        }
    }

    void Attack()
    {
        if (_isAttack)
        {
            _attackTime = _attackTime + Time.deltaTime;

            if (attackMaxTime < _attackTime)
            {
                Instantiate(bullet, attackP.transform.position, Quaternion.identity);

                _isAttack = false;

                _attackTime = 0;
            }
        }
        else
        {
            _coolTime = _coolTime + Time.deltaTime;

            if (_coolTime >= 4.0f)
            {
                _isAttack = true;

                _coolTime = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AA"))
        {
            if (!_invincible)
            {
                _hp -= GameObject.Find("Player_Test").GetComponent<PlayerControl>()._attackPower;

                _invincible = true;
            }
        }
    }
}
