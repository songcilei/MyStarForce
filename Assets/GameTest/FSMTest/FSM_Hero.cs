using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

public class FSM_Hero : EntityLogic
{
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);

        //状态机类型
        FsmState<FSM_Hero>[] heroState =
        {
            new DuckingState(),
            new JumpingState(),
            new StandingState(),
            new DrivingState()
        };
        //状态机创建
        var heroFsm = StarForce.GameEntry.Fsm.CreateFsm(this, heroState);
        //设置初始化状态
        heroFsm.Start<StandingState>();
        Debug.Log("创建FSM对象！");

    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        StarForce.GameEntry.Fsm.DestroyFsm<FSM_Hero>();

    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
    }
}
