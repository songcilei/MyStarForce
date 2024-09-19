using System.Collections;
using System.Collections.Generic;
using ECM2;
using GameFramework.Fsm;
using UnityEngine;
using UnityEngine.AI;

public class NpcControlPatrol : FsmState<NpcControlEntity>
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

    public NpcControlPatrol(float patrolRadius,float searchRadius,float searchAngle)
    {
        this.PatrolRadius = patrolRadius;
        this.searchRadius = searchRadius;
        this.searchAngle = searchAngle;
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
        agent.Warp(trans.position);
        heroTrans =  PlayerInputSystem.Instance.transform;
        m_Animator = fsm.Owner.gameObject.GetComponent<Animator>();
    }

    protected override void OnUpdate(IFsm<NpcControlEntity> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (!Patroling)
        {
            Vector3 randomDirection = Random.insideUnitSphere * PatrolRadius;
            randomDirection.y = 0;
            randomDirection += trans.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection,out hit,PatrolRadius,NavMesh.AllAreas))
            {
                Patroling = true;
                TargetPosition = hit.position;
            }
        }

        if (Patroling)
        {
            float distance = Vector3.Distance(TargetPosition, trans.position);
            if (distance > 1)
            {
                agent.SetDestination(TargetPosition);
                m_Animator.Play("walk");
            }
            else
            {
                Patroling = false;
            }
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
        Vector3 norVec = trans.forward;
        Vector3 temVec =  heroPos - trans.position;

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
