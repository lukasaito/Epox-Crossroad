using UnityEngine;

public class TimeMemory : MonoBehaviour
{
    private SpriteRenderer _sp;
    private BoxCollider2D _boxCol2d;
    private AudioSource _source;
    private Animator _animator;

    private int changeTimeMemory = 0;

    void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
        _boxCol2d = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        changeTimeMemory = FindObjectOfType<PlayerControl>()._modelNumber;
        _animator.SetInteger("ChangeTimeMemory", changeTimeMemory);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<UI_TimeMemory>().GetTimeMemory();
            _sp.enabled = false;
            _boxCol2d.enabled = false;
            Destroy(this.gameObject, 1.0f);
        }
    }
}