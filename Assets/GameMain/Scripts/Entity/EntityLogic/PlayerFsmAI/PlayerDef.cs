using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using StarForce;
using UnityEngine;

public class PlayerDef : FsmState<PlayerFSM>
{
    protected override void OnInit(IFsm<PlayerFSM> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<PlayerFSM> fsm)
    {
        base.OnEnter(fsm);
        fsm.Owner.IsDef = true;
        GameEntry.BattleSystem.nextPlayerFsm();
    }

    protected override void OnUpdate(IFsm<PlayerFSM> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        
    }

    protected override void OnLeave(IFsm<PlayerFSM> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        //在重置action的时候 会关闭防御模式
        fsm.Owner.IsDef = false;
    }


}
