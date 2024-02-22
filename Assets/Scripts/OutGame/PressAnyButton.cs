using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyButton : MonoBehaviour
{
    private int seSuounCounter;
    private Animator animator;
    public AudioSource source;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            if (0 <= seSuounCounter)
            {
                animator.SetBool("InputAnim", true);
                source.Play();
            }

            seSuounCounter++;
        }
    }
}