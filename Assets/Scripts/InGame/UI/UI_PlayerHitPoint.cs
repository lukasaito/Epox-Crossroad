using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHitPoint : MonoBehaviour
{
    // プレイヤーライフ画像
    public GameObject famiconLife;
    public GameObject segaSaturnLife;
    public GameObject dsLife;
    public GameObject switchLife;

    void Update()
    {
        switch(FindObjectOfType<PlayerControl>()._modelNumber)
        {
            // ファミコン時代
            case 0:
                switchLife.SetActive(false);
                famiconLife.SetActive(true);
                break;

            // セガサターン時代
            case 1:
                famiconLife.SetActive(false);
                segaSaturnLife.SetActive(true);
                break;

            //3DS時代
            case 2:
                segaSaturnLife.SetActive(false);
                dsLife.SetActive(true);
                break;

            // switch時代
            case 3:
                dsLife.SetActive(false);
                switchLife.SetActive(true);
                break;
        }
    }
}