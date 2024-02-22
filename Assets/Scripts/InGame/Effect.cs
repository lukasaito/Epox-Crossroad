using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    float destroyCount;
    public float destroyTime;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        destroyCount += Time.deltaTime;

        if(destroyCount >= destroyTime)
        {
            Destroy(this.gameObject);
        }
    }
}
