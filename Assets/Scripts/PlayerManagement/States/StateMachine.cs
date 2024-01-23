using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine
{
    public PlayerBaseState CurrentState;
    
    public JumpState jumpState;
    public DashState dashState;
    public AirState airState;
    public GroundedState groundedState;
    public CrouchState crouchState;
    public WalkState walkState;
    public RunState runState;
    public WallRunState wallRunState;
    public SlideState slideState;

    public StateMachine(PlayerMovement player)
    {
        this.jumpState = new JumpState(player, this);
        this.dashState = new DashState(player, this);
        this.airState = new AirState(player, this);
        this.groundedState = new GroundedState(player, this);
        this.crouchState = new CrouchState(player, this);
        this.walkState = new WalkState(player, this);
        this.runState = new RunState(player, this);
        this.wallRunState = new WallRunState(player, this);
        this.slideState = new SlideState(player, this);
    }

    public void Initialize(PlayerBaseState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }
    public void TransitionTo(PlayerBaseState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }
    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }

    public void FixedUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.FixedUpdate();
        }
    }
}