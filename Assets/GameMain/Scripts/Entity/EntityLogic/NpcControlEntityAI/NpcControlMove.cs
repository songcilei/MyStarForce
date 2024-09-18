using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameFramework.Fsm;
using UnityEngine;
using UnityEngine.AI;

public class NpcControlMove : FsmState<NpcControlEntity>
{
    public float MoveSpeed = 2;
    public float MaxTrackDistance = 10;
    private Transform trans;
    private Transform heroTrans;
    private NavMeshAgent agent;
    private Animator m_Animator;
    private float agentSpeed;
    protected override void OnInit(IFsm<NpcControlEntity> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<NpcControlEntity> fsm)
    {
        base.OnEnter(fsm);
        trans = fsm.Owner.transform;
        heroTrans = PlayerInputSystem.Instance.transform;
        agent = fsm.Owner.GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed;
        agent.Warp(trans.position);
        m_Animator = fsm.Owner.gameObject.GetComponent<Animator>();
    }

    protected override void OnUpdate(IFsm<NpcControlEntity> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        float distance = Vector3.Distance(trans.position, heroTrans.position);
        if (distance>1)
        {
            m_Animator.Play("run");
            agent.speed = agentSpeed * 2;
            agent.SetDestination(heroTrans.position);
        }
        else if (distance>MaxTrackDistance)
        {
            agent.speed = agentSpeed;
            ChangeState<NpcControlIdle>(fsm);
        }
        else

        {
            agent.speed = agentSpeed;
            ChangeState<NpcControlIdle>(fsm);
        }
    }



    async Task Wait1Sec()
    {
        await Task.Delay(1000);
    }

    protected override void OnLeave(IFsm<NpcControlEntity> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }
}
