using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    private Animator _animator;
    private int changeOpenDoor;

    void Start()
    {
        _animator = GetComponent<Animator>();        
    }

    void Update()
    {
        changeOpenDoor = FindObjectOfType<PlayerControl>()._modelNumber;
        _animator.SetInteger("ChangeOpenDoor", changeOpenDoor);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //SceneManager.LoadScene("");
        }
    }
}