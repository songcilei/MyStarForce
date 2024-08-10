using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;

public class StandingState : FsmState<FSM_Hero>
{
    protected override void OnInit(IFsm<FSM_Hero> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<FSM_Hero> fsm)
    {
        base.OnEnter(fsm);
        Debug.Log("current State: stadingState");
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
            Debug.Log("up Array");
            ChangeState<JumpingState>(fsm);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("up array");
            ChangeState<DuckingState>(fsm);
        }
    }
}
