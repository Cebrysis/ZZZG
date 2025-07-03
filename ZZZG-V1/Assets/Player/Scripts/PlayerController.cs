using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    private enum Directions
    {
        DOWN,
        UP,
        LEFT,
        RIGHT,
        UP_LEFT,
        UP_RIGHT
    }


    [Header("Movement")]
    [SerializeField] float _moveSpeed = 50f;

    [Header("Dependencies")]
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;

    private Vector2 _moveDir = Vector2.zero;
    private Directions _facingDirection = Directions.DOWN;

    // Animator parameter hashes
    private readonly int _animMoveRight = Animator.StringToHash("Anim_Player_Move_Right");
    private readonly int _animIdleRight = Animator.StringToHash("Anim_Player_Idle_Right");
    private readonly int _animMoveLeft = Animator.StringToHash("Anim_Player_Move_Left");
    private readonly int _animIdleLeft = Animator.StringToHash("Anim_Player_Idle_Left");
    private readonly int _animMoveUp = Animator.StringToHash("Anim_Player_Move_Up");
    private readonly int _animIdleUp = Animator.StringToHash("Anim_Player_Idle_Up");
    private readonly int _animMoveDown = Animator.StringToHash("Anim_Player_Move_Down");
    private readonly int _animIdleDown = Animator.StringToHash("Anim_Player_Idle_Down");
    private readonly int _animMoveUpLeft = Animator.StringToHash("Anim_Player_Move_LeftUp");
    private readonly int _animIdleUpLeft = Animator.StringToHash("Anim_Player_Idle_LeftUp");
    private readonly int _animMoveUpRight = Animator.StringToHash("Anim_Player_Move_RightUp");
    private readonly int _animIdleUpRight = Animator.StringToHash("Anim_Player_Idle_RightUp");


    private void Update()
    {
        GatherInput();
    }

    private void GatherInput()
    {
        _moveDir.x = Input.GetAxisRaw("Horizontal");
        _moveDir.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        MovementUpdate();
        CalculateFacingDirection();
        UpdateAnimation();
    }

    private void MovementUpdate()
    {
        _rb.velocity = _moveDir.normalized * _moveSpeed * Time.fixedDeltaTime;
    }

    private void CalculateFacingDirection()
    {
        if (_moveDir.sqrMagnitude == 0)
            return;

        float x = _moveDir.x;
        float y = _moveDir.y;

        if (x > 0 && y > 0)
            _facingDirection = Directions.UP_RIGHT;
        else if (x < 0 && y > 0)
            _facingDirection = Directions.UP_LEFT;
        else if (x > 0)
            _facingDirection = Directions.RIGHT;
        else if (x < 0)
            _facingDirection = Directions.LEFT;
        else if (y > 0)
            _facingDirection = Directions.UP;
        else if (y < 0)
            _facingDirection = Directions.DOWN;
    }


    private void UpdateAnimation()
    {
        bool isMoving = _moveDir.sqrMagnitude > 0;

        switch (_facingDirection)
        {
            case Directions.RIGHT:
                _spriteRenderer.flipX = false;
                _animator.CrossFade(isMoving ? _animMoveRight : _animIdleRight, 0);
                break;

            case Directions.LEFT:
                _spriteRenderer.flipX = false;
                _animator.CrossFade(isMoving ? _animMoveLeft : _animIdleLeft, 0);
                break;

            case Directions.UP:
                _spriteRenderer.flipX = false;
                _animator.CrossFade(isMoving ? _animMoveUp : _animIdleUp, 0);
                break;

            case Directions.DOWN:
                _spriteRenderer.flipX = false;
                _animator.CrossFade(isMoving ? _animMoveDown : _animIdleDown, 0);
                break;

            case Directions.UP_RIGHT:
                _spriteRenderer.flipX = false;
                _animator.CrossFade(isMoving ? _animMoveUpRight : _animIdleUpRight, 0);
                break;

            case Directions.UP_LEFT:
                _spriteRenderer.flipX = false;
                _animator.CrossFade(isMoving ? _animMoveUpLeft : _animIdleUpLeft, 0);
                break;
        }
    }
}
