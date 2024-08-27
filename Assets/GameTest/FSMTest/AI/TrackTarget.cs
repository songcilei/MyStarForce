using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using StarForce;
using UnityEngine;
using UnityEngine.AI;

public class TrackTarget : FsmState<NpcFSM>
{
    private float checkDistance=1;
    private Vector3 targetPosition;
    public TrackTarget(float minDistance)
    {
        this.checkDistance = minDistance;
    }

    protected override void OnInit(IFsm<NpcFSM> fsm)
    {
        base.OnInit(fsm);
        if (fsm.Owner._agent == null)
        {
            Debug.LogError("logerro == null");
        }
        fsm.Owner._agent.Warp(fsm.Owner.transform.position);
    }

    protected override void OnEnter(IFsm<NpcFSM> fsm)
    {
        base.OnEnter(fsm);

        fsm.Owner._agent.Warp(fsm.Owner.transform.position);
        
        var shopType = fsm.Owner.NeedProduct.shopType;
        targetPosition = GameEntry.Shop.GetShopAgentPosition(shopType);
        
    }

    protected override void OnUpdate(IFsm<NpcFSM> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

        fsm.Owner._agent.SetDestination(targetPosition);
        float distance = Vector3.Distance(fsm.Owner.transform.position, targetPosition);
        
        if (distance<=checkDistance)
        {
            Debug.Log("switch state");
            ChangeState<GetTarget>(fsm);
        }
    }

    protected override void OnLeave(IFsm<NpcFSM> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }

}
