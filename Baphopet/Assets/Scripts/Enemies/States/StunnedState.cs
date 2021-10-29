using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : State
{
    protected D_StunnedState stateData;

    protected bool isStunTimeOver;

    public StunnedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunnedState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;
        entity.SetVelocity(stateData.stunKnockbackSpeed, stateData.stunKnockbackAngle, entity.lastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.stunTime)
        {
            isStunTimeOver = true;
        }
    }

    public override void PhisicsUpdate()
    {
        base.PhisicsUpdate();
    }
}
