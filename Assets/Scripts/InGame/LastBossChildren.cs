using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossChildren : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<LastBossControl>().dead)
        {
            Destroy(this.gameObject);
        }
    }
}
