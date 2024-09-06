using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class AddRandomTeamListArgs : GameEventArgs
{
    public static readonly int EventId = typeof(AddRandomTeamListArgs).GetHashCode();

    public static AddRandomTeamListArgs Create()
    {
        AddRandomTeamListArgs addRandomTeamListArgs = ReferencePool.Acquire<AddRandomTeamListArgs>();
        return addRandomTeamListArgs;
    }


    public override void Clear()
    {
        
    }

    public override int Id
    {
        get { return EventId; }
    }
}
