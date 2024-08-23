using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;
using UnityEngine.AI;

public class TrackTarget : FsmState<NpcFSM>
{
    // private NavMeshAgent _agent;
    protected override void OnInit(IFsm<NpcFSM> fsm)
    {
        base.OnInit(fsm);
        fsm.Owner._agent.Warp(fsm.Owner.transform.position);

    }

    protected override void OnEnter(IFsm<NpcFSM> fsm)
    {
        base.OnEnter(fsm);
    }

    protected override void OnUpdate(IFsm<NpcFSM> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
    }

    protected override void OnLeave(IFsm<NpcFSM> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }
}
