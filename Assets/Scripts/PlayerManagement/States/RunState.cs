using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : PlayerBaseState
{
    private PlayerMovement p;
    private StateMachine sm;
    public RunState(PlayerMovement player,StateMachine sm)
    {
        this.p = player;
        this.sm = sm;
    }
    public override void FixedUpdate()
    {
        p.rb.AddForce(p.moveDirection * p.playerSpeeds[2], ForceMode.Force);
    }
    public override void Update()
    {
        CheckSwitch();
    }

    public override void CheckSwitch()
    {
        if (p.isMoving && !Input.GetKey(p.sprintKey))
        {
            sm.TransitionTo(sm.walkState);
        }
        else if (!p.isMoving)
        {
            sm.TransitionTo(sm.groundedState);
        }
        else if (Input.GetKeyDown(p.jumpKey))
        {
            sm.TransitionTo(sm.jumpState);
        }
    }
}
