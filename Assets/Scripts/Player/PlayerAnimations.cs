using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _anim;
    private PlayerMovement _player;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _player = GetComponent<PlayerMovement>();

        _player.OnMovement += Player_OnMovement;
        _player.OnRotate += Player_OnRotate;
    }

    private void Player_OnMovement(float input)
    {
        _anim.SetFloat("Speed", Mathf.Abs(input));
    }

    private void Player_OnRotate(Vector2Int direction)
    {
        _anim.SetInteger("LookY", direction.y);
        _anim.SetInteger("LookX", Mathf.Abs(direction.x));
    }

}
