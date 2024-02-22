using UnityEngine;

public class Coin : MonoBehaviour
{
    private SpriteRenderer _sp;
    private CircleCollider2D _cc2d;
    private Animator _animator;
    public int changeCoin;

    private Vector3 normalScale = new Vector3(2.7f, 2.7f, 2.7f);

    [Header("コインエフェクト")]
    public GameObject segaCoinEffect;
    public GameObject dsCoinEffect;
    public GameObject switchCoinEffect;


    void Start()
    {
        this.transform.localScale = normalScale;

        _sp = GetComponent<SpriteRenderer>();
        _cc2d = GetComponent<CircleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        changeCoin = FindObjectOfType<PlayerControl>()._modelNumber;
        _animator.SetInteger("ChangeCoin", changeCoin);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _sp.enabled = false;
            _cc2d.enabled = false;
            FindObjectOfType<UI_CoinCount>().coinCount++;
            Destroy(this.gameObject, 1.0f);

            switch (changeCoin)
            {
                case 1:
                    Instantiate(segaCoinEffect, this.transform.position, this.transform.rotation);
                    break;

                case 2:
                    Instantiate(dsCoinEffect, this.transform.position, this.transform.rotation);
                    break;

                case 3:
                    Instantiate(switchCoinEffect, this.transform.position, this.transform.rotation);
                    break;
            }
        }
    }
}