using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using GameFramework.Procedure;
using StarForce;
using UnityEngine;
using ProcedureBase = StarForce.ProcedureBase;

public class TestProcedureObjectPool : ProcedureBase
{
    public override bool UseNativeDialog { get; }


    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);

        GameObject TT = new GameObject("Player");
        TT.AddComponent<ObjectPlayer>();


    }
}
