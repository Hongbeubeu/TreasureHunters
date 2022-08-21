using System;
using Ultimate.Core.Runtime.FSM;

public abstract class AIBehaviour : IDisposable
{
    protected GameCharacter aiController;
    protected FSM fsm;

    public AIBehaviour(GameCharacter _aiController)
    {
        this.aiController = _aiController;
    }

    public void Init()
    {
        InitFSM();
    }

    public void Update(float dt)
    {
        fsm.Update(dt);
    }

    #region FSM

    public AiState GetCurrentState()
    {
        return (AiState) fsm.GetCurrentState();
    }

    protected abstract void InitFSM();

    public abstract void ChangeState(AiState _newState);

    public abstract void StartCalculating();

    #endregion FSM

    public virtual void Dispose()
    {
        aiController = null;
        fsm.Destroy();
        fsm = null;
    }
}

public enum AiState
{
    Idle,
    Move,
    Attack,
}

public enum FSMTransition
{
    ToAttack,
    ToIdle,
    ToMove
}