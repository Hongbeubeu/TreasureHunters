using Ultimate.Core.Runtime.FSM;

public class AttackAction : FSMAction
{
    private Character aiController;
    private AIBehaviour aiBehaviour;

    public AttackAction(FSMState _owner, Character _aiController, AIBehaviour _aiBehaviour) : base(_owner)
    {
        aiController = _aiController;
        aiBehaviour = _aiBehaviour;
    }
}