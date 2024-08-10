using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using GameFramework.Procedure;
using StarForce;
using UnityEngine;
using ProcedureBase = StarForce.ProcedureBase;

public class CreateTestFsmProcedure : ProcedureBase
{
    public override bool UseNativeDialog { get; }

    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);
        
        GameEntry.Entity.ShowEntity<FSM_Hero>(99999,"Assets/GameTest/FSMTest/hero.prefab","Aircraft");
    }
}
