using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

public class BattleComponent : GameFrameworkComponent
{
    public BattleType _battleType = BattleType.None;
    public List<PlayerFSM> PlayerFsmList = new List<PlayerFSM>();

    public RectTransform Timeline;
    public RectTransform PlayerHead;
    public RectTransform PlayerHeadIns;

    protected override void Awake()
    {
        base.Awake();
    }



    private void Start()
    {
        // float width = Timeline.get
    }


    [Button]
    public void TimeCenter()
    {
        Debug.Log("Pivot:"+Timeline.pivot);
        Debug.Log("anchoredPosition:"+Timeline.anchoredPosition);
        Debug.Log("position:"+Timeline.position);
        
    }




    private void Update()
    {
        switch (_battleType)
        {
            case BattleType.None:
                if (PlayerFsmList.Count>1)
                {
                    PlayerFsmList.Clear();
                    
                }
                break;
            case BattleType.OnInit:
                break;
            case BattleType.OnStart:
                for (int i = 0; i < PlayerFsmList.Count; i++)
                {
                    
                }
                break;
            case BattleType.OnUpdate:
                for (int i = 0; i < PlayerFsmList.Count; i++)
                {
                    
                }
                break;
            case BattleType.OnLeave:
                break;
            default:
                break;
        }    
    }

    public void SetPlayerToList(List<PlayerFSM> herolist)
    {
        PlayerFsmList = herolist;
    }

}
