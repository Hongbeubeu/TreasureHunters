using Ultimate.Core.Runtime.FSM;
using UnityEngine;

public class AllyMeleeBehaviour : AIBehaviour
{
    protected FSMState idleState, attackState, moveState;
    protected AttackAction attackAction;
    protected AllyIdleAction idleAction;
    protected AllyMoveAction moveAction;

    public AiState CurrentState { get; private set; }

    public AllyMeleeBehaviour(GameCharacter _aiController) : base(_aiController)
    {
        Init();
    }

    protected override void InitFSM()
    {
        fsm = new FSM("Melly Ally FSM");

        idleState = fsm.AddState((int)AiState.Idle);
        attackState = fsm.AddState((int)AiState.Attack);
        moveState = fsm.AddState((int)AiState.Move);

        idleAction = new AllyIdleAction(idleState, aiController, this);
        moveAction = new AllyMoveAction(idleState, aiController, this);
        attackAction = new AttackAction(idleState, aiController, this);
    }

    public override void ChangeState(AiState _newState)
    {
        if ((int)_newState == fsm.GetCurrentState())
        {
            return;
        }

        CurrentState = _newState;

        switch (_newState)
        {
            case AiState.Idle:
                fsm.ChangeToState(idleState);
                break;
            case AiState.Attack:
                fsm.ChangeToState(attackState);
                break;
            case AiState.Move:
                fsm.ChangeToState(moveState);
                break;
            default:
                Debug.LogError($"FSM does not contain state {_newState.ToString()}");
                break;
        }
    }

    public override void StartCalculating()
    {
        fsm.Start((int)AiState.Idle);
    }

    public override void Dispose()
    {
        base.Dispose();
        attackAction = null;
        idleAction = null;
        moveAction = null;
        attackState = null;
        idleState = null;
        moveState = null;
    }
}