using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameFramework.Event;
using Sirenix.OdinInspector;
using StarForce;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;
using Object = System.Object;
using Random = UnityEngine.Random;

public class BattleComponent : GameFrameworkComponent
{
    public BattleType _battleType = BattleType.None;
    public List<PlayerFSM> PlayerFsmList = new List<PlayerFSM>();
    private List<PlayerFSM> heroFsmList = new List<PlayerFSM>();
    private List<PlayerFSM> enemyFsmList = new List<PlayerFSM>();
    public Dictionary<string, BattleBase> battleHeadUIList = new Dictionary<string, BattleBase>();
    public RectTransform Timeline;

    public float YOffset = 15;
    public RectTransform PlayerHead;
    public RectTransform PlayerHeadTemp;
    public RectTransform PplayerHeadIns;

    public bool RunBattleState = false;

    public RectTransform StartPos;
    public RectTransform EndPos;
    private float StartPosX;
    private float EndPosX;
    public float WidthX;
    
    
    private float timeFrame = 0;
    public float TimeStandardSpeed = 1;
    private bool FreeTime = false;


    public Transform heroTransform;


    public UI_Menu_Panel UIMenuPanel;
    
    
    public PlayerFSM CurrentPlayer;
    private string CurrentKey;

    private int LoadFallbackIndex = 0;
    private bool LoadCheck = false;
    private int entitylistCount = 0;
    
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
                if (LoadCheck)
                {
                    Debug.Log(LoadFallbackIndex);
                    if (LoadFallbackIndex == entitylistCount)
                    {
                        Debug.Log("entity countss:"+entitylist.Count);
                        SetPlayerToList(entitylist);
                        LoadCheck = false;
                        GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId,LoadEntitySuccess);
                        GameEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId,LoadFailureLoad);

                    }
                }

                if (RunBattleState)
                {
                    _battleType = BattleType.OnInit;//change state
                    RectTransform rect = Timeline.GetComponent<RectTransform>();
                    StartPosX = StartPos.position.x;
                    EndPosX = EndPos.position.x;
                    WidthX = Mathf.Abs(EndPosX - StartPosX);
                    
                }

                Debug.Log("battle None");
                break;
            case BattleType.OnInit:
                Init();
                _battleType = BattleType.OnStart;
                break;
            case BattleType.OnStart:
                heroTransform = GameObject.Find("hero").transform;
    //enter BattleMgr  
                GameEntry.Event.Fire(this,StartBattleEventArgs.Create(heroTransform.position,heroFsmList,enemyFsmList));
                _battleType = BattleType.OnUpdate;
                break;
            case BattleType.OnUpdate:
                UpdateComponent();
                break;
            case BattleType.OnLeave:
                GameEntry.Event.Fire(this,CloseBattleEventArgs.Create(heroTransform.position,heroFsmList,enemyFsmList));
                Shutdown();
                RunBattleState = false;
                _battleType = BattleType.None;
                break;
            default:
                break;
        }    
    }
/// <summary>
/// run 
/// </summary>
/// <param name="herolist"></param>
    public void RunBattle(List<PlayerFSM> entitylist)
    {
        if (RunBattleState)
        {
            return;
        }

        
        SetPlayerToList(entitylist);
    }

