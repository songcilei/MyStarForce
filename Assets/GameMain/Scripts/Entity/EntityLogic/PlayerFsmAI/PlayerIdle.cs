using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

public class PlayerIdle : FsmState<PlayerFSM>
{
    protected override void OnInit(IFsm<PlayerFSM> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<PlayerFSM> fsm)
    {
        base.OnEnter(fsm);
        if (fsm.Owner.m_Animator!=null)
        {
            fsm.Owner.m_Animator.Play("idle");
        }

    }

    protected override void OnUpdate(IFsm<PlayerFSM> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
    }

    protected override void OnLeave(IFsm<PlayerFSM> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }
}
