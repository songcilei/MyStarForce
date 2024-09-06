using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;

public class PlayerAtk : FsmState<PlayerFSM>
{
    private int targetId;
    protected override void OnInit(IFsm<PlayerFSM> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<PlayerFSM> fsm)
    {
        base.OnEnter(fsm);
        targetId =fsm.GetData<VarInt32>("targetId");
        PlayerFSM entity = (PlayerFSM)GameEntry.Entity.GetEntity(targetId).Logic;
        entity.Damage(fsm.Owner.Atk);
        
    }

    protected override void OnUpdate(IFsm<PlayerFSM> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        
    }

    protected override void OnLeave(IFsm<PlayerFSM> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        fsm.RemoveData("targetId");
    }
}
