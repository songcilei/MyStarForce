using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class CloseBattleEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(CloseBattleEventArgs).GetHashCode();

    public override void Clear()
    {
    }

    public static CloseBattleEventArgs Create()
    {
        CloseBattleEventArgs closeBattleEventArgs = ReferencePool.Acquire<CloseBattleEventArgs>();
        return closeBattleEventArgs;
    }

    public override int Id {
        get
        {
            return EventId;
        }
    }
}
