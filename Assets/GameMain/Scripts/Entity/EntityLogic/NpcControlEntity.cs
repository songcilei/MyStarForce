using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;
/// <summary>
/// 世界随机敌人
/// </summary>
public class NpcControlEntity : EntityLogic
{
    public int EntityId;
    private NpcControlEntityData data;
    private FsmState<NpcControlEntity>[] npcState;
    private IFsm<NpcControlEntity> npcFsm;
    public bool Debug = false;
    private int GizmosSteps = 30;
//fsm
    public float PatrolRadius = 10;//随机巡逻的区域半径
    public float searchRadius = 5;//扇形检测的半径
    public float searchAngle = 60;//扇形检测的角度
    public string patrolPath = null;
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        data = userData as NpcControlEntityData;
        patrolPath = data.patrolPathName;
        npcState = new FsmState<NpcControlEntity>[]
        {
            new NpcControlIdle(patrolPath),
            new NpcControlFind(),
            new NpcControlMove(),
            new NpcControlDeath(),
            new NpcControlPathPatrol(PatrolRadius,searchRadius,searchAngle,patrolPath),
            new NpcControlPatrol(PatrolRadius,searchRadius,searchAngle)
        };
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
//data        
        data = userData as NpcControlEntityData;
        EntityId = data.EntityId;
        
        // GameEntry.Resource.LoadAsset();
//create fsm
        npcFsm = GameEntry.Fsm.CreateFsm(this.name + EntityId, this, npcState);
        npcFsm.Start<NpcControlIdle>();
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);


    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        GameEntry.Fsm.DestroyFsm(npcFsm);
        patrolPath = null;
    }


    private bool Check = false;
    private void OnDrawGizmos()
    {
        if (Debug)
        {
            Transform trans = PlayerInputSystem.Instance.transform;
            Vector3 center = transform.position;
            Vector3 direction = transform.forward;
            Gizmos.color = Color.red;

            // 绘制扇形的边界线
            for (int i = 0; i <= GizmosSteps; i++)
            {
                float stepAngle = searchAngle/ GizmosSteps * i-(searchAngle/2);
                Vector3 point = center + Quaternion.AngleAxis(stepAngle, transform.up) * (direction * searchRadius);
                if (Check)
                {
                    Gizmos.color = Color.yellow;
                }

                Gizmos.DrawLine(center, point);
            }
            
            Gizmos.DrawLine(trans.position,center);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(center,center+direction*5);
        }
    }
}
