using UnityEngine;

public class ChangeGrid : MonoBehaviour
{
    [Header("タイルのオブジェクト")]
    public GameObject famiconTile;
    public GameObject segaTile;
    public GameObject dsTile;
    public GameObject switchTile;

    private int changeTileCount;

    void Start()
    {
        famiconTile.SetActive(true);
        segaTile.SetActive(false);
        dsTile.SetActive(false);
        switchTile.SetActive(false);
    }

    void Update()
    {
        changeTileCount = FindObjectOfType<PlayerControl>()._modelNumber;
        
        switch(changeTileCount)
        {
            case 0:
                switchTile.SetActive(false);
                famiconTile.SetActive(true);
                break;

            case 1:
                famiconTile.SetActive(false);
                segaTile.SetActive(true);
                break;

            case 2:
                segaTile.SetActive(false);
                dsTile.SetActive(true);
                break;

            case 3:
                dsTile.SetActive(false);
                switchTile.SetActive(true);
                break;
        }
    }
}