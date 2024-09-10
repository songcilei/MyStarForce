using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;
public class PlayerMoveTarget : FsmState<PlayerFSM>
{
    private int targetId;
    private PlayerFSM entity;
    protected override void OnInit(IFsm<PlayerFSM> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<PlayerFSM> fsm)
    {
        base.OnEnter(fsm);
        targetId =fsm.GetData<VarInt32>("targetId");
        if (GameEntry.Entity.HasEntity(targetId))
        {
            entity = (PlayerFSM)GameEntry.Entity.GetEntity(targetId).Logic;
        }

    }

    protected override void OnUpdate(IFsm<PlayerFSM> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        Vector3 targetPos = entity.transform.position;
        Vector3 selfPos = fsm.Owner.transform.position;
        Vector3 Dir = (targetPos - selfPos).normalized;
        float radius = fsm.Owner.Radius;

        if (Vector3.Distance(targetPos,selfPos)>radius)
        {
            fsm.Owner.transform.forward = Dir.normalized;
            fsm.Owner.transform.position += Dir * Time.deltaTime*10;
        }
        else
        {
            ChangeState<PlayerAtk>(fsm);
        }
    }

    protected override void OnLeave(IFsm<PlayerFSM> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }
}
