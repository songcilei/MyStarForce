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
    public Dictionary<string, Transform> battleHeadUIList = new Dictionary<string, Transform>();
    public RectTransform Timeline;
    public RectTransform PlayerHead;
    public RectTransform PlayerHeadTemp;
    public RectTransform PplayerHeadIns;
    public bool RunBattleState = false;

    public RectTransform StartPos;
    public RectTransform EndPos;
    
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
        Debug.Log("Pivot:"+Timeline.pivot);//获取轴心点
        Debug.Log("anchoredPosition:"+Timeline.anchoredPosition);//获取自身轴心点距离 锚点的 距离
        Debug.Log("position:"+Timeline.position);//获取世界坐标
        Debug.Log("RectWidht:"+Timeline.rect.width);//获取UI整体的宽  即AABB 的Extens*2
        Debug.Log("RectHeight:"+Timeline.rect.height);//获取UI整体的高  既AABB 的Extens*2
        Debug.Log("RectPosition"+Timeline.rect.position);//是中心点 和 UI 的左下角的 距离  即 rect 的起始点位置
        Debug.Log("RectCenter"+Timeline.rect.center);// 0 0  即中心点
    }



    private void Update()
    {
        switch (_battleType)
        {
            case BattleType.None:
                if (RunBattleState)
                {
                    _battleType = BattleType.OnInit;
                    RunBattleState = false;
                }

                break;
            case BattleType.OnInit:
                Init();
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
                Shutdown();
                break;
            default:
                break;
        }    
    }
/// <summary>
/// run 
/// </summary>
/// <param name="herolist"></param>
    public void RunBattle(List<Entity> herolist)
    {
        if (RunBattleState)
        {
            return;
        }

        
        SetPlayerToList(herolist);
    }

    private void Init()
    {
        ShowUI();

        GameObject HeadTemp = PlayerHeadTemp.gameObject;
        foreach (var playerF in PlayerFsmList)
        {
            
            Texture2D icon = playerF.HeadIcon;
            // battleHeadUIList.Add(playerF.);
            // Transform  Instantiate(HeadTemp);
        }
    }

    private void Shutdown()
    {
//clear list
        if (PlayerFsmList.Count>0)
        {
            PlayerFsmList.Clear();
        }

        if (battleHeadUIList.Count>0)
        {
            battleHeadUIList.Clear();
        }

//Hide UI
        HideUI();
    }
    
    

    private void HideUI()
    {
        Timeline.gameObject.SetActive(false);
    }

    private void ShowUI()
    {
        Timeline.gameObject.SetActive(true);
    }


    private void SetPlayerToList(List<Entity> entitylist)
    {
        foreach (var entity in entitylist)
        {
            PlayerFsmList.Add(entity.Logic as PlayerFSM);
        }
        RunBattleState = true;
    }

}
