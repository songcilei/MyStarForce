using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class ClearTeamListArgs : GameEventArgs
{
    public static readonly int EventId = typeof(ClearTeamListArgs).GetHashCode();

    public static ClearTeamListArgs Create()
    {
        ClearTeamListArgs clearTeamListArgs = ReferencePool.Acquire<ClearTeamListArgs>();
        return clearTeamListArgs;
    }


    public override void Clear()
    {
        
    }

    public override int Id
    {
        get { return EventId; }
    }
}
