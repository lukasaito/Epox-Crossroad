using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM_Manager : MonoBehaviour
{
    private AudioSource _audio;

    [SerializeField]
    private float _minusVolume;

    [SerializeField]
    private float plusVolume;

    private bool plusFlag;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _audio.volume = 0.0f;
    }

    void Update()
    {
        if(!plusFlag && _audio.volume < 0.7f)
        {
            _audio.volume += plusVolume;
        }
        if(_audio.volume >= 0.7f)
        {
            plusFlag = true;
        }


        if(FindObjectOfType<FadeImageControl>().nextTime >= 1.0f)
        {
            _audio.volume -= _minusVolume * Time.deltaTime;
        }
    }
}