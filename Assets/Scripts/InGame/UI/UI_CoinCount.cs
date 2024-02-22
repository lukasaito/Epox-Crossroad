using UnityEngine;
using UnityEngine.UI;

public class UI_CoinCount : MonoBehaviour
{
    private Text _text;
    public int coinCount;

    void Start()
    {
        _text = GetComponent<Text>();
    }

    void Update()
    {
        _text.text = "ÉRÉCÉì : " + coinCount;
    }
}