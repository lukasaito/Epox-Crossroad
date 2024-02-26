using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public int moveCount; // 0 = ステージ1 / 1 = ステージ2 / ステージ3
    public float speed;
    public float jumpPower;

    private Rigidbody2D _rb2d;
    private Animator _animator;
    private SpriteRenderer _sp;
    private int _modelNumber;
    private bool nextScene;
    private float nextSceneTime;

    public bool isStop;
    public bool isMoveNow; // true = 移動中 / false = 到着
    public bool plusMove;
    public bool minusMove;

    public GameObject stage1ToStage2;
    public GameObject stage2ToStage1;
    public GameObject stage2ToStage3;
    public GameObject stage3ToStage2;

    public Animator stage1_PopUp;
    public Animator stage2_PopUp;
    public Animator stage3_PopUp;

    public float minusVolume;
    public AudioSource stageSelectBgm;

    public Animator maskPlayer;

    public AudioSource pressKeySound;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _sp = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        stage2ToStage1.SetActive(false);
        stage2ToStage3.SetActive(false);
        stage3ToStage2.SetActive(false);
    }

    void Update()
    {
        //_animator.SetInteger("ModelNumber", _modelNumber);

        // 遷移状態
        if(nextScene)
        {
            pressKeySound.Play();
            maskPlayer.SetBool("isStart", true);
            nextSceneTime += Time.deltaTime;

            if(nextSceneTime > 2.0f)
            {
                switch (moveCount)
                {
                    case 0:
                        SceneManager.LoadScene("StageLoading");
                        StageLoading.loadingNumber = 0;
                        break;

                    case 1:
                        SceneManager.LoadScene("StageLoading");
                        StageLoading.loadingNumber = 1;
                        break;

                    case 2:
                        SceneManager.LoadScene("StageLoading");
                        StageLoading.loadingNumber = 2;
                        break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            // 遷移状態にする。
            nextScene = true;
        }

        // 進める
        // 任意のボタン なおかつ 停止中の場合
        if (Input.GetKeyDown(KeyCode.D) && isStop || Input.GetKeyDown(KeyCode.RightArrow) && isStop)
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

        if (plusMove)
        {
            switch (moveCount)
            {
                // ステージ1 → ステージ2に進む
                case 1:
                    stage1ToStage2.SetActive(true);
                    stage2ToStage1.SetActive(false);

                    // 停止
                    if (isStop)
                    {
                        _rb2d.velocity = new Vector2(0, 0);
                    }
                    // X軸プラス方向に移動
                    else
                    {
                        stage1_PopUp.SetBool("isStart", false);
                        _sp.flipX = false;
                        _rb2d.velocity = new Vector2(speed, _rb2d.velocity.y);
                    }
                    break;

                // ステージ2 → ステージ3に進む
                case 2:
                    stage2ToStage3.SetActive(true);
                    stage3ToStage2.SetActive(false);

                    // 停止
                    if (isStop)
                    {
                        
                        _rb2d.velocity = new Vector2(0, 0);
                    }
                    // X軸プラス方向に移動
                    else
                    {
                        stage2_PopUp.SetBool("isStart", false);
                        _sp.flipX = false;
                        _rb2d.velocity = new Vector2(speed, _rb2d.velocity.y);
                    }
                    break;
            }
        }

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
                // ステージ2 → ステージ1に戻る
                case 0:
                    stage1ToStage2.SetActive(false);
                    stage2ToStage1.SetActive(true);

                    // 停止
                    if (isStop)
                    {
                        _rb2d.velocity = new Vector2(0, 0);
                    }
                    // X軸マイナス方向に移動
                    else
                    {
                        stage2_PopUp.SetBool("isStart", false);
                        _sp.flipX = true;
                        _rb2d.velocity = new Vector2(-speed, _rb2d.velocity.y);
                    }
                    break;

                // ステージ3 → ステージ2に戻る
                case 1:
                    stage2ToStage3.SetActive(false);
                    stage3ToStage2.SetActive(true);

                    // 停止
                    if (isStop)
                    {
                        _rb2d.velocity = new Vector2(0, 0);
                    }
                    // X軸マイナス方向に移動
                    else
                    {
                        stage3_PopUp.SetBool("isStart", false);
                        _sp.flipX = true;
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

        // ステージ1 到着
        if (collision.gameObject.name == "StopPoint_Area1")
        {
            stage1_PopUp.SetBool("isStart", true);
            _rb2d.velocity = new Vector2(0, 0);
            isMoveNow = false;
            isStop = true;
            minusMove = false;
        }

        // ステージ2 到着
        if (collision.gameObject.name == "StopPoint_Area2")
        {
            stage2_PopUp.SetBool("isStart", true);
            _rb2d.velocity = new Vector2(0, 0);
            isMoveNow = false;
            isStop = true;
            plusMove = false;
            minusMove = false;
        }

        // ステージ3 到着
        if (collision.gameObject.name == "StopPoint_Area3")
        {
            stage3_PopUp.SetBool("isStart", true);
            _rb2d.velocity = new Vector2(0, 0);
            isMoveNow = false;
            isStop = true;
            plusMove = false;
            minusMove = false;
        }
    }
}