using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public enum PlayerType
{
    None,
    Hero,
    Enemy
}

public class PlayerBase : EntityLogic
{
    
    public string name;
    public int entityId;
    public float TimeSpeed=0;
    public PlayerType PlayerType = PlayerType.None;
    public float TimelineInitPos = 0;
    
    

    
}
