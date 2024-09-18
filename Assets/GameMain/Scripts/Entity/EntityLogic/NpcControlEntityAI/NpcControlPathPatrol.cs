using System.Collections;
using System.Collections.Generic;
using ECM2;
using GameFramework.Fsm;
using UnityEngine;
using UnityEngine.AI;

public class NpcControlPathPatrol : FsmState<NpcControlEntity>
{
    public float PatrolRadius = 10;//随机巡逻的区域半径
    public float searchRadius = 5;//扇形检测的半径
    public float searchAngle = 60;//扇形检测的角度
    private bool Patroling = false;
    private NavMeshAgent agent;
    private Vector3 TargetPosition;
    private Transform trans;
    private Transform heroTrans;
    private Animator m_Animator;
    
    private string m_PatrolPathName;
    private PatrolPath m_PatrolPath;
    private int PathPointIndex = 0; 
    public NpcControlPathPatrol(float patrolRadius,float searchRadius,float searchAngle,string patrolPath)
    {
        this.PatrolRadius = patrolRadius;
        this.searchRadius = searchRadius;
        this.searchAngle = searchAngle;
        this.m_PatrolPathName = patrolPath;
    }

    protected override void OnInit(IFsm<NpcControlEntity> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<NpcControlEntity> fsm)
    {
        base.OnEnter(fsm);
        agent = fsm.Owner.GetComponent<NavMeshAgent>();
        trans = fsm.Owner.transform;
        heroTrans =  PlayerInputSystem.Instance.transform;
        m_Animator = fsm.Owner.gameObject.GetComponent<Animator>();
        if (m_PatrolPathName != null)
        {   
            m_PatrolPath = PatrolPoints.Instance.GetPatrolPath(m_PatrolPathName);
            agent.Warp(trans.position);
            Patroling = true;
        }


    }

    protected override void OnUpdate(IFsm<NpcControlEntity> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);


        if (Patroling)
        {
            Vector3 targetPos = m_PatrolPath.points[PathPointIndex];
            m_Animator.Play("walk");
            agent.SetDestination(targetPos);
            if (Vector3.Distance(targetPos,trans.position)<0.5f)
            {
                if (m_PatrolPath.points.Count == 1)
                {
                    Patroling = false;
                }

                PathPointIndex++;
                PathPointIndex = PathPointIndex%(m_PatrolPath.points.Count);
                
            }

        }
        if (!Patroling)
        {
            m_Animator.Play("idle");
            agent.Warp(trans.position);
        }
        if (SectorCheck())
        {
            ChangeState<NpcControlFind>(fsm);
        }

    }


    private bool SectorCheck()
    {
        Vector3 heroPos = heroTrans.position;
        float distance = Vector3.Distance(trans.position, heroPos);
        Vector3 norVec = trans.rotation * trans.forward;
        Vector3 temVec = heroPos - trans.position;

        float angle = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;
        if (distance<searchRadius)
        {
            if (angle <= searchAngle * 0.5f)
            {
                return true;
            }
        }
        return false;
    }


    protected override void OnLeave(IFsm<NpcControlEntity> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }
}
