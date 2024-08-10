using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;

public class DuckingState : FsmState<FSM_Hero>
{
    protected override void OnInit(IFsm<FSM_Hero> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<FSM_Hero> fsm)
    {
        base.OnEnter(fsm);
        Debug.Log("下蹲躲避！");
    }

    protected override void OnUpdate(IFsm<FSM_Hero> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        HandleInput(fsm);
    }

    protected override void OnLeave(IFsm<FSM_Hero> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }

    public void HandleInput(IFsm<FSM_Hero> fsm)
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("下蹲状态ing");
            return;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            Debug.Log("UPUp");
            ChangeState<StandingState>(fsm);
        }
    }
}
