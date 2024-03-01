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
    public int selectNumber;      // 0 = �`�F�b�N�|�C���g�ɑJ��  / 1 = �^�C�g���֑J��

    void Start()
    {
        backTitleMark.SetActive(false);
    }
    
    void Update()
    {
        // �J�ڏ�Ԃł͖�����
        if(!nextScene)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                // 1�������̏ꍇ
                if (selectNumber < 1)
                {
                    selectNumber++;
                }
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // 0������̏ꍇ
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

        // �J�ڏ�ԂɂȂ�����
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
                        // �^�C�g���ɑJ��
                        SceneManager.LoadScene("Title");
                    }
                    break;
            }
        }
    }
}