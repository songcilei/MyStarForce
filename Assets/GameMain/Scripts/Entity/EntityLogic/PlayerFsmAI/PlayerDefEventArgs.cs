using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class PlayerDefEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(PlayerDefEventArgs).GetHashCode();
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

    public static PlayerDefEventArgs Create()
    {
        PlayerDefEventArgs ne = ReferencePool.Acquire<PlayerDefEventArgs>();
        return ne;
    }
}
