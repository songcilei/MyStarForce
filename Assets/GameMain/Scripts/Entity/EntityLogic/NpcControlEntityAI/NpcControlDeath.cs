using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;

public class NpcControlDeath : FsmState<NpcControlEntity>
{
    protected override void OnInit(IFsm<NpcControlEntity> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<NpcControlEntity> fsm)
    {
        base.OnEnter(fsm);
    }

    protected override void OnUpdate(IFsm<NpcControlEntity> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
    }

    protected override void OnLeave(IFsm<NpcControlEntity> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }
}
