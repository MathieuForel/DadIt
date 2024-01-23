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
        p.rb.AddForce(p.moveDirection * p.playerSpeeds[1], ForceMode.Force);
    }

    public override void CheckSwitch()
    {
        if (!p.isMoving)
        {
            sm.TransitionTo(sm.groundedState);
        }
        else if (p.isMoving && Input.GetKey(p.sprintKey))
        {
            sm.TransitionTo(sm.runState);
        }
        else if (p.isMoving && Input.GetKey(p.slideKey) && p.canSlide)
        {
            sm.TransitionTo(sm.slideState);
        }
        else if (p.isMoving && Input.GetKeyDown(p.dashKey) && p.canDash)
        {
            sm.TransitionTo(sm.dashState);
        }
        else if (p.isMoving && Input.GetKeyDown(p.crouchKey))
        {
            sm.TransitionTo(sm.crouchState);
        }
        else if (Input.GetKeyDown(p.jumpKey))
        {
            sm.TransitionTo(sm.jumpState);
        }
    }
}