using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : PlayerBaseState
{
    private PlayerMovement p;
    private StateMachine sm;
    public WalkState(PlayerMovement player, StateMachine sm)
    {
        this.p = player;
        this.sm = sm;
    }
    public override void Update()
    {
        CheckSwitch();
    }
    public override void FixedUpdate()
    {
        p.rb.AddForce(p.moveDirection * p.playerSpeeds[0], ForceMode.Force);
    }

    public override void CheckSwitch()
    {
        if (!p.isMoving)
        {
            sm.TransitionTo(sm.groundedState);
        }
    }
}