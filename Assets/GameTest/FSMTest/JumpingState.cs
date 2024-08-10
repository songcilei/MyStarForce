using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;

public class JumpingState : FsmState<FSM_Hero>
{
    protected override void OnEnter(IFsm<FSM_Hero> fsm)
    {
        base.OnEnter(fsm);
        Debug.Log("current State: JumpingState");
    }

    protected override void OnUpdate(IFsm<FSM_Hero> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        HandleInput(fsm);
    }

    public void HandleInput(IFsm<FSM_Hero> fsm)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("已经在跳跃ing");
            return;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Down Arrow");
            ChangeState<DrivingState>(fsm);
        }

    }

}
