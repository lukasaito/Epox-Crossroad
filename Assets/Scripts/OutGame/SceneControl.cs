using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    private float _nextTitleTime;
    public bool isSkipOn = false;  // スキップ可能状態か否か
    public float skipTime = 0.0f;  // 入力からの経過時間

    public int loadingNumber = 0;
    private float _nextTime;

    public float onButton;
    public bool isNextScene;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        // ファンクションキー割り当て--------------------------------
        if(Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("Logo");
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("Title");
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene("Loading");
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("Story");
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene("StageSelect");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Stage_01");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Stage_02");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("Stage_03");
        }
        // ファンクションキー割り当て--------------------------------


        // 現在のシーン名が「Logo」の場合
        if (SceneManager.GetActiveScene().name == "Logo")
        {
            _nextTitleTime += Time.deltaTime;

            // 3秒経過でタイトルに遷移
            if(_nextTitleTime >= 3.0f)
            {
                SceneManager.LoadScene("Title");
            }
        }

        // 現在のシーン名が「Title」の場合
        if(SceneManager.GetActiveScene().name == "Title")
        {
            onButton += Time.deltaTime;

            if(onButton >= 1.0f)
            {
                //何かのキーが入力される
                if (Input.anyKeyDown)
                {
                    isNextScene = true;
                }

                if(isNextScene)
                {
                    if (FindObjectOfType<FadeImageControl>().nextTime >= 1.3f)
                    {
                        // 「Story」に遷移する
                        // SceneManager.LoadScene("Story");

                        SceneManager.LoadScene("Loading");
                    }
                }
            }
        }

        if(SceneManager.GetActiveScene().name == "Loading")
        {
            switch(loadingNumber)
            {
                case 0:
                    _nextTime += Time.deltaTime;

                    if(_nextTime >= 3.0f)
                    {
                        SceneManager.LoadScene("StageSelect");
                    }
                    break;
            }
        }

        // 現在のシーン名が「Story」の場合
        if (SceneManager.GetActiveScene().name == "Story")
        {
            if(Input.anyKeyDown)
            {
                // スキップ可能状態にする。
                isSkipOn = true;
            }

            if(isSkipOn)
            {
                if(Input.anyKey)
                {
                    skipTime += Time.deltaTime;
                }

                if(skipTime > 2.0f)
                {
                    SceneManager.LoadScene("StageSelect");
                }
            }
        }

        // 現在のscene名が「GameClear」の場合
        if (SceneManager.GetActiveScene().name == "GameClear")
        {
            //何かのキーが入力される
            if (Input.GetKeyDown(KeyCode.A))
            {
                SceneManager.LoadScene("Title");
            }
        }
    }
}