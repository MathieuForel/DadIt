using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerBaseState
{
    private PlayerMovement p;
    private StateMachine sm;
    public DashState(PlayerMovement player, StateMachine sm)
    {
        this.p = player;
        this.sm = sm;
    }

    public override void Enter()
    {
        p.rb.AddForce((p.orientation.forward * p.verticalInput + p.orientation.right * p.horizontalInput).normalized * p.dashSpeed, ForceMode.Impulse);
        p.StartCoroutine(p.ResetTrue(endValue => p.canDash = endValue, p.dashTimeOut));
        CheckSwitch();
    }

    public override void CheckSwitch()
    {
        sm.TransitionTo(sm.airState);
    }
}