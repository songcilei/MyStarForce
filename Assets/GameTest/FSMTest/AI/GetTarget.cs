using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using StarForce;
using UnityEngine;
using UnityGameFramework.Runtime;

public class GetTarget : FsmState<NpcFSM>
{
    protected override void OnInit(IFsm<NpcFSM> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<NpcFSM> fsm)
    {
        base.OnEnter(fsm);
        
        Vector3 leavePosition = fsm.Owner.NpcFsmData.InitPosition;
        fsm.Owner._agent.SetDestination(leavePosition);
        fsm.SetData<VarVector3>("targetPosition",leavePosition);
        ChangeState<LeaveTarget>(fsm);

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
