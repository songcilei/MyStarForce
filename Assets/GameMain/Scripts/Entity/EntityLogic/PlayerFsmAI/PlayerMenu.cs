using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;

public class PlayerMenu : FsmState<PlayerFSM>
{
    private MenuType _menuType = MenuType.None;
    private int targetId;
    protected override void OnInit(IFsm<PlayerFSM> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<PlayerFSM> fsm)
    {
        base.OnEnter(fsm);
        _menuType = MenuType.None;
        GameEntry.Event.Subscribe(PlayerAtkEventArgs.EventId,AtkEvent);
        GameEntry.Event.Subscribe(PlayerDefEventArgs.EventId,DefEvent);
        GameEntry.Event.Subscribe(PlayerLeaveEventArgs.EventId,LeaveEvent);
        GameEntry.Event.Subscribe(PlayerPropEventArgs.EventId,PropEvent);
        GameEntry.Event.Subscribe(PlayerSkillEventArgs.EventId,SkillEvent);
    }

    protected override void OnUpdate(IFsm<PlayerFSM> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

        switch (_menuType)
        {
            case MenuType.Atk:
                fsm.SetData<VarInt32>("targetId",targetId);
                ChangeState<PlayerMoveTarget>(fsm);
                break;
            case MenuType.Def:
                ChangeState<PlayerDef>(fsm);
                break;
            case MenuType.Leave:
                ChangeState<PlayerLeave>(fsm);
                break;
            case MenuType.Prop:
                ChangeState<PlayerProp>(fsm);
                break;
            case MenuType.Skill:
                ChangeState<PlayerSkill>(fsm);
                break;
            default:
                break;
        }

    }

    protected override void OnLeave(IFsm<PlayerFSM> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
        GameEntry.Event.Unsubscribe(PlayerAtkEventArgs.EventId,AtkEvent);
        GameEntry.Event.Unsubscribe(PlayerDefEventArgs.EventId,DefEvent);
        GameEntry.Event.Unsubscribe(PlayerLeaveEventArgs.EventId,LeaveEvent);
        GameEntry.Event.Unsubscribe(PlayerPropEventArgs.EventId,PropEvent);
        GameEntry.Event.Unsubscribe(PlayerSkillEventArgs.EventId,SkillEvent);
    }
    private void AtkEvent(object obj,GameEventArgs args)
    {
        PlayerAtkEventArgs ne = (PlayerAtkEventArgs)args;
        targetId = ne.targetId;
        _menuType = MenuType.Atk;
    }
    private void DefEvent(object obj,GameEventArgs args)
    {
        _menuType = MenuType.Def;
    }
    private void LeaveEvent(object obj,GameEventArgs args)
    {
        _menuType = MenuType.Leave;
    }
    private void PropEvent(object obj,GameEventArgs args)
    {
        _menuType = MenuType.Prop;
    }
    private void SkillEvent(object obj,GameEventArgs args)
    {
        _menuType = MenuType.Skill;
    }

}
