using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using StarForce;
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

    private FsmState<NpcFSM>[] npcState;
//table
    private float FsmDistance = 1.5f;
    
    public int entityId;

    public int ModelID;
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
        
        npcState =new FsmState<NpcFSM>[] {
            new KeepIdle(),
            new TrackTarget(FsmDistance),
            new GetTarget(),
            new LeaveTarget(FsmDistance)
        };

        Debug.Log("NPC init");
        Debug.Log("create fsm");

    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
//get id
        int randomId = TableUtility.GetRandomArrayID(NpcFsmData.ModelIDs);
        ModelID = NpcFsmData.ModelIDs[randomId];
        var table =GameEntry.DataTable.GetDataTable<DRNpcModel>();
        DRNpcModel drNpcModel = table.GetDataRow(ModelID);
//load model asset
        string path = AssetUtility.GetNPCModelAsset(drNpcModel.AssetName);
        // GameEntry.Resource.load
        
        npcF = GameEntry.Fsm.CreateFsm(this.name + entityId, this, npcState);
        npcF.Start<TrackTarget>();
        
        //run fsm
        Debug.Log("run FSM:"+name);
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
    }

    public void loadModelSuccess()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position,FsmDistance);
    }
}
