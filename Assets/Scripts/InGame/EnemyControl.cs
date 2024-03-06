using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyControl : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private SpriteRenderer _sr;
    private Animator _animator;
    private AudioSource _audioSource;
    private BoxCollider2D _box2d;
    public float _moveSpeed;
    public float _hp;

    private Transform _playerPos;
    private Vector2 _direction;
    private Vector2 _moveMnet;

    private bool _isAttack;
    private int _attackPower;
    private float _attackDelay;
    public float _attackDelayMax;

    public int id;

    public bool inATKArea;
    public bool isDamage = false;
    public float colorTime;

    // 行動許可用の変数
    [SerializeField]
    public Camera mainCamera;
    private bool canAction;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _box2d = GetComponent<BoxCollider2D>();

        _attackDelay = _attackDelayMax;

        _hp = 15;

        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        // 画面情報の取得用
        CheckOnScreen();

        if(canAction)
        {
            // 敵の種別
            switch (id)
            {
                // スライム
                case 0:
                    // Tutorialでは無い時
                    if(SceneManager.GetActiveScene().name != "Tutorial")
                    {
                        // 移動処理。
                        _rb2d.velocity = new Vector2(_moveSpeed, _rb2d.velocity.y);

                        // 向きに応じて画像を反転させる。
                        if (_moveSpeed > 0)
                        {
                            _sr.flipX = true;
                        }
                        else if (_moveSpeed < 0)
                        {
                            _sr.flipX = false;
                        }
                    }
                    break;

                // 小型ドラゴン
                case 1:
                    // Tutorialでは無い時
                    if (SceneManager.GetActiveScene().name != "Tutorial")
                    {
                        // プレイヤーに向かって移動する処理。
                        _playerPos = FindObjectOfType<PlayerControl>().transform;
                        this.transform.position = Vector2.MoveTowards(this.transform.position, _playerPos.position, _moveSpeed * Time.deltaTime);
                            
                        Vector3 dir = _playerPos.position - this.transform.position;
                        this.transform.rotation = Quaternion.FromToRotation(Vector3.left, dir);

                        // 向きに応じて画像を反転させる。
                        if (this.transform.position.x > _playerPos.transform.position.x)
                        {
                            _sr.flipY = false;
                        }
                        else
                        {
                            _sr.flipY = true;
                        }
                    }
                    break;
            }

            _playerPos = FindObjectOfType<PlayerControl>().transform;

            _direction = _playerPos.position - transform.position;

            if (transform.position.x - _playerPos.position.x <= 1.5 && transform.position.x - _playerPos.position.x >= -1.5)
            {
                if (_isAttack)
                {
                    Debug.Log("Attack");

                    _isAttack = false;
                }

                _attackDelay += Time.deltaTime;

                if (_attackDelay >= _attackDelayMax)
                {
                    _isAttack = true;

                    _attackDelay = 0;
                }
            }
            else
            {
                _isAttack = false;
            }

            // 死亡時
            if (_hp <= 0)
            {
                if (!_audioSource.isPlaying)
                {
                    _audioSource.Play();
                }
                _rb2d.gravityScale = 0;
                _sr.enabled = false;
                _box2d.enabled = false;
                Destroy(gameObject,0.2f);
            }

            _animator.SetInteger("ModelNumber", FindAnyObjectByType<PlayerControl>()._modelNumber);
        }

        // 攻撃範囲内にいるときに、ダメージを受けた場合の処理
        // 03/02：オカムラ追加
        if (inATKArea)
        {
            if (isDamage == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Dmg");
                    _hp -= GameObject.Find("Player_Test").GetComponent<PlayerControl>()._attackPower;
                    isDamage = true;
                }
            }
        }

        // 攻撃を受けたら色を変える。
        // 一定時間経過で元に戻す。
        if(isDamage)
        {            
            colorTime += Time.deltaTime;
            _sr.color = new Color(255f, 0f, 0f);
        }
        if (colorTime > 0.2f)
        {
            _sr.color = new Color(255f, 255f, 255f);
            colorTime = 0.0f;
            isDamage = false;

            // 03/02 : マシコ追加。
            inATKArea = false;
        }
    }

    void CheckOnScreen()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        canAction = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (id == 0)
        {
            if (collision.gameObject)
            {
                _moveSpeed = _moveSpeed * -1;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("AA"))
        {
            Debug.Log("in");
            inATKArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AA"))
        {
            inATKArea = false;
        }
    }
}