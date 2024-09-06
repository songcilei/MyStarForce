using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TeamBase
{
    public int TypeId;
    public PlayerId PlayerId;
    public int Level;
    public float Grow;
    public int[] Skills;
    
    //-------------------------------------
    public float Spd=0;
    public int Atk = 0;
    public int Mag = 0;
    public int Def = 0;
    public int Mdf = 0;
    public int Hp = 0;
    public int Mp = 0;

    public int Luck = 0;
}
