using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerBaseState
{
    private PlayerMovement p;
    private StateMachine sm;
    public JumpState(PlayerMovement player, StateMachine sm)
    {
        this.p = player;
        this.sm = sm;
    }

    public override void FixedUpdate()
    {
        p.rb.drag = 0f;
        
        p.rb.velocity = new Vector3(p.rb.velocity.x, 0f, p.rb.velocity.z);

        p.rb.AddForce(p.transform.up * p.jumpSpeed, ForceMode.Impulse);

        p.StartCoroutine(p.ResetTrue(endValue => p.canJump = endValue, p.jumpTimeOut));

        CheckSwitch();
    }

    public override void CheckSwitch()
    {
        sm.TransitionTo(sm.airState);
    }
}