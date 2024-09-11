using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class PlayerLeaveEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(PlayerLeaveEventArgs).GetHashCode();
    public override void Clear()
    {
    }

    public override int Id {
        get
        {
            return EventId;
        }
    }

    public static PlayerLeaveEventArgs Create()
    {
        PlayerLeaveEventArgs ne = ReferencePool.Acquire<PlayerLeaveEventArgs>();
        return ne;
    }

}
