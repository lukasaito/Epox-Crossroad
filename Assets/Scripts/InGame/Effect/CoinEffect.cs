using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffect : MonoBehaviour
{
    void Update()
    {
        Destroy(this.gameObject, 1.0f);
    }
}
