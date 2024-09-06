using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class CloseBattleEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(CloseBattleEventArgs).GetHashCode();
    public Vector3 playPos;
    public List<PlayerFSM> heroFsm = new List<PlayerFSM>();
    public List<PlayerFSM> enemyFsm = new List<PlayerFSM>();
    public override void Clear()
    {
    }

    public static CloseBattleEventArgs Create(Vector3 playPos,List<PlayerFSM> heroList,List<PlayerFSM> enemyList)
    {
        CloseBattleEventArgs closeBattleEventArgs = ReferencePool.Acquire<CloseBattleEventArgs>();
        closeBattleEventArgs.playPos = playPos;
        closeBattleEventArgs.heroFsm = heroList;
        closeBattleEventArgs.enemyFsm = enemyList;
        return closeBattleEventArgs;
    }
    public override int Id {
        get
        {
            return EventId;
        }
    }
}
