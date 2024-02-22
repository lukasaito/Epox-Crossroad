using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private SpriteRenderer _sr;
    private Animator _animator;
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

    // çsìÆãñâ¬ópÇÃïœêî
    [SerializeField]
    public Camera mainCamera;
    private bool canAction;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        _attackDelay = _attackDelayMax;

        _hp = 15;

        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        // âÊñ èÓïÒÇÃéÊìæóp
        CheckOnScreen();

        if(canAction)
        {
            switch (id)
            {
                case 0:
                    _rb2d.velocity = new Vector2(_moveSpeed, _rb2d.velocity.y);

                    if (_moveSpeed > 0)
                    {
                        _sr.flipX = true;
                    }
                    else if (_moveSpeed < 0)
                    {
                        _sr.flipX = false;
                    }
                    break;

                case 1:
                    _playerPos = FindObjectOfType<PlayerControl>().transform;
                    this.transform.position = Vector2.MoveTowards(this.transform.position, _playerPos.position, _moveSpeed * Time.deltaTime);

                    Vector3 dir = _playerPos.position - this.transform.position;
                    this.transform.rotation = Quaternion.FromToRotation(Vector3.left, dir);

                    if (this.transform.position.x > _playerPos.transform.position.x)
                    {
                        _sr.flipY = false;
                    }
                    else
                    {
                        _sr.flipY = true;
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

            if (_hp <= 0)
            {
                Destroy(gameObject);
            }

            _animator.SetInteger("ModelNumber", FindAnyObjectByType<PlayerControl>()._modelNumber);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("AA"))
        {
            _hp -= GameObject.Find("Player_Test").GetComponent<PlayerControl>()._attackPower;
        }
    }
}
