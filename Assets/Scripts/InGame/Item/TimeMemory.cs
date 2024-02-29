using UnityEngine;

public class TimeMemory : MonoBehaviour
{
    private SpriteRenderer _sp;
    private BoxCollider2D _boxCol2d;
    private AudioSource _source;
    private Animator _animator;

    private int changeTimeMemory = 0;

    public GameObject effect_GetTimeMemory;

    void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
        _boxCol2d = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
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
            Instantiate(effect_GetTimeMemory, transform.position, transform.rotation);
            FindObjectOfType<UI_TimeMemory>().GetTimeMemory();
            _sp.enabled = false;
            _boxCol2d.enabled = false;
            _source.Play();
            Destroy(this.gameObject, 1.0f);
        }
    }
}