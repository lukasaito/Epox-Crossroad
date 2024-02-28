using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Scroll : MonoBehaviour
{
    public float scrollSpeed;

    void Update()
    {
        transform.Translate(-scrollSpeed * Time.deltaTime, 0, 0);

        if(transform.position.x < -19.2f)
        {
            transform.position = new Vector3(19.2f, 0, 0);
        }
    }
}
