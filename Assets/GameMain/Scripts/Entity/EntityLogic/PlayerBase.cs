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
    public PlayerType PlayerType = PlayerType.None;
    public float TimelineInitPos = 0;
    public float Spd=0;
    public int Atk = 0;
    public int Mag = 0;
    public int Def = 0;
    public int Mdf = 0;
    public int Hp = 0;
    public int Mp = 0;
    public int Level;
    public int Luck = 0;

    public virtual void OnAction()
    {
        
    }

    public virtual void OnAIAction()
    {
        
    }

}
