using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    private float _nextTitleTime;
    public bool isSkipOn = false;  // �X�L�b�v�\��Ԃ��ۂ�
    public float skipTime = 0.0f;  // ���͂���̌o�ߎ���

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
        // �t�@���N�V�����L�[���蓖��--------------------------------
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
        // �t�@���N�V�����L�[���蓖��--------------------------------


        // ���݂̃V�[�������uLogo�v�̏ꍇ
        if (SceneManager.GetActiveScene().name == "Logo")
        {
            _nextTitleTime += Time.deltaTime;

            // 3�b�o�߂Ń^�C�g���ɑJ��
            if(_nextTitleTime >= 3.0f)
            {
                SceneManager.LoadScene("Title");
            }
        }

        // ���݂̃V�[�������uTitle�v�̏ꍇ
        if(SceneManager.GetActiveScene().name == "Title")
        {
            onButton += Time.deltaTime;

            if(onButton >= 1.0f)
            {
                //�����̃L�[�����͂����
                if (Input.anyKeyDown)
                {
                    isNextScene = true;
                }

                if(isNextScene)
                {
                    if (FindObjectOfType<FadeImageControl>().nextTime >= 1.3f)
                    {
                        // �uStory�v�ɑJ�ڂ���
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

        // ���݂̃V�[�������uStory�v�̏ꍇ
        if (SceneManager.GetActiveScene().name == "Story")
        {
            if(Input.anyKeyDown)
            {
                // �X�L�b�v�\��Ԃɂ���B
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

        // ���݂�scene�����uGameClear�v�̏ꍇ
        if (SceneManager.GetActiveScene().name == "GameClear")
        {
            //�����̃L�[�����͂����
            if (Input.GetKeyDown(KeyCode.A))
            {
                SceneManager.LoadScene("Title");
            }
        }
    }
}