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
    private AudioSource _audioSource;

    [SerializeField]
    private float _jumpPower;
    [SerializeField]
    private bool _isJump;

    [SerializeField]
    private SheetData sheetData;
    public int level;

    public int _modelNumber;
    public int _memoryCount;

    public GameObject famiCom;
    public GameObject scanLine;
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

    bool _invincible;
    float _invincibleCoolTime;
    float _colorTime;

    private float deadTime;

    [Header("プレイヤーサウンドクリップ")]
    public AudioClip jump;
    public AudioClip dead;
    public AudioClip damage;


    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _bc2d = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        level = 1;

        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            _hp += 10000;   
        }
        else
        {
            sega.SetActive(false);
            ds.SetActive(false);
            switch_N.SetActive(false);
        }
        

        _jumpPower = sheetData.sheetDataRecord[level - 1].JumpPower;

        _attackPower = sheetData.sheetDataRecord[level - 1].AttackPower;

        _hp = sheetData.sheetDataRecord[level - 1].HitPoint;
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name =="Tutorial")
        {
            if (!_dead)
            {
                _inputX = Input.GetAxisRaw("Horizontal");
                _animator.SetInteger("ModelNumber", _modelNumber);


                _rb2d.velocity = new Vector2(_nowSpeed, _rb2d.velocity.y);

                if (0 != _inputX)
                {
                    _animator.SetBool("Walk", true);

                    if (0.3 < _inputX || -0.3 > _inputX)
                    {
                        if (_inputX > 0)
                        {
                            _nowSpeed = runSpeed;
                        }
                        else if (_inputX < 0)
                        {
                            _nowSpeed = -runSpeed;
                        }
                    }
                    else
                    {
                        if (_inputX > 0)
                        {
                            _nowSpeed = walkSpeed;
                        }
                        else if (_inputX < 0)
                        {
                            _nowSpeed = -walkSpeed;
                        }
                    }
                }
                else
                {
                    _nowSpeed = 0;
                    _animator.SetBool("Walk", false);
                }

                if (_inputX > 0)
                {
                    _sr.flipX = false;
                }
                else if (_inputX < 0)
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
                    _audioSource.clip = jump;
                    _audioSource.Play();

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
                    if (_memoryCount > 0)
                    {
                        FindObjectOfType<TimeChangeEffect>().isTimeChange();
                        _memoryCount--;
                        _modelNumber++;

                        if (_modelNumber > 3)
                        {
                            _modelNumber = 0;
                        }
                    }
                }

                switch (_modelNumber)
                {
                    // ファミコン
                    case 0:
                        famiCom.SetActive(true);
                        _bc2d.offset = new Vector2(0.05f, -0.05f);
                        _bc2d.size = new Vector2(0.4f, 0.9f);

                        Application.targetFrameRate = 30;
                        break;

                    // Sega
                    case 1:
                        famiCom.SetActive(false);
                        Application.targetFrameRate = 60;
                        break;

                    // 3DS
                    case 2:
                        _bc2d.offset = new Vector2(0.05f, -0.04f);
                        _bc2d.size = new Vector2(0.5f, 0.8f);

                        Application.targetFrameRate = 90;
                        break;

                    // Switch
                    case 3:
                        _bc2d.offset = new Vector2(0.02f, -0.095f);
                        _bc2d.size = new Vector2(0.4f, 1.15f);

                        Application.targetFrameRate = 120;
                        break;
                }

                if (_attackFlag)
                {
                    if (Input.GetMouseButtonDown(0))
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
                    if (Input.GetMouseButtonDown(0))
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
            // 死亡
            else
            {
                deadTime += Time.deltaTime;

                if (deadTime > 2.0f)
                {
                    SceneManager.LoadScene("GameOver");
                }
            }


            if (_hp <= 0)
            {
                _animator.SetBool("Dead", true);
                _dead = true;
            }

            if (_invincible)
            {
                _invincibleCoolTime += Time.deltaTime;
                _colorTime += Time.deltaTime;

                if (_colorTime >= 0.2f)
                {
                    _sr.enabled = true;
                    _sr.color = new Color(255f, 255f, 255f);
                }
                else if (_colorTime >= 0.1f)
                {
                    _sr.enabled = false;
                }
                else
                {
                    _sr.color = new Color(255f, 0, 0);
                }

                if (_invincibleCoolTime >= 0.8f)
                {
                    _invincible = false;
                    _invincibleCoolTime = 0;
                    _colorTime = 0;
                }
            }
        }
        else
        {
            if (!_dead)
            {
                _inputX = Input.GetAxisRaw("Horizontal");
                _animator.SetInteger("ModelNumber", _modelNumber);


                _rb2d.velocity = new Vector2(_nowSpeed, _rb2d.velocity.y);

                if (0 != _inputX)
                {
                    _animator.SetBool("Walk", true);

                    if (0.3 < _inputX || -0.3 > _inputX)
                    {
                        if (_inputX > 0)
                        {
                            _nowSpeed = runSpeed;
                        }
                        else if (_inputX < 0)
                        {
                            _nowSpeed = -runSpeed;
                        }
                    }
                    else
                    {
                        if (_inputX > 0)
                        {
                            _nowSpeed = walkSpeed;
                        }
                        else if (_inputX < 0)
                        {
                            _nowSpeed = -walkSpeed;
                        }
                    }
                }
                else
                {
                    _nowSpeed = 0;
                    _animator.SetBool("Walk", false);
                }

                if (_inputX > 0)
                {
                    _sr.flipX = false;
                }
                else if (_inputX < 0)
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
                    _audioSource.clip = jump;
                    _audioSource.Play();
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
                    if(_memoryCount > 0)
                    {
                        FindObjectOfType<TimeChangeEffect>().isTimeChange();
                        _memoryCount--;
                        _modelNumber++;

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
                        scanLine.SetActive(true);
                        switch_N.SetActive(false);

                        _bc2d.offset = new Vector2(0.05f, -0.05f);
                        _bc2d.size = new Vector2(0.4f, 0.9f);

                        Application.targetFrameRate = 30;
                        break;

                    case 1:
                        sega.SetActive(true);
                        famiCom.SetActive(false);
                        scanLine.SetActive(false);

                        Application.targetFrameRate = 60;
                        break;

                    case 2:
                        ds.SetActive(true);
                        sega.SetActive(false);

                        _bc2d.offset = new Vector2(0.05f, -0.04f);
                        _bc2d.size = new Vector2(0.5f, 0.8f);

                        Application.targetFrameRate = 90;
                        break;

                    case 3:
                        switch_N.SetActive(true);
                        ds.SetActive(false);

                        _bc2d.offset = new Vector2(0.02f, -0.095f);
                        _bc2d.size = new Vector2(0.4f, 1.15f);

                        Application.targetFrameRate = 120;
                        break;
                }

                if (_attackFlag)
                {
                    if (Input.GetMouseButtonDown(0))
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
                    if (Input.GetMouseButtonDown(0))
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
            // 死亡
            else
            {
                deadTime +=Time.deltaTime;

                if(deadTime > 2.0f)
                {
                    SceneManager.LoadScene("GameOver");
                }
            }


            if (_hp <= 0)
            {
                _animator.SetBool("Dead", true);
                _dead = true;
            }

            if (_invincible)
            {
                _invincibleCoolTime += Time.deltaTime;
                _colorTime += Time.deltaTime;

                if (_colorTime >= 0.2f)
                {
                    _sr.enabled = true;
                    _sr.color = new Color(255f, 255f, 255f);
                }
                else if (_colorTime >= 0.1f)
                {
                    _sr.enabled = false;
                }
                else
                {
                    _sr.color = new Color(255f, 0, 0);
                }

                if (_invincibleCoolTime >= 0.8f)
                {
                    _invincible = false;
                    _invincibleCoolTime = 0;
                    _colorTime = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "NextScene")
        {
            SceneManager.LoadScene("StageSelect");
        }

        if(collision.CompareTag("Key"))
        {
            keyF = true;
        }

        if(collision.CompareTag("Door"))
        {
            if(keyF)
            {
                FindObjectOfType<CloseDoor>().isOpenDoor = true;
            }
        }

        if(collision.gameObject.name == "OpenDoor")
        {
            SceneManager.LoadScene("GameClear");
            PlayerMove.isClear_Stage1 = true;
        }

        if (collision.gameObject.CompareTag("Boss_Bullet"))
        {
            if (!_invincible)
            {
                _audioSource.clip = damage;
                _audioSource.Play();

                _hp = _hp - 10;

                Vector2 vector2 = this.transform.position - collision.transform.position;
                _rb2d.AddForce(transform.up * vector2 * 10, ForceMode2D.Impulse);

                Debug.Log("ダメージ");
                _invincible = true;
            }
        }

        if(collision.CompareTag("DArea"))
        {
            _dead = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Fire"))
        {
            if (!_invincible)
            {
                _hp -= 2f * Time.deltaTime;

                Debug.Log("Fire");
                _invincible = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!_invincible)
            {
                _audioSource.clip = damage;
                _audioSource.Play();

                _hp = _hp - 5;

                Vector2 vector2 = this.transform.position - collision.transform.position;
                _rb2d.AddForce(transform.up * vector2 * 10, ForceMode2D.Impulse);

                Debug.Log("ダメージ");
                _invincible = true;
            }
        }
    }
}