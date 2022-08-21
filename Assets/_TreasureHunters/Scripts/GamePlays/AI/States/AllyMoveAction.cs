using Ultimate.Core.Runtime.FSM;

public class AllyMoveAction : FSMAction
{
    private GameCharacter aiController;
    private AIBehaviour aiBehaviour;

    public AllyMoveAction(FSMState _owner, GameCharacter _aiController, AIBehaviour _aiBehaviour) : base(_owner)
    {
        aiController = _aiController;
        aiBehaviour = _aiBehaviour;
    }

    public override void OnEnter()
    {
    }

    public override void OnUpdate(float dt)
    {
    }

    public override void OnDestroy()
    {
        aiController = null;
        aiBehaviour = null;
    }
}