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
        // ���݂̃V�[�������uTitle�v�̏ꍇ
        if (SceneManager.GetActiveScene().name == "Title")
        {
            onButton += Time.deltaTime;

            if (onButton >= 1.0f)
            {
                //�����̃L�[�����͂����
                if (Input.anyKeyDown)
                {
                    nextProcessing = true;
                    
                }
            }

            // ���̏����t���O��ON�ɂȂ�����
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