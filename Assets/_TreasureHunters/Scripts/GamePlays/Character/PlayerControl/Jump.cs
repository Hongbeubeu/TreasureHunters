using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;
    [SerializeField] private GameCharacter gameCharacter;

    private Vector2 velocity;

    private int jumpPhase;
    private float jumpSpeed;

    private bool desiredJump, isOnGround;

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            desiredJump = true;
        }

        if (context.canceled)
        {
            desiredJump = false;
        }
    }

    private void Update()
    {
        isOnGround = gameCharacter.IsOnGround();
        gameCharacter.characterAction.SetOnGround(isOnGround);
        if (isOnGround)
        {
            gameCharacter.characterAction.SetFalling(!isOnGround);
        }
    }

    private void FixedUpdate()
    {
        velocity = gameCharacter.body.velocity;
        if (!isOnGround && velocity.y <= 0)
        {
            gameCharacter.characterAction.SetFalling(true);
            gameCharacter.characterAction.SetJumping(false);
        }

        if (isOnGround)
        {
            jumpPhase = 0;
        }

        if (desiredJump)
        {
            desiredJump = false;
            JumpAction();
        }

        if (gameCharacter.body.velocity.y > 0)
        {
            gameCharacter.body.gravityScale = upwardMovementMultiplier;
        }
        else if (gameCharacter.body.velocity.y <= 0)
        {
            gameCharacter.body.gravityScale = downwardMovementMultiplier;
        }

        gameCharacter.body.velocity = velocity;
    }

    private void JumpAction()
    {
        if (!isOnGround && jumpPhase >= maxAirJumps) return;
        jumpPhase += 1;

        var multi = 1f;
        if (jumpPhase >= 2)
        {
            multi = 0.5f;
            float _t = 0.2f;
            var newGravity = new Vector2(0, -2 * jumpHeight * multi / (_t * _t));
            gameCharacter.body.gravityScale = newGravity.y / Physics2D.gravity.y;
        }

        jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight * multi);

//        if (_velocity.y > 0f)
//        {
//            _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
//        }
//        else if (_velocity.y < 0f)
//        {
//            _jumpSpeed += Mathf.Abs(_body.velocity.y);
//        }
        velocity.y = 0f;
        velocity.y += jumpSpeed;
        gameCharacter.characterAction.SetJumping(true);
    }
}