using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ChangeMove : MonoBehaviour
{
    private VideoPlayer _videoPlayer;

    private float nextSceneTime;
    private bool nextScene;
    public VideoClip pressMove;

    public AudioSource _audio;

    [SerializeField]
    private float _minusVolume;

    [SerializeField]
    private float plusVolume;

    public GameObject titleLogo;
    public GameObject pressButton;
    public GameObject setting;

    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            nextScene = true;
            _videoPlayer.clip = pressMove;
            _videoPlayer.isLooping = false;
        }

        if(nextScene)
        {
            _audio.volume -= _minusVolume * Time.deltaTime;
            nextSceneTime += Time.deltaTime;

            if(nextSceneTime > 2.0f)
            {
                titleLogo.SetActive(false);
                pressButton.SetActive(false);
                setting.SetActive(false);
            }

            if(nextSceneTime > 4.0f)
            {
                SceneManager.LoadScene("Tutorial");
            }
        }
    }
}