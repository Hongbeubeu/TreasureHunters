using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;
    [SerializeField, Range(0f, 2f)] private float _friction = 0f;

    private Vector2 _direction, _desiredVelocity;
    private PlayerController _controller;
    public Vector2 _velocity;
    private Rigidbody2D _body;
    private CharacterGround _ground;

    private float _maxSpeedChange, _acceleration;
    private bool _onGround;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<CharacterGround>();
        _controller = GetComponent<PlayerController>();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _direction.x = context.ReadValue<float>();
    }


    private void Update()
    {
//        _direction.x = _controller.input.RetrieveMoveInput();
        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _friction, 0f);
        if (_controller.isAttacking)
        {
            _desiredVelocity.x = 0;
        }

        _controller.animator.SetFloat("Speed", Mathf.Abs(_desiredVelocity.x));
        var rotation = transform.eulerAngles;
        if (_desiredVelocity.x > 0)
        {
            rotation.y = 0;
        }
        else if (_desiredVelocity.x < 0)
        {
            rotation.y = 180;
        }

        transform.eulerAngles = rotation;
    }

    private void FixedUpdate()
    {
        _onGround = _ground.GetOnGround();
        _velocity = _body.velocity;

        _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

        _body.velocity = _velocity;
    }
}