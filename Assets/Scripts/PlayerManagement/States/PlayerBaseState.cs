using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    public virtual void Enter(){}
    public virtual void Update(){}
    public virtual void FixedUpdate(){}
    public virtual void Exit(){}
    public virtual void CheckSwitch(){}
}