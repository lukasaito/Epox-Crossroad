using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int moveCount; // 0 = �X�e�[�W1 / 1 = �X�e�[�W2 / �X�e�[�W3
    public float speed;
    public float jumpPower;

    private float _inputX;
    private Rigidbody2D _rb2d;
    private BoxCollider2D _bc2d;
    private Animator _animator;
    private SpriteRenderer _sp;
    private int _modelNumber;

    private bool isFlip;
    public bool isStop;
    public bool isMoveNow; // true = �ړ��� / false = ����
    public bool plusMove;
    public bool minusMove;

    public GameObject stage1ToStage2;
    public GameObject stage2ToStage1;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _bc2d = GetComponent<BoxCollider2D>();
        _sp = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        stage2ToStage1.SetActive(false);
    }

    void Update()
    {

        //_animator.SetInteger("ModelNumber", _modelNumber);

        //�߂�
        // �C�ӂ̃{�^�� �Ȃ����� ��~���̏ꍇ
        if (Input.GetKeyDown(KeyCode.A) && isStop || Input.GetKeyDown(KeyCode.LeftArrow) && isStop)
        {
            // 0������̏ꍇ
            if (moveCount > 0)
            {
                isMoveNow = true;
                moveCount -= 1;
                isStop = false;
                minusMove = true;
            }

            
        }

        if(minusMove)
        {
            switch (moveCount)
            {
                // �X�e�[�W2 ? �X�e�[�W1�ɖ߂�
                case 0:
                    stage1ToStage2.SetActive(false);
                    stage2ToStage1.SetActive(true);

                    // ��~
                    if (isStop)
                    {
                        Debug.Log("1");
                        _rb2d.velocity = new Vector2(0, 0);
                    }
                    // X���v���X�����Ɉړ�
                    else if (!isFlip && !isStop)
                    {
                        Debug.Log("2");
                        _rb2d.velocity = new Vector2(-speed, _rb2d.velocity.y);
                    }
                    // X���}�C�i�X�����Ɉړ�
                    else if (isFlip && !isStop)
                    {
                        Debug.Log("3");
                        _rb2d.velocity = new Vector2(speed, _rb2d.velocity.y);
                    }
                    break;
            }
        }

        // �i�߂�
        // �C�ӂ̃{�^�� �Ȃ����� ��~���̏ꍇ
        if (Input.GetKeyDown(KeyCode.D) && isStop || Input.GetKeyDown(KeyCode.RightArrow) &&isStop)
        {
            // 3�������̏ꍇ
            if (moveCount < 3)
            {
                isStop = false;
                isMoveNow = true;
                moveCount += 1;
                plusMove = true;
            }
        }

        if(plusMove)
        {
            switch (moveCount)
            {
                // �X�e�[�W1 ? �X�e�[�W2�ɐi��
                case 1:
                    stage1ToStage2.SetActive(true);
                    stage2ToStage1.SetActive(false);

                    // ��~
                    if (isStop)
                    {
                        _rb2d.velocity = new Vector2(0, 0);
                    }
                    // X���v���X�����Ɉړ�
                    else if (!isFlip && !isStop)
                    {
                        _rb2d.velocity = new Vector2(speed, _rb2d.velocity.y);
                    }
                    // X���}�C�i�X�����Ɉړ�
                    else if(isFlip && !isStop)
                    {
                        _rb2d.velocity = new Vector2(-speed, _rb2d.velocity.y);
                    }
                    break;
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �G�ꂽ��W�����v
        if(collision.gameObject.name == "JumpPoint_Area1")
        {
            _rb2d.AddForce(jumpPower * transform.up, ForceMode2D.Impulse);
        }
        // �G�ꂽ��W�����v
        if (collision.gameObject.name == "JumpUpPoint_Area1")
        {
            jumpPower += 10.5f;
            _rb2d.AddForce(jumpPower * transform.up, ForceMode2D.Impulse);
        }

        

        // �G�ꂽ�瑬�x�E����A�b�v
        if (collision.gameObject.name == "JumpPoint&Flip_Area1")
        {
            // ���]���Ă��鎞�͖���
            if(!isFlip)
            {
                speed += 4.5f;
                jumpPower += 5.5f;
                _rb2d.AddForce(jumpPower * transform.up, ForceMode2D.Impulse);
            }
            // ���]��Ԃ����ɖ߂�
            else
            {
                isFlip = false;
                _sp.flipX = false;
            }
        }

        // �X�e�[�W1 ����
        if (collision.gameObject.name == "StopPoint_Area1")
        {
            _rb2d.velocity = new Vector2(0, 0);
            isMoveNow = false;
            isStop = true;
        }

        // �X�e�[�W2 ����
        if (collision.gameObject.name == "StopPoint_Area2")
        {
            Debug.Log("�X�e�[�W2");
            _rb2d.velocity = new Vector2(0, 0);
            isMoveNow = false;
            isStop = true;
            plusMove = false;
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "JumpPoint&Flip_Area1")
        {
            // ���]���Ă��Ȃ���
            if(!isFlip)
            {
                isFlip = true;�@�@�@// ���]�t���OON
                speed -= 4.5f;
                jumpPower -= 5.5f;
                _sp.flipX = true;  // ���]������
            }
        }

        // �G�ꂽ��W�����v
        if (collision.gameObject.name == "JumpUpPoint_Area1")
        {
            jumpPower -= 10.5f;
            
        }
    }
}