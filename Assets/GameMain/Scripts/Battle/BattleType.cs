using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleType
{
    None,
    OnInit,
    OnStart,
    OnUpdate,
    OnLeave
}

public class BattleBase
{
    public string Name;
    public RectTransform RectTrans;
    public Texture2D Icon;
    public float TimeSpeed=1;
    public float m_TotalTime=0;
    public float m_LocalTime = 0;
    public Vector3 StartPosition;
    public Vector3 EndPosition;
    public int EntityId = 0;
    public BattleBase(string name,int entityId,float timeSpeed,RectTransform trans,Texture2D icon,Vector3 startPosition,Vector3 endPosition)
    {
        Name = name;
        EntityId = entityId;
        TimeSpeed = timeSpeed;
        RectTrans = trans;
        Icon = icon;
        StartPosition = startPosition;
        EndPosition = endPosition;
    }
}