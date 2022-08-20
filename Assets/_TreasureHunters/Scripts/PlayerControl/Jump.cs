using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int _maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f;

    private Rigidbody2D _body;
    private CharacterGround _ground;
    private PlayerController _controller;
    private Vector2 _velocity;

    private int _jumpPhase;
    private float _jumpSpeed;

    private bool _desiredJump, _onGround;

    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<CharacterGround>();
        _controller = GetComponent<PlayerController>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _desiredJump = true;
        }

        if (context.canceled)
        {
            _desiredJump = false;
        }
    }

    private void FixedUpdate()
    {
        _onGround = _ground.GetOnGround();
        _controller.animator.SetBool("IsOnGround", _onGround);
        if (_controller.animator.GetBool("IsFalling"))
        {
            _controller.animator.SetBool("IsFalling", !_onGround);
        }


        _velocity = _body.velocity;
        if (!_onGround && _velocity.y <= 0)
        {
            _controller.animator.SetBool("IsFalling", true);
            _controller.animator.SetBool("IsJumping", false);
        }

        if (_onGround)
        {
            _jumpPhase = 0;
        }

        if (_desiredJump)
        {
            _desiredJump = false;
            JumpAction();
        }

        if (_body.velocity.y > 0)
        {
            _body.gravityScale = _upwardMovementMultiplier;
        }
        else if (_body.velocity.y <= 0)
        {
            _body.gravityScale = _downwardMovementMultiplier;
        }

        _body.velocity = _velocity;
    }

    private void JumpAction()
    {
        if (!_onGround && _jumpPhase >= _maxAirJumps) return;
        _jumpPhase += 1;

        var multi = 1f;
        if (_jumpPhase >= 2)
        {
            multi = 0.5f;
            float _t = 0.2f;
            var newGravity = new Vector2(0, -2 * _jumpHeight * multi / (_t * _t));
            _body.gravityScale = newGravity.y / Physics2D.gravity.y;
        }

        _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight * multi);

//        if (_velocity.y > 0f)
//        {
//            _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
//        }
//        else if (_velocity.y < 0f)
//        {
//            _jumpSpeed += Mathf.Abs(_body.velocity.y);
//        }
        _velocity.y = 0f;
        _velocity.y += _jumpSpeed;
        _controller.animator.SetBool("IsJumping", true);
    }
}