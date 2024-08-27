using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using StarForce;
using UnityEngine;

public class KeepIdle : FsmState<NpcFSM>
{
    protected override void OnInit(IFsm<NpcFSM> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<NpcFSM> fsm)
    {
        base.OnEnter(fsm);
    }

    protected override void OnUpdate(IFsm<NpcFSM> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        // ChangeState<TrackTarget>(fsm);
        // GameEntry.Fsm.DestroyFsm<NpcFSM>(fsm);

    }

    protected override void OnLeave(IFsm<NpcFSM> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }
}
