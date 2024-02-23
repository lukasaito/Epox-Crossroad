using UnityEngine;
using UnityEngine.UI;

public class UI_CoinCount : MonoBehaviour
{
    public Sprite[] numberSprites; 
    public Image tensPlaceImage;
    public Image onesPlaceImage;

    private int tensPlace = 0; // 十の位の数字
    private int onesPlace = 0; // 一の位の数字

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

        // 十の位の画像を更新
        tensPlaceImage.sprite = numberSprites[tensPlace];

        // 一の位の画像を更新
        onesPlaceImage.sprite = numberSprites[onesPlace];
    }

    void Update()
    {
        timeNumber = FindObjectOfType<PlayerControl>()._modelNumber;

        switch(timeNumber)
        {
            // ファミコン時代
            case 0:
                ui_Switch.SetActive(false);
                ui_Famicon.SetActive(true);
                break;

            // セガサターン時代
            case 1:
                ui_Famicon.SetActive(false);
                ui_Sega.SetActive(true);
                break;

            // 3DS時代
            case 2:
                ui_Sega.SetActive(false);
                ui_3DS.SetActive(true);
                break;

            // Switch時代
            case 3:
                ui_3DS.SetActive(false);
                ui_Switch.SetActive(true);
                break;
        }
    }

    // コインを獲得した時の処理
    public void EarnCoin()
    {
        // 一の位を加算
        onesPlace++;

        // 一の位が9になった場合
        if (onesPlace > 9)
        {
            // 一の位を0に戻す
            onesPlace = 0;

            // 十の位を加算
            tensPlace++;

            // 十の位が9になった場合、99枚の表示上限を超えないように
            if (tensPlace > 9)
            {
                tensPlace = 9;
                onesPlace = 9;
            }
        }

        // 数字の画像を更新
        UpdateNumberImages();
    }

    // 数字の画像を更新
    private void UpdateNumberImages()
    {
        // 十の位の画像を更新
        tensPlaceImage.sprite = numberSprites[tensPlace];

        // 一の位の画像を更新
        onesPlaceImage.sprite = numberSprites[onesPlace];
    }
}