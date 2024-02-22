using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHitPoint : MonoBehaviour
{
    // �v���C���[���C�t�摜
    public GameObject famiconLife;
    public GameObject segaSaturnLife;
    public GameObject dsLife;
    public GameObject switchLife;

    void Update()
    {
        switch(FindObjectOfType<PlayerControl>()._modelNumber)
        {
            // �t�@�~�R������
            case 0:
                switchLife.SetActive(false);
                famiconLife.SetActive(true);
                break;

            // �Z�K�T�^�[������
            case 1:
                famiconLife.SetActive(false);
                segaSaturnLife.SetActive(true);
                break;

            //3DS����
            case 2:
                segaSaturnLife.SetActive(false);
                dsLife.SetActive(true);
                break;

            // switch����
            case 3:
                dsLife.SetActive(false);
                switchLife.SetActive(true);
                break;
        }
    }
}