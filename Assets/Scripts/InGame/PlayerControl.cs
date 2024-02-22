using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private float _inputX;
    private float _nowSpeed;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    private Rigidbody2D _rb2d;
    private SpriteRenderer _sr;
    private BoxCollider2D _bc2d;

    private float h;

    [SerializeField]
    private float _jumpPower;
    [SerializeField]
    private bool _isJump;

    [SerializeField]
    private SheetData sheetData;
    public int level;

    public int _modelNumber;
    private int _memoryCount;
    [SerializeField]
    private bool _isHaveMemory;

    public GameObject famiCom;
    public GameObject sega;
    public GameObject ds;
    public GameObject switch_N;

    public Animator _animator;

    public float _attackPower;
    public GameObject attackArea;
    private float _attackDelay;
    private bool _attackFlag;

    [SerializeField]
    float _hp;

    bool keyF;

    bool _dead;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _bc2d = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        level = 1;

        sega.SetActive(false);
        ds.SetActive(false);
        switch_N.SetActive(false);

        _jumpPower = sheetData.sheetDataRecord[level - 1].JumpPower;

        _attackPower = sheetData.sheetDataRecord[level - 1].AttackPower;

        _hp = sheetData.sheetDataRecord[level - 1].HitPoint;
    }

    void Update()
    {
        if (!_dead)
        {
            h = Input.GetAxisRaw("Horizontal");
            _animator.SetInteger("ModelNumber", _modelNumber);


            if (0 != h)
            {
                _inputX = transform.position.x;

                _rb2d.velocity = new Vector2(_nowSpeed, _rb2d.velocity.y);

                _animator.SetBool("Walk", true);

                if (0.3 < h || -0.3 > h)
                {
                    if (h > 0)
                    {
                        _nowSpeed = runSpeed;
                    }
                    else if (h < 0)
                    {
                        _nowSpeed = -runSpeed;
                    }
                }
                else
                {
                    if (h > 0)
                    {
                        _nowSpeed = walkSpeed;
                    }
                    else if (h < 0)
                    {
                        _nowSpeed = -walkSpeed;
                    }
                }
            }
            else
            {
                _animator.SetBool("Walk", false);
            }

            if (h > 0)
            {
                _sr.flipX = false;
            }
            else if (h < 0)
            {
                _sr.flipX = true;
            }


            if (0 != Input.GetAxisRaw("Jump"))
            {
                if (_rb2d.velocity.y == 0)
                {
                    _isJump = true;
                }
            }

            if (_isJump)
            {
                _rb2d.AddForce(_jumpPower * transform.up, ForceMode2D.Impulse);

                _isJump = false;
            }

            if (_rb2d.velocity.y == 0)
            {
                _animator.SetBool("Jump", false);
            }
            else
            {
                _animator.SetBool("Jump", true);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                //if(_isHaveMemory)
                {
                    _modelNumber++;
                    _isHaveMemory = false;

                    if (_modelNumber > 3)
                    {
                        _modelNumber = 0;
                    }
                }
            }

            switch (_modelNumber)
            {
                case 0:
                    famiCom.SetActive(true);
                    switch_N.SetActive(false);

                    _bc2d.offset = new Vector2(0.05f, -0.05f);
                    _bc2d.size = new Vector2(0.4f, 0.9f);
                    break;

                case 1:
                    sega.SetActive(true);
                    famiCom.SetActive(false);
                    break;

                case 2:
                    ds.SetActive(true);
                    sega.SetActive(false);

                    _bc2d.offset = new Vector2(0.05f, -0.04f);
                    _bc2d.size = new Vector2(0.5f, 0.8f);
                    break;

                case 3:
                    switch_N.SetActive(true);
                    ds.SetActive(false);

                    _bc2d.offset = new Vector2(0.02f, -0.095f);
                    _bc2d.size = new Vector2(0.4f, 1.15f);
                    break;
            }

            if (_attackFlag)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    attackArea.SetActive(true);

                    _attackFlag = false;
                }
            }
            else
            {
                _attackDelay += Time.deltaTime;

                if (_attackDelay >= 1f)
                {
                    _attackDelay = 0;
                    _attackFlag = true;
                }
                else if (_attackDelay >= 0.3f)
                {
                    attackArea.SetActive(false);
                }
            }

            if (_attackFlag)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    _attackFlag = false;
                }
            }
            else
            {
                _attackDelay += Time.deltaTime;

                _animator.SetBool("Attack", true);

                if (_attackDelay >= 0.6f)
                {
                    _animator.SetBool("Attack", false);

                    _attackFlag = true;

                    Debug.Log("aa");

                    _attackDelay = 0;
                }
            }
        }

        if(_hp <= 0)
        {
            _animator.SetBool("Dead", true);
            _dead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Key"))
        {
            keyF = true;

            Destroy(collision.gameObject);
        }

        if(collision.CompareTag("Door"))
        {
            if(keyF)
            {
                SceneManager.LoadScene("Stage_02");
            }
        }


        if (collision.gameObject.CompareTag("Boss_Bullet"))
        {
            _hp = _hp - 10;

            Vector2 vector2 = this.transform.position - collision.transform.position;
            _rb2d.AddForce(transform.up * vector2 * 10, ForceMode2D.Impulse);

            Debug.Log("ダメージ");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _hp = _hp -  5;

            Vector2 vector2 = this.transform.position - collision.transform.position;
            _rb2d.AddForce(transform.up * vector2*10, ForceMode2D.Impulse);

            Debug.Log("ダメージ");
        }
    }
}
