using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : PlayerBaseState
{
    private PlayerMovement p;
    private StateMachine sm;
    public SlideState(PlayerMovement player, StateMachine sm)
    {
        this.p = player;
        this.sm = sm;
    }
    public override void Enter()
    {
        p.transform.localScale = new Vector3(p.scale.x, p.playerCrouchHeight, p.scale.z);
        p.rb.AddForce(Vector3.down * 2f, ForceMode.Impulse);
    }
    public override void Update()
    {
        p.slideTime -= Time.deltaTime;
        if (p.slideTime <= 0)
        {
            sm.TransitionTo(sm.walkState);
        }

        CheckSwitch();
    }
    
    public override void FixedUpdate()
    {
        p.rb.AddForce(p.moveDirection * p.playerSpeeds[4], ForceMode.Force);
    }
    public override void Exit()
    {
        p.transform.localScale = p.scale;
        p.StartCoroutine(p.ResetTrue(endValue => p.canSlide = endValue, p.slideTimeOut));
        p.slideTime = p.maxSlideTime;
    }
    
    public override void CheckSwitch()
    {
        if (Input.GetKeyDown(p.jumpKey))
        {
            sm.TransitionTo(sm.jumpState);
        }
        else if (p.isMoving && !Input.GetKey(p.slideKey))
        {
            sm.TransitionTo(sm.walkState);
        }
        else if (!p.isMoving)
        {
            sm.TransitionTo(sm.groundedState);
        }
    }
}
