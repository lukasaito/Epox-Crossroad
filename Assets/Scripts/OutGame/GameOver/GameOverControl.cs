using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverControl : MonoBehaviour
{
    public GameObject reStart;
    public GameObject reStartMark;
    public GameObject backTitle;
    public GameObject backTitleMark;

    public Animator reStartAnim;
    public Animator backTitleAnim;

    static public int checkPointCount;

    private bool nextScene;
    public float nextSceneTime;
    public int selectNumber;      // 0 = チェックポイントに遷移  / 1 = タイトルへ遷移

    void Start()
    {
        backTitleMark.SetActive(false);
    }
    
    void Update()
    {
        // 遷移状態では無い時
        if(!nextScene)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                // 1よりも下の場合
                if (selectNumber < 1)
                {
                    selectNumber++;
                }
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // 0よりも上の場合
                if (selectNumber > 0)
                {
                    selectNumber--;
                }
            }

            switch (selectNumber)
            {
                case 0:
                    backTitleMark.SetActive(false);
                    reStartMark.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
                    {
                        nextScene = true;
                    }
                    break;

                case 1:
                    reStartMark.SetActive(false);
                    backTitleMark.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
                    {
                        nextScene = true;
                    }
                    break;
            }
        }

        // 遷移状態になった時
        else
        {
            switch(selectNumber)
            {
                
                case 0:
                    nextSceneTime += Time.deltaTime;

                    if (nextSceneTime > 2.0f)
                    {
                        nextScene = false;
                    }
                    break;
                

                case 1:
                    nextSceneTime += Time.deltaTime;

                    if (nextSceneTime > 2.0f)
                    {
                        // タイトルに遷移
                        SceneManager.LoadScene("Title");
                    }
                    break;
            }
        }
    }
}