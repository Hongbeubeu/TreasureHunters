using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    [SerializeField] protected GameCharacter gameCharacter;
    [SerializeField] public Animator animator;

    public void Init()
    {
    }

    public void SetSpeed(float _speed)
    {
        animator.SetFloat(AnimatorParameters.Speed, _speed);
    }

    public void SetAttack1()
    {
        animator.SetTrigger(AnimatorParameters.Attack1);
    }

    public void SetAttack2()
    {
        animator.SetTrigger(AnimatorParameters.Attack2);
    }

    public void SetAttack3()
    {
        animator.SetTrigger(AnimatorParameters.Attack3);
    }

    public void SetAirAttack1()
    {
        animator.SetTrigger(AnimatorParameters.AirAttack1);
    }

    public void SetOnGround(bool _isOnGround)
    {
        animator.SetBool(AnimatorParameters.IsOnGround, _isOnGround);
    }

    public void SetJumping(bool _isJumping)
    {
        animator.SetBool(AnimatorParameters.IsJumping, _isJumping);
    }

    public void SetFalling(bool _isFalling)
    {
        animator.SetBool(AnimatorParameters.IsFalling, _isFalling);
    }

    public void SetAttacking(bool _isAttacking)
    {
        animator.SetBool(AnimatorParameters.IsAttaking, _isAttacking);
    }

    public bool IsAttacking()
    {
        return animator.GetBool(AnimatorParameters.IsAttaking);
    }
}