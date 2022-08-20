using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    private float atkTime1 = 0.5f;
    private float atkTime2 = 0.5f;
    private float atkTime3 = 0.5f;
    private float atkAirTime1 = 0f;
    private float attackCooldown;

    public CharacterGround _OnGround;
    public bool isAttacking;

    private void Awake()
    {
        _OnGround = GetComponent<CharacterGround>();
        var animationClips = animator.runtimeAnimatorController.animationClips;
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
        animator.SetBool("IsAttacking", true);
        isAttacking = true;
        var rand = Random.Range(0, 1f);
        if (_OnGround.GetOnGround())
        {
            if (rand < 0.4f)
            {
                animator.SetTrigger("Attack1");
                attackCooldown = atkTime1;
                return;
            }

            if (rand < 0.7f)
            {
                animator.SetTrigger("Attack2");
                attackCooldown = atkTime2;

                return;
            }

            animator.SetTrigger("Attack3");
            attackCooldown = atkTime3;
        }
        else
        {
            animator.SetTrigger("AirAttack1");
            attackCooldown = atkAirTime1;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager.Instance.Test();
        }

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("IsAttacking", false);
            isAttacking = false;
        }
    }
}