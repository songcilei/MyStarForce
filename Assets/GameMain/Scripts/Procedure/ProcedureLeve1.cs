using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using GameFramework.Procedure;
using StarForce;
using UnityEngine;
using ProcedureBase = StarForce.ProcedureBase;

public class ProcedureLeve1 : ProcedureBase
{
    public override bool UseNativeDialog {
        get
        {
            return false;
        }
    }


    protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnInit(procedureOwner);
    }

    private BattleDebugger battleDebugger;
    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);
        GameEntry.Entity.ShowPlayerControl(new PlayerControlEntityData(GameEntry.Entity.GenerateSerialId(),90001));
        battleDebugger = new BattleDebugger();
        GameEntry.Debugger.RegisterDebuggerWindow("Battle",battleDebugger);
    }

    protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
    }

    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);
        GameEntry.Debugger.UnregisterDebuggerWindow("Battle");
    }
}
