using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public float id;
    public GameObject stonePoint;

    private SpriteRenderer _sr;
    private BoxCollider2D _cl2d;

    bool _srON;

    void Start()
    {
        if (id <= 2)
        {
            _sr = GetComponent<SpriteRenderer>();
            if(id == 2)
            {
                _cl2d = GetComponent<BoxCollider2D>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(id)
        {
            case 0:
                if (this.transform.localPosition.y <= -7)
                {
                    Destroy(this.gameObject);
                }
                break;

            case 1:
                if(this.transform.position.y <= stonePoint.transform.position.y)
                {
                    Destroy(this.gameObject);
                }
                break;

            case 2:
                if (this.transform.position.y >= stonePoint.transform.position.y)
                {
                    _sr.enabled = true;
                    _cl2d.enabled = true;
                    _srON = true;
                    id = 3;
                }
                else
                {
                    _sr.enabled = false;
                    _cl2d.enabled = false;
                }
                break;

            case 3:
                break;
        }
    }
}
