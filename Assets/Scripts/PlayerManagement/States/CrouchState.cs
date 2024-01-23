using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : PlayerBaseState
{
    private PlayerMovement p;
    private StateMachine sm;
    public CrouchState(PlayerMovement player, StateMachine sm)
    {
        this.p = player;
        this.sm = sm;
    }
    public override void Enter()
    {
        p.transform.localScale = new Vector3(p.scale.x, p.playerCrouchHeight, p.scale.z);
        p.rb.AddForce(Vector3.down * 2f, ForceMode.Impulse);
    }
    public override void Exit()
    {
        p.transform.localScale = p.scale;
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
        if (!p.isMoving && !Input.GetKey(p.crouchKey))
        {
            sm.TransitionTo(sm.groundedState);
        }
        else if (p.isMoving && !Input.GetKey(p.crouchKey))
        {
            sm.TransitionTo(sm.walkState);
        }
    }
}