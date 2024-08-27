using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;

public class LeaveTarget : FsmState<NpcFSM>
{
    private float checkDistance=1;
    private Vector3 targetPosition;
    public LeaveTarget(float minDistance)
    {
        this.checkDistance = minDistance;
    }
    
    protected override void OnInit(IFsm<NpcFSM> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<NpcFSM> fsm)
    {
        base.OnEnter(fsm);
        targetPosition = fsm.GetData<VarVector3>("targetPosition");

    }

    protected override void OnUpdate(IFsm<NpcFSM> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        float distance = Vector3.Distance(fsm.Owner.transform.position, targetPosition);
        
        if (distance<=checkDistance)
        {
            ChangeState<KeepIdle>(fsm);
            GameEntry.Entity.HideEntity(fsm.Owner.Entity);

            GameEntry.Fsm.DestroyFsm<NpcFSM>(fsm);
            //这里可以考虑换成pool
            GameObject.DestroyImmediate(fsm.Owner.Model);
        }
    }

    protected override void OnLeave(IFsm<NpcFSM> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        

        
    }
}
