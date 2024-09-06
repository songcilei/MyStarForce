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
    }

    protected override void OnUpdate(IFsm<PlayerFSM> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        bool action = fsm.GetData<VarBoolean>("Action");
        if (action)
        {
            fsm.RemoveData("Action");
            ChangeState<PlayerMenu>(fsm);
        }
    }

    protected override void OnLeave(IFsm<PlayerFSM> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }
}
