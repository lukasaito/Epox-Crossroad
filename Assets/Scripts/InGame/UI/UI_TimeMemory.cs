using UnityEngine;
using UnityEngine.UI;

public class UI_TimeMemory : MonoBehaviour
{
    public Animator _1;
    public Animator _2;
    public Animator _3;

    public Image _1Image;
    public Image _2Image;
    public Image _3Image;

    public GameObject ui_Famicon;
    public GameObject ui_Sega;
    public GameObject ui_3DS;
    public GameObject ui_Switch;

    private int changeUITimeMemory = 0;

    void Start()
    {
        _1Image.fillAmount = 0.0f;
        _2Image.fillAmount = 0.0f;
        _3Image.fillAmount = 0.0f;

        ui_Famicon.SetActive(true);
        ui_3DS.SetActive(false);
        ui_Sega.SetActive(false);
        ui_Switch.SetActive(false);
    }

    void Update()
    {
        // �摜�̐؂�ւ�
        changeUITimeMemory = FindObjectOfType<PlayerControl>()._modelNumber;
        _1.SetInteger("ChangeUITimeMemory", changeUITimeMemory);
        _2.SetInteger("ChangeUITimeMemory", changeUITimeMemory);
        _3.SetInteger("ChangeUITimeMemory", changeUITimeMemory);

        switch (changeUITimeMemory)
        {
            // �t�@�~�R������
            case 0:
                ui_Switch.SetActive(false);
                ui_Famicon.SetActive(true);
                break;

            // �Z�K�T�^�[������
            case 1:
                ui_Famicon.SetActive(false);
                ui_Sega.SetActive(true);
                break;

            // 3DS����
            case 2:
                ui_Sega.SetActive(false);
                ui_3DS.SetActive(true);
                break;

            // Switch����
            case 3:
                ui_3DS.SetActive(false);
                ui_Switch.SetActive(true);
                break;
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            // 3�������Ă��鎞
            if (_3Image.fillAmount >= 1.0f)
            {
                _3Image.fillAmount += 0.0f;
            }

            // 2�������Ă��鎞
            if (_2Image.fillAmount >= 1.0f)
            {
                _2Image.fillAmount += 0.0f;
            }

            // 1�������Ă��鎞
            else if (_1Image.fillAmount >= 1.0f)
            {
                _1Image.fillAmount = 0.0f;
            }
        }
    }

    public void GetTimeMemory()
    {
        // 2�������Ă��鎞
        if (_2Image.fillAmount >= 1.0f)
        {
            _3Image.fillAmount += 0.5f;
        }

        // 1�������Ă��鎞
        else if (_1Image.fillAmount >= 1.0f)
        {
            _2Image.fillAmount += 0.5f;
        }

        // 1���������Ă��Ȃ���
        else if (_1Image.fillAmount < 1.0f)
        {
            _1Image.fillAmount += 0.5f;
        }

        if(_1Image.fillAmount >= 1.0f)
        {
            FindObjectOfType<PlayerControl>()._memoryCount++;
        }
        if (_2Image.fillAmount >= 1.0f)
        {
            FindObjectOfType<PlayerControl>()._memoryCount++;
        }
        if (_3Image.fillAmount >= 1.0f)
        {
            FindObjectOfType<PlayerControl>()._memoryCount++;
        }
    }
}