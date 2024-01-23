using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : PlayerBaseState
{
    private PlayerMovement p;
    private StateMachine sm;
    public AirState(PlayerMovement player, StateMachine sm)
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
        p.rb.AddForce(p.moveDirection * p.playerSpeeds[5], ForceMode.Force);
    }

    public override void CheckSwitch()
    {
        if (p.isGrounded)
        {
            sm.TransitionTo(sm.groundedState);
        }
        
        else if (p.isMoving && Input.GetKeyDown(p.dashKey) && p.canDash)
        {
            sm.TransitionTo(sm.dashState);
        }
        
        else if (p.AboveGround() && p.CheckWall() && p.canWallRun)
        {
            sm.TransitionTo(sm.wallRunState);
        }
    }
}