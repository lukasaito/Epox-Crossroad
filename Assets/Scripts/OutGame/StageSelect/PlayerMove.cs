using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int moveCount; // 0 = ステージ1 / 1 = ステージ2 / ステージ3
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
    public bool isMoveNow; // true = 移動中 / false = 到着
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

        //戻る
        // 任意のボタン なおかつ 停止中の場合
        if (Input.GetKeyDown(KeyCode.A) && isStop || Input.GetKeyDown(KeyCode.LeftArrow) && isStop)
        {
            // 0よりも上の場合
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
                // ステージ2 ? ステージ1に戻る
                case 0:
                    stage1ToStage2.SetActive(false);
                    stage2ToStage1.SetActive(true);

                    // 停止
                    if (isStop)
                    {
                        Debug.Log("1");
                        _rb2d.velocity = new Vector2(0, 0);
                    }
                    // X軸プラス方向に移動
                    else if (!isFlip && !isStop)
                    {
                        Debug.Log("2");
                        _rb2d.velocity = new Vector2(-speed, _rb2d.velocity.y);
                    }
                    // X軸マイナス方向に移動
                    else if (isFlip && !isStop)
                    {
                        Debug.Log("3");
                        _rb2d.velocity = new Vector2(speed, _rb2d.velocity.y);
                    }
                    break;
            }
        }

        // 進める
        // 任意のボタン なおかつ 停止中の場合
        if (Input.GetKeyDown(KeyCode.D) && isStop || Input.GetKeyDown(KeyCode.RightArrow) &&isStop)
        {
            // 3よりも下の場合
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
                // ステージ1 ? ステージ2に進む
                case 1:
                    stage1ToStage2.SetActive(true);
                    stage2ToStage1.SetActive(false);

                    // 停止
                    if (isStop)
                    {
                        _rb2d.velocity = new Vector2(0, 0);
                    }
                    // X軸プラス方向に移動
                    else if (!isFlip && !isStop)
                    {
                        _rb2d.velocity = new Vector2(speed, _rb2d.velocity.y);
                    }
                    // X軸マイナス方向に移動
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
        // 触れたらジャンプ
        if(collision.gameObject.name == "JumpPoint_Area1")
        {
            _rb2d.AddForce(jumpPower * transform.up, ForceMode2D.Impulse);
        }
        // 触れたらジャンプ
        if (collision.gameObject.name == "JumpUpPoint_Area1")
        {
            jumpPower += 10.5f;
            _rb2d.AddForce(jumpPower * transform.up, ForceMode2D.Impulse);
        }

        

        // 触れたら速度・跳躍アップ
        if (collision.gameObject.name == "JumpPoint&Flip_Area1")
        {
            // 反転している時は無し
            if(!isFlip)
            {
                speed += 4.5f;
                jumpPower += 5.5f;
                _rb2d.AddForce(jumpPower * transform.up, ForceMode2D.Impulse);
            }
            // 反転状態を元に戻す
            else
            {
                isFlip = false;
                _sp.flipX = false;
            }
        }

        // ステージ1 到着
        if (collision.gameObject.name == "StopPoint_Area1")
        {
            _rb2d.velocity = new Vector2(0, 0);
            isMoveNow = false;
            isStop = true;
        }

        // ステージ2 到着
        if (collision.gameObject.name == "StopPoint_Area2")
        {
            Debug.Log("ステージ2");
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
            // 反転していない時
            if(!isFlip)
            {
                isFlip = true;　　　// 反転フラグON
                speed -= 4.5f;
                jumpPower -= 5.5f;
                _sp.flipX = true;  // 反転させる
            }
        }

        // 触れたらジャンプ
        if (collision.gameObject.name == "JumpUpPoint_Area1")
        {
            jumpPower -= 10.5f;
            
        }
    }
}