using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;

public class ReferencePoolPlayer : MonoBehaviour
{
    private ReferencePoolInfo[] Tem;
    private State state1, state2;
    private StructData StructData1, StructData2, StructData3;

    private void Start()
    {
        //创建状态列表（使用引用池）
        state1 = ReferencePool.Acquire<State>();
        state1.Add("游走状态");
        state2 = ReferencePool.Acquire<State>();
        state2.Add("移动状态");


        StructData1 = ReferencePool.Acquire<StructData>();
        StructData1.Add("游走状态",new Vector2Int(0,0));
        StructData2 = ReferencePool.Acquire<StructData>();
        StructData2.Add("移动状态",new Vector2Int(1,15));
        StructData3 = ReferencePool.Acquire<StructData>();
        StructData3.Add("潜行状态",new Vector2Int(4,15));
        ReferencePool.Add<StructData>(4);

        Tem = ReferencePool.GetAllReferencePoolInfos();
        foreach (var item in Tem)
        {
            Debug.LogError(item.Type+":"+item.UsingReferenceCount);
        }
        
    }



    [ContextMenu("ReleaseItem")]
    public void ReleaseItem()
    {
        ReferencePool.Release(state2);

        foreach (var item in Tem)
        {
            Debug.LogError(item.Type+":"+item.UsingReferenceCount);
        }
        
    }


    [ContextMenu("ReleaseList")]
    public void ReleaseList()
    {
        ReferencePool.Release(StructData1);
        ReferencePool.Release(StructData3);
        ReferencePool.RemoveAll(typeof(StructData));
        foreach (var item in Tem)
        {
            Debug.LogError(item.Type+":"+item.UsingReferenceCount); 
        }
    }

    [ContextMenu("AddItem")]
    public void AddItem()
    {
        StructData C = ReferencePool.Acquire<StructData>();
    }

    [ContextMenu("LogAllReference")]
    public void LogAllReference()
    {
        Tem = ReferencePool.GetAllReferencePoolInfos();
        foreach (var item in Tem)
        {
            Debug.LogError(item.Type+":"+item.UsingReferenceCount);
        }
    }

}


public class State : IReference
{
    private string StateName;

    public State()
    {
    }

    public void Add(string StateName)
    {
        this.StateName = StateName;
    }

    public void Clear()
    {
        StateName = null;
    }
}

public class StructData : IReference
{
    private string StateName;
    private Vector2Int pos;

    public StructData()
    {
        
    }

    public void Add(string StateName,Vector2Int pos)
    {
        this.StateName = StateName;
        this.pos = pos;
    }

    public void Clear()
    {
        StateName = null;
        pos = default(Vector2Int);
    }



}