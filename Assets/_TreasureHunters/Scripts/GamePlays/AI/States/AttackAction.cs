using Ultimate.Core.Runtime.FSM;

public class AttackAction : FSMAction
{
    private GameCharacter aiController;
    private AIBehaviour aiBehaviour;

    public AttackAction(FSMState _owner, GameCharacter _aiController, AIBehaviour _aiBehaviour) : base(_owner)
    {
        aiController = _aiController;
        aiBehaviour = _aiBehaviour;
    }
}