public void RunBattle(List<int> entityIDlist)
{
    if (RunBattleState)
    {
        return;
    }
    GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId,LoadEntitySuccess);
    GameEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId,LoadFailureLoad);

    LoadFallbackIndex = 0;
    entitylist.Clear();
    
    List<TeamBase> teams = GameEntry.TeamComponent.GetTeam();
    foreach (var team in teams)
    {
        Debug.Log("team:"+team.TypeId);
        GameEntry.Entity.ShowPlayer(new PlayerFSMData(GameEntry.Entity.GenerateSerialId(),team.TypeId,PlayerType.Hero));
    }
    
    foreach (var entityID in entityIDlist)
    {
        // GameEntry.Entity.ShowPlayer(new PlayerFSMData(GameEntry.Entity.GetInstanceID(),entityID,PlayerType.Enemy));
        GameEntry.Entity.ShowPlayer(new PlayerFSMData(GameEntry.Entity.GenerateSerialId(),entityID,PlayerType.Enemy));
    }
    entitylistCount = teams.Count + entityIDlist.Count;
    Debug.Log("entitylistCount:"+entitylistCount);
    LoadCheck = true;
}

    private void Init()
    {
        ShowUI();
        heroFsmList.Clear();
        enemyFsmList.Clear();
        GameObject HeadTemp = PlayerHeadTemp.gameObject;
        Debug.Log("PlayerFsmList:"+PlayerFsmList.Count);
        foreach (var playerF in PlayerFsmList)
        {
            Debug.Log("type:"+playerF.PlayerType);
            RectTransform insTran = Instantiate(HeadTemp,PlayerHead).GetComponent<RectTransform>();
            Texture2D icon = playerF.HeadIcon;
            RawImage icon_ui = insTran.transform.Find("HeadIcon")?.GetComponent<RawImage>();
            icon_ui.texture= icon;
            insTran.gameObject.SetActive(true);
   
            float initPos = playerF.TimelineInitPos*WidthX;
            Vector3 startPosition = Vector3.zero;
            Vector3 endPosition = Vector3.zero;
            switch (playerF.PlayerType)
            {
                case PlayerType.Hero:
                    startPosition = new Vector3(StartPosX+initPos, PlayerHead.position.y+YOffset, 0);
                    heroFsmList.Add(playerF);
                    break;
                case PlayerType.Enemy:
                    startPosition = new Vector3(StartPosX+initPos, PlayerHead.position.y-YOffset, 0);
                    enemyFsmList.Add(playerF);
                    break;
                default:
                    break;
            }
            endPosition = startPosition + new Vector3(WidthX,0,0);
            
            
            
            BattleBase battleBase = new BattleBase(playerF.name,playerF.entityId,playerF.Spd,insTran,icon,startPosition,endPosition);
            if (playerF != null)
            {
                if (!battleHeadUIList.ContainsKey(playerF.name))
                {
                    battleHeadUIList.Add(playerF.name,battleBase);
                }
            }
        }
        
        
    }


    private void UpdateComponent()
    {
        
        if (CheckHeroHPZero() || CheckEnemyHPZero())
        {
            _battleType = BattleType.OnLeave;
        }

        if (FreeTime)
        {
            return;
        }

        timeFrame = Time.deltaTime*TimeStandardSpeed;
        for (int i = 0; i < battleHeadUIList.Count; i++)
        {
            string key = battleHeadUIList.ElementAt(i).Key;

            BattleBase mbase = battleHeadUIList[key];
            mbase.m_TotalTime += timeFrame*mbase.TimeSpeed;
            if (mbase.m_TotalTime>100000)
            {
                mbase.m_TotalTime = 0;
            }

            mbase.m_LocalTime = battleHeadUIList[key].m_TotalTime % 100;
            mbase.RectTrans.position = new Vector3(mbase.StartPosition.x + (WidthX * mbase.m_LocalTime / 100),mbase.StartPosition.y,mbase.StartPosition.z);
//action
            if (Mathf.Abs(mbase.RectTrans.position.x - EndPosX)<10)
            {
                Debug.Log("action!");
                PlayerFSM playerFsm = (PlayerFSM)GameEntry.Entity.GetEntity(mbase.EntityId).Logic;


                CurrentPlayer = playerFsm;
                CurrentKey = key;

                switch (playerFsm.PlayerType)
                {
                    case PlayerType.Hero:
                        playerFsm.OnAction();
                        if (UIMenuPanel)
                        {
                            UIMenuPanel.ShowUIPanel();
                            UIMenuPanel.HideSkillUIPanel();
                        }
                        break;
                    case PlayerType.Enemy:
                        //AI
                        playerFsm.OnAIAction();
                        
                        break;
                    default:
                        break;
                }
                break;
            }
        }
    }

    /// <summary>
    /// debbug => leave current
    /// </summary>
    public void nextPlayerFsm()
    {
        battleHeadUIList[CurrentKey].InitStartPos();
        CurrentPlayer.LeaveFsm();
    }


//set free time
    public void SetFreeTimeState(bool state)
    {
        FreeTime = state;
    }

    private void Shutdown()
    {
        
        //Hide UI
        HideUI();
//clear list
        if (PlayerFsmList.Count>0)
        {
            PlayerFsmList.Clear();
        }
    }

    public bool CheckHeroHPZero()
    {
        bool death = true;
        foreach (var hero in heroFsmList)
        {
            if (hero.Hp>0)
            {
                death = false;
                break;
            }
        }
        return death;
    }

    public bool CheckEnemyHPZero()
    {
        bool death = true;
        foreach (var enemy in enemyFsmList)
        {
            if (enemy.Hp>0)
            {
                death = false;
                break;
            }
        }
        return death;
    }

    private void HideUI()
    {
        Timeline.gameObject.SetActive(false);

        foreach (var battleBase in battleHeadUIList)
        {
            Debug.Log(battleBase.Value.RectTrans.name);
            GameObject.Destroy(battleBase.Value.RectTrans.gameObject);
        }
        battleHeadUIList.Clear();
    }

    private void ShowUI()
    {
        Timeline.gameObject.SetActive(true);
    }


    private void SetPlayerToList(List<PlayerFSM> Entitylist)
    {
        foreach (var entity in Entitylist)
        {
            PlayerFsmList.Add(entity);
        }

        Debug.Log("Entity Counts:"+PlayerFsmList.Count);
        RunBattleState = true;
    }


    public List<PlayerFSM> GetLiveHeroList()
    {
        List<PlayerFSM> liveHeroList = new List<PlayerFSM>();
        foreach (var hero in heroFsmList)
        {
            if (hero.Hp>0)
            {
                liveHeroList.Add(hero);
            }
        }

        return liveHeroList;
    }

    public PlayerFSM GetSoloLiveHero()
    {
        List<PlayerFSM> heroList = GetLiveHeroList();
        int index = Random.Range(0, heroList.Count);
        return heroList[index];
    }

    List<PlayerFSM> entitylist = new List<PlayerFSM>();
    void LoadEntitySuccess(Object obj,GameEventArgs args)
    {
        ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs) args;
        if (ne.EntityLogicType == typeof(PlayerFSM))
        {
            entitylist.Add((PlayerFSM)ne.Entity.Logic);
            LoadFallbackIndex++;
        }
    }
    
    private void LoadFailureLoad(object obj,GameEventArgs args)
    {
        ShowEntityFailureEventArgs ne = (ShowEntityFailureEventArgs)args;
        Debug.LogError(ne.EntityAssetName + "Load Error!!!!");
    }

}
