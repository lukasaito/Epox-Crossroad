using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_02_Gimmick : MonoBehaviour
{
    public GameObject[] stone;
    int random;

    float count;
    float destroyTime;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Random.Range(10f, -10f);
        random = Random.Range(0, 15);

        count += Time.deltaTime;
        destroyTime += Time.deltaTime;

        if (count >= 0.05f)
        {
            Instantiate(stone[random], new Vector3(x, 8, 0), Quaternion.identity);
            count = 0;
        }

        if(destroyTime >= 3)
        {
            Destroy(this.gameObject);
        }
    }
}
