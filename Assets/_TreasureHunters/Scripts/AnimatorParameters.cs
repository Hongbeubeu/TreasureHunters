using UnityEngine;

public class AnimatorParameters
{
    public static readonly int IsOnGround = Animator.StringToHash("IsOnGround");
    public static readonly int Speed = Animator.StringToHash("Speed");
    public static readonly int IsJumping = Animator.StringToHash("IsJumping");
    public static readonly int IsFalling = Animator.StringToHash("IsFalling");
    public static readonly int Attack1 = Animator.StringToHash("Attack1");
    public static readonly int Attack2 = Animator.StringToHash("Attack2");
    public static readonly int Attack3 = Animator.StringToHash("Attack3");
    public static readonly int IsAttaking = Animator.StringToHash("IsAttacking");
    public static readonly int AirAttack1 = Animator.StringToHash("AirAttack1");
}