using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

public class PlayerSkillEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(PlayerSkillEventArgs).GetHashCode();
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

    public static PlayerSkillEventArgs Create()
    {
        PlayerSkillEventArgs ne = ReferencePool.Acquire<PlayerSkillEventArgs>();
        return ne;
    }

}
