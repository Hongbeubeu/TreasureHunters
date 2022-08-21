using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;
    [SerializeField, Range(0f, 2f)] private float friction = 0f;
    [SerializeField] private GameCharacter gameCharacter;
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] public Transform trans;
    private Vector2 direction, desiredVelocity;
    public Vector2 velocity;
    private float maxSpeedChange, acceleration;
    private bool isOnGround;

    public void OnMovement(InputAction.CallbackContext context)
    {
        direction.x = context.ReadValue<float>();
    }


    private void Update()
    {
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - friction, 0f);
        if (gameCharacter.characterAction.IsAttacking())
        {
            desiredVelocity.x = 0;
        }

        gameCharacter.characterAction.SetSpeed(Mathf.Abs(desiredVelocity.x));
        UpdateCharacterDirection();
    }

    private void UpdateCharacterDirection()
    {
        var scale = trans.localScale;
        if (desiredVelocity.x > 0)
        {
            scale.x = 1;
        }
        else if (desiredVelocity.x < 0)
        {
            scale.x = -1;
        }

        trans.localScale = scale;
    }

    private void FixedUpdate()
    {
        isOnGround = groundChecker.GetOnGround();
        velocity = gameCharacter.body.velocity;

        acceleration = isOnGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        gameCharacter.body.velocity = velocity;
    }
}