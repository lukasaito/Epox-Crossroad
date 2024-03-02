using UnityEngine;

public class PlayerWalkEffect : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Animator _animator;
    public PlayerControl PlayerControl;
    public int changePlayerWalkEffect;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>(); 
    }

    void Update()
    {

        //0.45
        changePlayerWalkEffect = PlayerControl._modelNumber;
        _animator.SetInteger("ChangePlayerWalkEffect", changePlayerWalkEffect);

        if(PlayerControl._inputX != 0)
        {
            _sr.enabled = true;
        }
        else
        {
            _sr.enabled = false;
        }

        // プレイイヤーが反転している時
        if(PlayerControl._sr.flipX)
        {
            _sr.flipX = true;
            //this.transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y);
        }
        else
        {
            _sr.flipX = false;
           // this.transform.position = new Vector2(transform.position.x - 0.2f, transform.position.y);
        }
    }
}