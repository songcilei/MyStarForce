using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFramework.Fsm;
using UnityEngine;
using UnityEngine.AI;

public class NpcControlIdle : FsmState<NpcControlEntity>
{
    
    private Animator m_Animator;
    private string m_PatrolPathName;
    public float searchRadius = 5;//扇形检测的半径
    public float searchAngle = 60;//扇形检测的角度
    
    private Transform trans;
    private Transform heroTrans;
    private NavMeshAgent agent;
    private bool Waiting = true;
    public NpcControlIdle(float searchRadius,float searchAngle,string patrolPathName)
    {
        this.searchRadius = searchRadius;
        this.searchAngle = searchAngle;
        m_PatrolPathName = patrolPathName;
    }

    protected override void OnInit(IFsm<NpcControlEntity> fsm)
    {
        base.OnInit(fsm);
        heroTrans =  PlayerInputSystem.Instance.transform;
        m_Animator = fsm.Owner.gameObject.GetComponent<Animator>();
        agent = fsm.Owner.gameObject.GetComponent<NavMeshAgent>();
        trans = fsm.Owner.transform;
    }

    protected override void OnEnter(IFsm<NpcControlEntity> fsm)
    {
        base.OnEnter(fsm);
        Waiting = true;
        if (m_PatrolPathName != null)
        {
            agent.Warp(trans.position);
        }
        //heroTrans =  PlayerInputSystem.Instance.transform;
        WaitSec();
    }

    protected override void OnUpdate(IFsm<NpcControlEntity> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        m_Animator.Play("idle");

        if (Waiting == false)
        {
            if (PatrolPoints.Instance.GetPatrolPath(m_PatrolPathName)==null)
            {
                ChangeState<NpcControlPatrol>(fsm);
            }
            else
            {
                ChangeState<NpcControlPathPatrol>(fsm);
            }
        }
        else
        {
            if (SectorCheck())
            {
                ChangeState<NpcControlFind>(fsm);
            }
        }
    }

    async UniTaskVoid WaitSec()
    {
        await UniTask.WaitForSeconds(2);
        Waiting = false;
    }
    
    
    private bool SectorCheck()
    {
        Vector3 heroPos = heroTrans.position;
        float distance = Vector3.Distance(trans.position, heroPos);
        // Quaternion qUaRation = Quaternion.Euler(0,searchAngle/2,0);
        Vector3 norVec = trans.forward;
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
