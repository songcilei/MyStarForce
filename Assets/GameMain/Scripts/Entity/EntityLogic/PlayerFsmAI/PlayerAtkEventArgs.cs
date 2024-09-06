using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class PlayerAtkEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(PlayerAtkEventArgs).GetHashCode();
    public int targetId;
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

    public override int Id {
        get
        {
            return EventId;
        }
    }

    public static PlayerAtkEventArgs Create(int targetId)
    {
        PlayerAtkEventArgs ne = ReferencePool.Acquire<PlayerAtkEventArgs>();
        ne.targetId = targetId;
        return ne;
    }

}
