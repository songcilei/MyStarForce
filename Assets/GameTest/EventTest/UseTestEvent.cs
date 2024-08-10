using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using StarForce;
using UnityEngine;
using ProcedureBase = StarForce.ProcedureBase;

public class UseTestEvent : ProcedureBase
{
    public override bool UseNativeDialog { get; }


    protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnInit(procedureOwner);
    }

    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);
        
        GameEntry.Event.Subscribe(TestGameEventArgs.EventId,OnTestEvent);
        
        
        
    }

    protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
    }

    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);
        GameEntry.Event.Unsubscribe(TestGameEventArgs.EventId,OnTestEvent);
        
    }




    private void OnTestEvent(object sender,BaseEventArgs e)
    {
        TestGameEventArgs ne = (TestGameEventArgs)e; 
        if (ne == null)
        {
            return;
        }

        if (ne.EntityId == -1)
        {
            Debug.Log("false!!!!");
        }
        
        //Run=================================
        

    }


}
