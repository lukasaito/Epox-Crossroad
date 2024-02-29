using UnityEngine;

public class TimeChangeEffect : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;

    private float endTime;
    private bool isStart;

    public GameObject a;
    public GameObject b;

    void Start()
    {
        a.SetActive(false);
        b.SetActive(false);

        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(isStart)
        {
            _animator.SetBool("isStart", true);
            endTime += Time.deltaTime;

            if(endTime@> 1.0f)
            {
                _animator.SetBool("isStart", false);
                isStart = false;
                endTime = 0.0f;
            }
        }
        else
        {
            a.SetActive(false);
            b.SetActive(false);
        }
    }

    public void isTimeChange()
    {
        _audioSource.Play();
        a.SetActive(true);
        b.SetActive(true);
        isStart = true;
    }
}