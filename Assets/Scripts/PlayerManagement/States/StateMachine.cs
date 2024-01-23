using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine
{
    public PlayerBaseState CurrentState;
    
    public GroundedState groundedState;
    public WalkState walkState;

    public StateMachine(PlayerMovement player)
    {
        this.groundedState = new GroundedState(player, this);
        this.walkState = new WalkState(player, this);
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