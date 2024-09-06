using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class PlayerPropEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(PlayerPropEventArgs).GetHashCode();
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

    public static PlayerPropEventArgs Create()
    {
        PlayerPropEventArgs ne = ReferencePool.Acquire<PlayerPropEventArgs>();
        return ne;
    }
}
