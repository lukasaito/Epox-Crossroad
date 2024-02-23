using UnityEngine;
using UnityEngine.UI;

public class UI_CoinCount : MonoBehaviour
{
    public Sprite[] numberSprites; 
    public Image tensPlaceImage;
    public Image onesPlaceImage;

    private int tensPlace = 0; // �\�̈ʂ̐���
    private int onesPlace = 0; // ��̈ʂ̐���

    private const int maxCoins = 99;

    private int timeNumber;

    public GameObject ui_Famicon;
    public GameObject ui_Sega;
    public GameObject ui_3DS;
    public GameObject ui_Switch;

    void Start()
    {
        ui_Famicon.SetActive(true);
        ui_3DS.SetActive(false);
        ui_Sega.SetActive(false);
        ui_Switch.SetActive(false);

        // �\�̈ʂ̉摜���X�V
        tensPlaceImage.sprite = numberSprites[tensPlace];

        // ��̈ʂ̉摜���X�V
        onesPlaceImage.sprite = numberSprites[onesPlace];
    }

    void Update()
    {
        timeNumber = FindObjectOfType<PlayerControl>()._modelNumber;

        switch(timeNumber)
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
    }

    // �R�C�����l���������̏���
    public void EarnCoin()
    {
        // ��̈ʂ����Z
        onesPlace++;

        // ��̈ʂ�9�ɂȂ����ꍇ
        if (onesPlace > 9)
        {
            // ��̈ʂ�0�ɖ߂�
            onesPlace = 0;

            // �\�̈ʂ����Z
            tensPlace++;

            // �\�̈ʂ�9�ɂȂ����ꍇ�A99���̕\������𒴂��Ȃ��悤��
            if (tensPlace > 9)
            {
                tensPlace = 9;
                onesPlace = 9;
            }
        }

        // �����̉摜���X�V
        UpdateNumberImages();
    }

    // �����̉摜���X�V
    private void UpdateNumberImages()
    {
        // �\�̈ʂ̉摜���X�V
        tensPlaceImage.sprite = numberSprites[tensPlace];

        // ��̈ʂ̉摜���X�V
        onesPlaceImage.sprite = numberSprites[onesPlace];
    }
}