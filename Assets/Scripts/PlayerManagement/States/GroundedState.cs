using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : PlayerBaseState
{
    private PlayerMovement p;
    private StateMachine sm;
    public GroundedState(PlayerMovement player, StateMachine sm)
    {
        this.p = player;
        this.sm = sm;
    }

    public override void Update()
    {
        p.rb.velocity /= 1.3f;
        p.rb.drag = p.groundDrag;
        CheckSwitch();
    }

    public override void CheckSwitch()
    {
        if (p.isMoving)
        {
            sm.TransitionTo(sm.walkState);
        }
    }
}
