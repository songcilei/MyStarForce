using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;
using UnityEngine.AI;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;

public class NpcFSM : EntityLogic
{
    private NpcFSMData NpcFsmData = null;

    public NavMeshAgent _agent;

    public ProductBase NeedProduct;

    private IFsm<NpcFSM> npcF;
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);

        FsmState<NpcFSM>[] npcState =
        {
            new TrackTarget(1),
            new GetTarget(),
            new LeaveTarget()
        };

        npcF = GameEntry.Fsm.CreateFsm(this, npcState);
        _agent = this.GetComponent<NavMeshAgent>();
        Debug.Log("create state");

    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        NpcFsmData = userData as NpcFSMData;
        if (userData == null)
        {
            Debug.LogError("my npc data is invalid");
            return;
        }
        //Init value=====================

        this.transform.position = NpcFsmData.InitPosition;
        this.NeedProduct = NpcFsmData.NeedProduct;    
        
        //run fsm
        npcF.Start<TrackTarget>();
        Debug.Log("run FSM");
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
    }
}
