using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class StartBattleEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(StartBattleEventArgs).GetHashCode();
    public Vector3 playPos;
    public List<PlayerFSM> heroFsm = new List<PlayerFSM>();
    public List<PlayerFSM> enemyFsm = new List<PlayerFSM>();
    
    public StartBattleEventArgs()
    {
        
    }

    public override void Clear()
    {
        // throw new System.NotImplementedException();
    }

    public static StartBattleEventArgs Create(Vector3 playPos,List<PlayerFSM> heroList,List<PlayerFSM> enemyList)
    {
        StartBattleEventArgs startBattleEventArgs = ReferencePool.Acquire<StartBattleEventArgs>();
        startBattleEventArgs.playPos = playPos;
        startBattleEventArgs.heroFsm = heroList;
        startBattleEventArgs.enemyFsm = enemyList;
        return startBattleEventArgs;
    }

    public override int Id {
        get
        {
            return EventId;
        }
    }
}
