using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MiddleBossControl : MonoBehaviour
{
    SpriteRenderer _sr;
    BoxCollider2D _cl2d;

    private bool _isAttack;
    private float _attackTime;
    [SerializeField]
    private float attackMaxTime;
    private float _coolTime;

    public GameObject player;

    public GameObject bullet;
    public GameObject bigBullet;
    public GameObject fire;

    private Animator animator;

    public float _hp;
    float _hpMAX;

    GameObject attackP;
    public GameObject attackL;
    public GameObject attackR;
    public GameObject fireP;

    float phase;
    float phase_0;
    float phase_1;
    float phase_2;

    float stopTime;

    float attackCount;

    public GameObject warpPoint_1;
    bool warp_1;
    bool warp_1Effect;

    public GameObject warpPoint_2;
    bool warp_2;
    bool warp_2Effect;

    public GameObject warpPoint_3;
    bool warp_3;
    bool warp_3Effect;

    public GameObject warpPoint_4;
    bool warp_4;
    bool warp_4Effect;

    public GameObject warpPoint_5;
    bool warp_5;
    bool warp_5Effect;

    float warpDeray;
    public GameObject warpEffect;

    public GameObject explosionEffect;
    bool exEffect;

    bool impulse;
    public GameObject stoneSpawn;

    // Start is called before the first frame update
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _cl2d = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        _hpMAX = _hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hp <= 0)
        {
            _sr.enabled = false;
            _cl2d.enabled = false;

            if (!exEffect)
            {
                Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
                exEffect = true;
            }
        }
        else if (_hp <= _hpMAX * 0.3)
        {
            phase = 2;
        }
        else if (_hp <= _hpMAX * 0.6)
        {
            phase = 1;

            if(!impulse)
            {
                var impulseSorce = GetComponent<CinemachineImpulseSource>();
                impulseSorce.GenerateImpulse();

                stoneSpawn.SetActive(true);

                impulse = true;
            }
        }
        else
        {
            phase = 0;
        }

        switch(phase)
        {
            case 0:
                switch(phase_0)
                {
                    case 0:
                        Attack();
                        if(attackCount == 3)
                        {
                            phase_0++;
                            attackCount = 0;
                        }
                        break;

                    case 1:
                        stopTime += Time.deltaTime;

                        if (stopTime >= 3)
                        {
                            if (!warp_5Effect)
                            {
                                Instantiate(warpEffect, warpPoint_5.transform.position, Quaternion.identity);
                                warp_5Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_5.transform.position;
                                warpDeray = 0;

                                warp_5Effect = false;
                                phase_0++;

                                stopTime = 0;
                            }
                        }
                        break;

                    case 2:
                        stopTime += Time.deltaTime;

                        if(stopTime >= 3)
                        {
                            if (!warp_2Effect)
                            {
                                Instantiate(warpEffect, warpPoint_2.transform.position, Quaternion.identity);
                                warp_2Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_2.transform.position;
                                warpDeray = 0;

                                warp_2Effect = false;
                                phase_0++;

                                stopTime = 0;
                            }
                        }
                        break;

                    case 3:
                        Attack();
                        if (attackCount == 3)
                        {
                            phase_0++;
                            attackCount = 0;
                        }
                        break;

                    case 4:
                        stopTime += Time.deltaTime;

                        if (stopTime >= 3)
                        {
                            if (!warp_5Effect)
                            {
                                Instantiate(warpEffect, warpPoint_5.transform.position, Quaternion.identity);
                                warp_5Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_5.transform.position;
                                warpDeray = 0;

                                warp_5Effect = false;
                                phase_0++;

                                stopTime = 0;
                            }
                        }
                        break;

                    case 5:
                        stopTime += Time.deltaTime;

                        if (stopTime >= 3)
                        {
                            if (!warp_1Effect)
                            {
                                Instantiate(warpEffect, warpPoint_1.transform.position, Quaternion.identity);
                                warp_1Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_1.transform.position;
                                warpDeray = 0;

                                warp_1Effect = false;
                                phase_0++;

                                stopTime = 0;
                            }
                        }
                        break;

                    case 6:
                        stopTime += Time.deltaTime;

                        if (stopTime >= 3)
                        {
                            if (!warp_5Effect)
                            {
                                Instantiate(warpEffect, warpPoint_5.transform.position, Quaternion.identity);
                                warp_5Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_5.transform.position;
                                warpDeray = 0;

                                warp_5Effect = false;
                                phase_0++;

                                stopTime = 0;
                            }
                        }
                        break;

                    case 7:
                        Attack();
                        if (attackCount == 3)
                        {
                            phase_0++;
                            attackCount = 0;
                        }
                        break;

                    case 8:
                        stopTime += Time.deltaTime;

                        if (stopTime >= 3)
                        {
                            if (!warp_2Effect)
                            {
                                Instantiate(warpEffect, warpPoint_2.transform.position, Quaternion.identity);
                                warp_2Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_2.transform.position;
                                warpDeray = 0;

                                warp_2Effect = false;
                                phase_0++;

                                stopTime = 0;
                            }
                        }
                        break;

                    case 9:
                        stopTime += Time.deltaTime;

                        if (stopTime >= 3)
                        {
                            if (!warp_5Effect)
                            {
                                Instantiate(warpEffect, warpPoint_5.transform.position, Quaternion.identity);
                                warp_5Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_5.transform.position;
                                warpDeray = 0;

                                warp_5Effect = false;
                                phase_0++;

                                stopTime = 0;
                            }
                        }
                        break;

                    case 10:
                        stopTime += Time.deltaTime;

                        if (stopTime >= 3)
                        {
                            if (!warp_1Effect)
                            {
                                Instantiate(warpEffect, warpPoint_1.transform.position, Quaternion.identity);
                                warp_1Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_1.transform.position;
                                warpDeray = 0;

                                warp_1Effect = false;
                                phase_0 = 0;

                                stopTime = 0;
                            }
                        }
                        break;
                }
                break;

            case 1:
                switch(phase_1)
                {
                    case 0:
                        attackCount = 0;

                        if (!warp_1Effect)
                        {
                            Instantiate(warpEffect, warpPoint_1.transform.position, Quaternion.identity);
                            warp_1Effect = true;
                        }

                        warpDeray += Time.deltaTime;

                        _sr.enabled = false;
                        _cl2d.enabled = false;

                        if (warpDeray >= 1)
                        {
                            _sr.enabled = true;
                            _cl2d.enabled = true;

                            this.transform.position = warpPoint_1.transform.position;
                            warpDeray = 0;

                            warp_1Effect = false;
                            phase_1++;
                        }
                        break;

                    case 1:
                        BigAttack();
                        if (attackCount == 1)
                        {
                            phase_1++;
                            attackCount = 0;
                        }
                        break;

                    case 2:
                        stopTime += Time.deltaTime;

                        if (stopTime >= 3)
                        {
                            if (!warp_4Effect)
                            {
                                Instantiate(warpEffect, warpPoint_4.transform.position, Quaternion.identity);
                                warp_4Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_4.transform.position;
                                warpDeray = 0;

                                warp_4Effect = false;
                                phase_1++;

                                stopTime = 0;
                            }
                        }
                        break;

                    case 3:
                        Attack();
                        if (attackCount == 3)
                        {
                            phase_1++;
                            attackCount = 0;
                        }
                        break;

                    case 4:
                        stopTime += Time.deltaTime;

                        if (stopTime >= 3)
                        {
                            if (!warp_3Effect)
                            {
                                Instantiate(warpEffect, warpPoint_3.transform.position, Quaternion.identity);
                                warp_3Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_3.transform.position;
                                warpDeray = 0;

                                warp_3Effect = false;
                                phase_1++;

                                stopTime = 0;
                            }
                        }
                        break;

                    case 5:
                        Attack();
                        if (attackCount == 3)
                        {
                            phase_1++;
                            attackCount = 0;
                        }
                        break;

                    case 6:
                        if(player.transform.position.y <= 0)
                        {
                            stopTime += Time.deltaTime;

                            if (stopTime >= 3)
                            {
                                if (!warp_1Effect)
                                {
                                    Instantiate(warpEffect, warpPoint_1.transform.position, Quaternion.identity);
                                    warp_1Effect = true;
                                }

                                warpDeray += Time.deltaTime;

                                _sr.enabled = false;
                                _cl2d.enabled = false;

                                if (warpDeray >= 1)
                                {
                                    _sr.enabled = true;
                                    _cl2d.enabled = true;

                                    this.transform.position = warpPoint_1.transform.position;
                                    warpDeray = 0;

                                    warp_1Effect = false;
                                    phase_1++;

                                    stopTime = 0;
                                }
                            }
                        }
                        else
                        {
                            stopTime += Time.deltaTime;

                            if (stopTime >= 3)
                            {
                                if (!warp_3Effect)
                                {
                                    Instantiate(warpEffect, warpPoint_3.transform.position, Quaternion.identity);
                                    warp_3Effect = true;
                                }

                                warpDeray += Time.deltaTime;

                                _sr.enabled = false;
                                _cl2d.enabled = false;

                                if (warpDeray >= 1)
                                {
                                    _sr.enabled = true;
                                    _cl2d.enabled = true;

                                    this.transform.position = warpPoint_3.transform.position;
                                    warpDeray = 0;

                                    warp_3Effect = false;
                                    phase_1++;

                                    stopTime = 0;
                                }
                            }
                        }
                        break;

                    case 7:
                        if(this.transform.position.y <= 0)
                        {

                            Debug.Log("ƒ_ƒbƒVƒ…");

                            this.transform.position = Vector2.MoveTowards(this.transform.position, warpPoint_2.transform.position, 2 * Time.deltaTime);

                            if(this.transform.position.x <= warpPoint_2.transform.position.x)
                            {
                                phase_1++;
                            }
                        }
                        else
                        {
                            this.transform.position = Vector2.MoveTowards(this.transform.position, warpPoint_4.transform.position, Time.deltaTime);

                            if (this.transform.position.x <= warpPoint_4.transform.position.x)
                            {
                                phase_1++;
                            }
                        }
                        break;

                    case 8:
                        stopTime += Time.deltaTime;

                        if (stopTime >= 3)
                        {
                            if (!warp_1Effect)
                            {
                                Instantiate(warpEffect, warpPoint_1.transform.position, Quaternion.identity);
                                warp_1Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_1.transform.position;
                                warpDeray = 0;

                                warp_1Effect = false;
                                phase_1 = 1;

                                stopTime = 0;
                            }
                        }
                        break;
                }
                break;

            case 2:
                switch (phase_2)
                {
                    case 0:
                        attackCount = 0;

                        if (!warp_5Effect)
                        {
                            Instantiate(warpEffect, warpPoint_5.transform.position, Quaternion.identity);
                            warp_5Effect = true;
                        }

                        warpDeray += Time.deltaTime;

                        _sr.enabled = false;
                        _cl2d.enabled = false;

                        if (warpDeray >= 1)
                        {
                            _sr.enabled = true;
                            _cl2d.enabled = true;

                            this.transform.position = warpPoint_5.transform.position;
                            warpDeray = 0;

                            warp_5Effect = false;
                            phase_2++;
                        }
                        break;

                    case 1:
                        Fire();
                        if (attackCount == 1)
                        {
                            phase_2++;
                            attackCount = 0;
                        }
                        break;

                    case 2:
                        Attack();
                        if (attackCount == 3)
                        {
                            phase_2++;
                            attackCount = 0;
                        }
                        break;

                    case 3:
                        stopTime += Time.deltaTime;

                        if (stopTime >= 3)
                        {
                            if (!warp_3Effect)
                            {
                                Instantiate(warpEffect, warpPoint_3.transform.position, Quaternion.identity);
                                warp_3Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_3.transform.position;
                                warpDeray = 0;

                                warp_3Effect = false;
                                phase_2++;

                                stopTime = 0;
                            }
                        }
                        break;

                    case 4:
                        BigAttack();
                        if (attackCount == 1)
                        {
                            phase_2++;
                            attackCount = 0;
                        }
                        break;

                    case 5:
                        stopTime += Time.deltaTime;

                        if (stopTime >= 3)
                        {
                            if (!warp_4Effect)
                            {
                                Instantiate(warpEffect, warpPoint_4.transform.position, Quaternion.identity);
                                warp_4Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_4.transform.position;
                                warpDeray = 0;

                                warp_4Effect = false;
                                phase_2++;

                                stopTime = 0;
                            }
                        }
                        break;

                    case 6:
                        BigAttack();
                        if (attackCount == 1)
                        {
                            phase_2++;
                            attackCount = 0;
                        }
                        break;

                    case 7:
                        stopTime += Time.deltaTime;

                        if (stopTime >= 3)
                        {
                            if (!warp_5Effect)
                            {
                                Instantiate(warpEffect, warpPoint_5.transform.position, Quaternion.identity);
                                warp_5Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_5.transform.position;
                                warpDeray = 0;

                                warp_5Effect = false;
                                phase_2++;

                                stopTime = 0;
                            }
                        }
                        break;

                    case 8:
                        Fire();
                        if (attackCount == 2)
                        {
                            phase_2++;
                            attackCount = 0;
                        }
                        break;

                    case 9:
                        stopTime += Time.deltaTime;

                        if (stopTime >= 3)
                        {
                            if (!warp_1Effect)
                            {
                                Instantiate(warpEffect, warpPoint_1.transform.position, Quaternion.identity);
                                warp_1Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_1.transform.position;
                                warpDeray = 0;

                                warp_1Effect = false;
                                phase_2++;

                                stopTime = 0;
                            }
                        }
                        break;

                    case 10:
                        BigAttack();
                        if (attackCount == 1)
                        {
                            phase_2++;
                            attackCount = 0;
                        }
                        break;

                    case 11:
                        stopTime += Time.deltaTime;

                        if (stopTime >= 3)
                        {
                            if (!warp_2Effect)
                            {
                                Instantiate(warpEffect, warpPoint_2.transform.position, Quaternion.identity);
                                warp_2Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_2.transform.position;
                                warpDeray = 0;

                                warp_2Effect = false;
                                phase_2++;

                                stopTime = 0;
                            }
                        }
                        break;

                    case 12:
                        BigAttack();
                        if (attackCount == 1)
                        {
                            phase_2++;
                            attackCount = 0;
                        }
                        break;

                    case 13:
                        stopTime += Time.deltaTime;

                        if (stopTime >= 3)
                        {
                            if (!warp_5Effect)
                            {
                                Instantiate(warpEffect, warpPoint_5.transform.position, Quaternion.identity);
                                warp_5Effect = true;
                            }

                            warpDeray += Time.deltaTime;

                            _sr.enabled = false;
                            _cl2d.enabled = false;

                            if (warpDeray >= 1)
                            {
                                _sr.enabled = true;
                                _cl2d.enabled = true;

                                this.transform.position = warpPoint_5.transform.position;
                                warpDeray = 0;

                                warp_5Effect = false;
                                phase_2 = 1;

                                stopTime = 0;
                            }
                        }
                        break;
                }
                break;
        }

        if(this.transform.position.x > player.transform.position.x)
        {
            _sr.flipX = false;
            attackP = attackL;
        }
        else
        {
            _sr.flipX = true;
            attackP = attackR;
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

            if (_coolTime >= 4.0f)
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
                animator.SetBool("Attack", true);

                _coolTime = 0;
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
                Instantiate(fire, fireP.transform.position, Quaternion.identity);
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
                animator.SetBool("Attack", true);

                _coolTime = 0;
            }
        }
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AA"))
        {
            _hp -= GameObject.Find("Player_Test").GetComponent<PlayerControl>()._attackPower;
        }
    }
}
