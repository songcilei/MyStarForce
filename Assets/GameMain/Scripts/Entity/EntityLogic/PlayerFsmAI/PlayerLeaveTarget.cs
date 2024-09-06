using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;

public class PlayerLeaveTarget : FsmState<PlayerFSM>
{
    private Vector3 targetPos;
    private Quaternion targetRotation;
    
    protected override void OnInit(IFsm<PlayerFSM> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<PlayerFSM> fsm)
    {
        base.OnEnter(fsm);
        targetPos = fsm.Owner.InitActionPos;
        targetRotation = fsm.Owner.InitActionRotation;
    }

    protected override void OnUpdate(IFsm<PlayerFSM> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

        Vector3 Dir = fsm.Owner.transform.position;
        if (Vector3.Distance(fsm.Owner.transform.position,targetPos)>0.1f)
        {
            fsm.Owner.transform.position += Dir * Time.deltaTime;
        }
        else
        {
            fsm.Owner.transform.position = targetPos;
            fsm.Owner.transform.rotation = targetRotation;
            ChangeState<PlayerIdle>(fsm);
        }


    }

    protected override void OnLeave(IFsm<PlayerFSM> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }
}
