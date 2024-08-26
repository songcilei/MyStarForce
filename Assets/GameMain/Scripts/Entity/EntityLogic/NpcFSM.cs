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
    public NpcFSMData NpcFsmData = null;

    public NavMeshAgent _agent;

    public ProductBase NeedProduct;

    private IFsm<NpcFSM> npcF;
//table
    private float FsmDistance = 1.5f;
    
    public int entityId;
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);

        _agent = GetComponent<NavMeshAgent>();
        
        NpcFsmData = userData as NpcFSMData;
        if (userData == null)
        {
            Debug.LogError("my npc data is invalid");
            return;
        }
        //Init value=====================

        this.transform.position = NpcFsmData.InitPosition;
        this.NeedProduct = NpcFsmData.NeedProduct;
        this.entityId = NpcFsmData.EntityId;
        this.FsmDistance = NpcFsmData.FSMDistance;
        //FsmState<NpcFSM>[] npcState = new FsmState<NpcFSM>[] { };
        
        FsmState<NpcFSM>[] npcState = {
            new TrackTarget(FsmDistance),
            new GetTarget(),
            new LeaveTarget(FsmDistance)
        };


        Debug.Log("NPC init");
        // if (GameEntry.Fsm.HasFsm<NpcFSM>())
        // {
        //     npcF = GameEntry.Fsm.GetFsm<NpcFSM>();
        //     npcF.GetState<TrackTarget>();
        // }
        // else
        // {
        //     npcF = GameEntry.Fsm.CreateFsm(this, npcState);
        //     npcF.Start<TrackTarget>();
        // }
        npcF = GameEntry.Fsm.CreateFsm(this.name+entityId,this, npcState);
     
        npcF.Start<TrackTarget>();
        Debug.Log("create state");

    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        
        //run fsm
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position,FsmDistance);
    }
}
