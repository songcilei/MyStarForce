using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;
using ProcedureBase = StarForce.ProcedureBase;

public class TestProcedureReferencePool : ProcedureBase
{
    public override bool UseNativeDialog { get; }

    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);

        GameObject referenceObj = new GameObject("Player");
        referenceObj.AddComponent<ReferencePoolPlayer>();

    }
}
