using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastBossControl : MonoBehaviour
{
    SpriteRenderer _sr;
    BoxCollider2D _cl2d;
    Animator animator;

    public bool dead;

    private bool _isAttack;
    private float _attackTime;
    [SerializeField]
    private float attackMaxTime;
    private float _coolTime;

    public GameObject player;

    public GameObject bullet;
    public GameObject bigBullet;

    public float _hp;
    float _hpMAX;

    public GameObject attackP;

    bool _invincible;
    float _invincibleCoolTime;
    float _colorTime;

    public GameObject explosionEffect;
    bool exEffect;

    float phase;
    float phase_0;
    float phase_1;

    float stopTime;

    public GameObject a1;
    public GameObject a2;
    public GameObject a3;
    public Transform _transform;

    bool tracking;
    Vector3 trackingP;
    float trackingCount;

    float attackCount;

    public GameObject fire;
    public GameObject notice;

    public float deadTime;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _cl2d = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        _transform = this.transform;
        _hpMAX = _hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if(_hp <= 0)
            {

                _sr.enabled = false;
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                _cl2d.enabled = false;

                if (!exEffect)
                {
                    Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
                    exEffect = true;
                    dead = true;
                }
            }
            else if(_hp <= _hpMAX * 0.5)
            {
                phase = 1;
            }
            else
            {
                phase = 0;
            }

            switch (phase)
            {
                case 0:
                    switch (phase_0)
                    {
                        case 0:
                            this.transform.position = Vector2.MoveTowards(this.transform.position, a1.transform.position, 2 * Time.deltaTime);
                            
                            if(this.transform.position == a1.transform.position)
                            {

                                phase_0++;
                            }
                            break;

                        case 1:
                            this.transform.position = Vector2.MoveTowards(this.transform.position, a2.transform.position, 2 * Time.deltaTime);
                            if (this.transform.position == a2.transform.position)
                            {

                                phase_0++;
                            }
                            break;

                        case 2:
                            this.transform.position = Vector2.MoveTowards(this.transform.position, _transform.position, 2 * Time.deltaTime);
                            if (this.transform.position == _transform.position)
                            {

                                phase_0++;
                            }
                            break;

                        case 3:
                            stopTime += Time.deltaTime;

                            if (stopTime >= 3)
                            {
                                if(!tracking)
                                {
                                    trackingP = player.transform.position;

                                    tracking = true;
                                }

                                trackingCount += Time.deltaTime;

                                if (trackingCount <= 1)
                                {
                                    this.transform.position = Vector2.MoveTowards(this.transform.position, trackingP, 4 * Time.deltaTime);
                                }
                                else
                                {
                                    trackingCount = 0;

                                    phase_0++;

                                    tracking = false;

                                    stopTime = 0;
                                }
                                
                            }
                            break;

                        case 4:
                            this.transform.position = Vector2.MoveTowards(this.transform.position, _transform.position, 2 * Time.deltaTime);
                            if (this.transform.position == _transform.position)
                            {

                                phase_0++;
                            }
                            break;

                        case 5:
                            stopTime += Time.deltaTime;

                            if (stopTime >= 3)
                            {
                                Attack();
                                if (attackCount == 1)
                                {
                                    phase_0++;
                                    attackCount = 0;
                                    stopTime = 0;
                                }
                            }
                            break;

                        case 6:
                            this.transform.position = Vector2.MoveTowards(this.transform.position, a1.transform.position, 2 * Time.deltaTime);
                            if (this.transform.position == a1.transform.position)
                            {

                                phase_0++;
                            }
                            break;

                        case 7:
                            stopTime += Time.deltaTime;

                            if (stopTime >= 3)
                            {
                                if (!tracking)
                                {
                                    trackingP = player.transform.position;

                                    tracking = true;
                                }

                                trackingCount += Time.deltaTime;

                                if (trackingCount <= 1)
                                {
                                    this.transform.position = Vector2.MoveTowards(this.transform.position, trackingP, 4 * Time.deltaTime);
                                }
                                else
                                {
                                    trackingCount = 0;

                                    phase_0++;

                                    tracking = false;
                                    stopTime = 0;
                                }

                            }
                            break;

                        case 8:
                            this.transform.position = Vector2.MoveTowards(this.transform.position, _transform.position, 2 * Time.deltaTime);
                            if (this.transform.position == _transform.position)
                            {

                                phase_0 = 0;
                            }
                            break;
                    }
                    break;

                case 1:
                    switch(phase_1)
                    {
                        case 0:
                            this.transform.position = Vector2.MoveTowards(this.transform.position, _transform.position, 2 * Time.deltaTime);
                            if (this.transform.position == _transform.position)
                            {

                                phase_1++;
                            }
                            break;

                        case 1:
                            this.transform.position = Vector2.MoveTowards(this.transform.position, a3.transform.position, 2 * Time.deltaTime);
                            if (this.transform.position == a3.transform.position)
                            {

                                phase_1++;
                            }
                            break;

                        case 2:
                            BigAttack();
                            if (attackCount == 1)
                            {
                                phase_1++;
                                attackCount = 0;
                                stopTime = 0;
                            }
                            break;

                        case 3:
                            Fire();
                            if (attackCount == 3)
                            {
                                phase_1++;
                                attackCount = 0;
                                stopTime = 0;
                                tracking = false;
                            }
                            break;

                        case 4:
                            stopTime += Time.deltaTime;

                            if (stopTime >= 3)
                            {
                                this.transform.position = Vector2.MoveTowards(this.transform.position, _transform.position, 2 * Time.deltaTime);
                                if (this.transform.position == _transform.position)
                                {
                                    phase_1++;
                                    stopTime = 0;
                                }
                            }
                            break;

                        case 5:
                            stopTime += Time.deltaTime;

                            if (stopTime >= 3)
                            {
                                this.transform.position = Vector2.MoveTowards(this.transform.position, a2.transform.position, 2 * Time.deltaTime);
                                if (this.transform.position == a2.transform.position)
                                {

                                    phase_1++;
                                    stopTime = 0;
                                }
                            }
                            break;

                        case 6:
                            stopTime += Time.deltaTime;

                            if (stopTime >= 3)
                            {
                                this.transform.position = Vector2.MoveTowards(this.transform.position, a1.transform.position, 2 * Time.deltaTime);
                                if (this.transform.position == a1.transform.position)
                                {
                                    phase_1++;
                                    stopTime = 0;
                                }
                            }
                            break;

                        case 7:
                            stopTime += Time.deltaTime;

                            if (stopTime >= 3)
                            {
                                if (!tracking)
                                {
                                    trackingP = player.transform.position;

                                    tracking = true;
                                }

                                trackingCount += Time.deltaTime;

                                if (trackingCount <= 1)
                                {
                                    this.transform.position = Vector2.MoveTowards(this.transform.position, trackingP, 4 * Time.deltaTime);
                                }
                                else
                                {
                                    trackingCount = 0;

                                    phase_1++;

                                    tracking = false;
                                    stopTime = 0;
                                }

                            }
                            break;

                        case 8:
                            this.transform.position = Vector2.MoveTowards(this.transform.position, _transform.position, 2 * Time.deltaTime);
                            if (this.transform.position == _transform.position)
                            {

                                phase_1++;
                            }
                            break;

                        case 9:
                            this.transform.position = Vector2.MoveTowards(this.transform.position, a2.transform.position, 2 * Time.deltaTime);
                            if (this.transform.position == a2.transform.position)
                            {

                                phase_1 = 0;
                            }
                            break;
                    }
                    break;
            }

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
        else
        {

            deadTime += Time.deltaTime;

            if (deadTime >= 3)
            {
                SceneManager.LoadScene("GameClear");
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
                animator.SetBool("Attack", false);
                Instantiate(bullet, attackP.transform.position, Quaternion.identity);
                attackCount++;

                _isAttack = false;

                _attackTime = 0;
            }
        }
        else
        {
            _coolTime = _coolTime + Time.deltaTime;

            if (_coolTime >= 3.0f)
            {
                _isAttack = true;
                animator.SetBool("Attack", true);

                _coolTime = 0;
            }
        }
    }

    void BigAttack()
    {
        if (_isAttack)
        {
            _attackTime = _attackTime + Time.deltaTime;

            if (attackMaxTime < _attackTime)
            {
                animator.SetBool("Attack", false);
                Instantiate(bigBullet, attackP.transform.position, Quaternion.identity);
                attackCount++;

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
            else if(_coolTime >= 1)
            {

                animator.SetBool("Attack", true);
            }
        }
    }

    void Fire()
    {
        if (_isAttack)
        {
            _attackTime = _attackTime + Time.deltaTime;

            if (attackMaxTime < _attackTime)
            {


                animator.SetBool("Attack", false);
                Instantiate(fire, new Vector3(trackingP.x, -3.1f, 0), Quaternion.identity);
                tracking = false;
                attackCount++;

                _isAttack = false;

                _attackTime = 0;
            }
        }
        else
        {
            _coolTime = _coolTime + Time.deltaTime;

            if (_coolTime >= 4.0f)
            {

                if (!tracking)
                {
                    trackingP = player.transform.position;

                    tracking = true;
                }

                _isAttack = true;
                animator.SetBool("Attack", true);
                Instantiate(notice, new Vector3(trackingP.x,-3.1f,0) , Quaternion.identity);

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
