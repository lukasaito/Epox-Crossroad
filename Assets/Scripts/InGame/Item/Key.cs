using UnityEngine;

public class Key : MonoBehaviour
{
    private Animator _animator;
    private BoxCollider2D _boxcol2d;
    private SpriteRenderer _sp;

    private int changeKey = 0;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _boxcol2d = GetComponent<BoxCollider2D>();
        _sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        changeKey = FindObjectOfType<PlayerControl>()._modelNumber;
        _animator.SetInteger("ChangeKey", changeKey);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _boxcol2d.enabled = false;
            _sp.enabled = false;
            Destroy(this.gameObject, 1.0f);
        }
    }
}