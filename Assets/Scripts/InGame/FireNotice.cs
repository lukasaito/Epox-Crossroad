using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNotice : MonoBehaviour
{
    SpriteRenderer _sr;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _sr.color += new Color(0, 0, 0, 0.8f * Time.deltaTime);
    }
}
