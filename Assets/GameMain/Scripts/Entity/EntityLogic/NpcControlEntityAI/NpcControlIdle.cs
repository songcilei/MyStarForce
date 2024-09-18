using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;

public class NpcControlIdle : FsmState<NpcControlEntity>
{
    
    private Animator m_Animator;
    private string m_PatrolPath;

    public NpcControlIdle(string patrolPath)
    {
        m_PatrolPath = patrolPath;
    }

    protected override void OnInit(IFsm<NpcControlEntity> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<NpcControlEntity> fsm)
    {
        base.OnEnter(fsm);
        m_Animator = fsm.Owner.gameObject.GetComponent<Animator>();
    }

    protected override void OnUpdate(IFsm<NpcControlEntity> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        m_Animator.Play("idle");
        if (PatrolPoints.Instance.GetPatrolPath(m_PatrolPath)==null)
        {
            ChangeState<NpcControlPatrol>(fsm);
        }
        else
        {
            ChangeState<NpcControlPathPatrol>(fsm);
        }


    }

    protected override void OnLeave(IFsm<NpcControlEntity> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }
}
