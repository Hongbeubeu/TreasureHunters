using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameCharacter gameCharacter;
    private float atkTime1 = 0.5f;
    private float atkTime2 = 0.5f;
    private float atkTime3 = 0.5f;
    private float atkAirTime1 = 0f;
    private float attackCooldown;

    public bool isAttacking;

    public bool IsAttacking
    {
        get => isAttacking;
        set
        {
            isAttacking = value; 
            gameCharacter.characterAction.SetAttacking(isAttacking);
        }
    }

    private void Awake()
    {
        var animationClips = gameCharacter.characterAction.animator.runtimeAnimatorController.animationClips;
        for (int i = 0; i < animationClips.Length; i++)
        {
            if (animationClips[i].name == "Attack1")
            {
                atkTime1 = animationClips[i].length;
            }

            if (animationClips[i].name == "Attack2")
            {
                atkTime2 = animationClips[i].length;
            }

            if (animationClips[i].name == "Attack3")
            {
                atkTime3 = animationClips[i].length;
            }

            if (animationClips[i].name == "AirAttack1" || animationClips[i].name == "AirAttack2")
            {
                atkAirTime1 += animationClips[i].length;
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.started || attackCooldown > 0) return;
        IsAttacking = true;
        var rand = Random.Range(0, 1f);
        if (gameCharacter.IsOnGround())
        {
            if (rand < 0.4f)
            {
                gameCharacter.characterAction.SetAttack1();
                attackCooldown = atkTime1;
                return;
            }

            if (rand < 0.7f)
            {
                gameCharacter.characterAction.SetAttack2();
                attackCooldown = atkTime2;
                return;
            }

            gameCharacter.characterAction.SetAttack3();
            attackCooldown = atkTime3;
        }
        else
        {
            gameCharacter.characterAction.SetAirAttack1();
            attackCooldown = atkAirTime1;
        }
    }

    private void Update()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        else
        {
            IsAttacking = false;
        }
    }
}