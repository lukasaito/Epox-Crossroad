using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerMove : MonoBehaviour
{
    private float _inputX;
    private float _nowSpeed;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float _jumpPower;

    private bool _isJump;

    private Rigidbody2D _rb2d;
    private Animator _animator;
    private SpriteRenderer _sr;
    private BoxCollider2D _bc2d;
    private bool _attackFlag;
    private float _attackDelay;

    public int _modelNumber;
    public GameObject attackArea;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        //_animator.SetInteger("ModelNumber", _modelNumber);

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

        switch (_modelNumber)
        {
            case 0:
                _bc2d.offset = new Vector2(0.05f, -0.05f);
                _bc2d.size = new Vector2(0.4f, 0.9f);
                break;

            case 1:
                break;

            case 2:
                _bc2d.offset = new Vector2(0.05f, -0.04f);
                _bc2d.size = new Vector2(0.5f, 0.8f);
                break;

            case 3:
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
}
