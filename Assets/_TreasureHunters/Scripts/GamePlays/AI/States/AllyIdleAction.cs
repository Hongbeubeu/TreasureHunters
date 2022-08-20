﻿using Ultimate.Core.Runtime.FSM;

public class AllyIdleAction : FSMAction
{
    private Character aiController;
    private AIBehaviour aiBehaviour;

    public AllyIdleAction(FSMState _owner, Character _aiController, AIBehaviour _aiBehaviour) : base(_owner)
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