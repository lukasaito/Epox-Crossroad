using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    private Animator _animator;
    private int changeCloseDoor;

    public GameObject openDoor;
    public bool isOpenDoor;

    void Start()
    {
        _animator = GetComponent<Animator>();
        openDoor.SetActive(false);
    }

    void Update()
    {
        changeCloseDoor = FindObjectOfType<PlayerControl>()._modelNumber;
        _animator.SetInteger("ChangeCloseDoor", changeCloseDoor);

        if(isOpenDoor)
        {
            openDoor.SetActive(true);
            Destroy(this.gameObject, 1.0f);
        }
    }
}