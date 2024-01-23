using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunState : PlayerBaseState
{  
    private PlayerMovement p;
    private StateMachine sm;
    public WallRunState(PlayerMovement player, StateMachine sm)
    {
        this.p = player;
        this.sm = sm;
    }
    public override void Enter()
    {
        p.rb.useGravity = false;
        p.rb.velocity = new Vector3(p.rb.velocity.x, 0f, p.rb.velocity.z);
    }
    public override void Update()
    {
        p.wallRunDuration -= Time.deltaTime;
        if (p.wallRunDuration <= 0)
        {
            sm.TransitionTo(sm.airState);
        }
        
        CheckSwitch();
    }

    public override void CheckSwitch()
    {
        if (p.isGrounded)
        {
            sm.TransitionTo(sm.groundedState);
        }
    }
    
    public override void FixedUpdate()
    {
        Vector3 wallNormal = p.wallRight ? p.wallHitRight.normal : p.wallHitLeft.normal;
        p.rb.AddForce(-wallNormal * p.wallRunForce, ForceMode.Force);
        p.rb.AddForce(p.moveDirection * p.playerSpeeds[3], ForceMode.Force);

        if (Input.GetKeyDown(p.jumpKey))
        {
            Jump();
        }
    }
    public override void Exit()
    {
        p.StartCoroutine(p.ResetTrue(endValue => p.canWallRun = endValue, p.jumpTimeOut));
        p.wallRunDuration = p.wallRunMaxDuration;
        p.rb.useGravity = true;
    }
    
    public void Jump()
    {
        p.canWallRun = false;

        //Finds the wall normal
        Vector3 wallNormal = p.wallRight ? p.wallHitRight.normal : p.wallHitLeft.normal;

        //Apply force to push the player horizontally
        Vector3 forceToApply = wallNormal * p.wallJumpSideForce;
        p.rb.AddForce(forceToApply, ForceMode.Impulse);
        
        //Reset player y velocity and jump
        p.rb.velocity = new Vector3(p.rb.velocity.x, 0f, p.rb.velocity.z);
        p.rb.AddForce(p.transform.up * p.wallJumpUpForce, ForceMode.Impulse);

        sm.TransitionTo(sm.airState);
    }
}