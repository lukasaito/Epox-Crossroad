using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeImageControl : MonoBehaviour
{
    private Animator anim;
    public float nextTime;
    public float onButton;
    public bool nextProcessing = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 現在のシーン名が「Title」の場合
        if (SceneManager.GetActiveScene().name == "Title")
        {
            onButton += Time.deltaTime;

            if (onButton >= 1.0f)
            {
                //何かのキーが入力される
                if (Input.anyKeyDown)
                {
                    nextProcessing = true;
                    
                }
            }

            // 次の処理フラグがONになった時
            if (nextProcessing)
            {
                nextTime += Time.deltaTime;

                if(nextTime > 2.0f)
                {
                    anim.SetBool("FadeOut", true);
                }
            }
        }
    }
}