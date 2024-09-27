using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using Sirenix.OdinInspector;
using StarForce;
using UnityEngine;

public class BattleMgr : MonoBehaviour
{
    public static BattleMgr Instance;
    public Transform Scene;
    public Transform HeropPints;
    public Transform EnemyPoints;
    public new Camera camera;

    private List<Transform> heroPointList = new List<Transform>();
    private List<Transform> enemyPointList = new List<Transform>();

    private BattlePoints battlePoints;

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }

        camera.enabled = false;
        
        GameEntry.Event.Subscribe(StartBattleEventArgs.EventId,StartBattle);
        GameEntry.Event.Subscribe(CloseBattleEventArgs.EventId,CloseBattle);
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }


    void Init()
    {
        heroPointList = GetPointsToList(HeropPints);
        enemyPointList = GetPointsToList(EnemyPoints);
        
        //init battle mgr
        battlePoints = FindObjectOfType<BattlePoints>();
    }


    /// <summary>
    /// init battle scene
    /// </summary>
    /// <param name="playPos"></param>
    public void CreatBattle(Vector3 playPos,List<PlayerFSM> heroFsm,List<PlayerFSM> enemyFsm)
    {
        if (battlePoints==null)
        {
            Debug.LogError("battlePoint is null!!");
            return;
        }

        camera.enabled = true;
        BattlePointValue battlePos = battlePoints.GetNearBattlePoint(playPos);
        
        //load asset battle scene

        this.transform.position = battlePos.trans.position;
        this.transform.rotation = battlePos.trans.rotation;

        int heroCount = heroFsm.Count;
        int enemyCount = enemyFsm.Count;
        for (int i = 0; i < heroCount; i++)
        {
            PlayerFSM hero = heroFsm[i];
            hero.transform.position = heroPointList[i].position;
            hero.transform.rotation = heroPointList[i].rotation;

        }

        for (int i = 0; i < enemyCount; i++)
        {
            PlayerFSM enemy = enemyFsm[i];
            enemy.transform.position = enemyPointList[i].position;
            enemy.transform.rotation = enemyPointList[i].rotation;
        }
    }

    public void CloseBattle(object obj,GameEventArgs args)
    {
        CloseBattleEventArgs ne = (CloseBattleEventArgs)args;
        if (ne == null)
        {
            return;
        }

        //hide hero
        foreach (var hero in ne.heroFsm)
        {

            if (GameEntry.Entity.HasEntity(hero.entityId))
            {
                hero.StopFsm();
                GameEntry.Entity.HideEntity(hero.entityId);
            }
            else
            {
                Debug.LogError("dont have EntityId:"+hero.entityId);
            }
        }
        //hide enemy
        foreach (var enemy in ne.enemyFsm)
        {

            if (GameEntry.Entity.HasEntity(enemy.entityId))
            {
                enemy.StopFsm();
                GameEntry.Entity.HideEntity(enemy.entityId);
            }
            else
            {
                Debug.LogError("dont have EntityId:"+enemy.entityId);
            }
        }
        
        camera.enabled = false;
    }



    public void StartBattle(object obj,GameEventArgs args)
    {
        StartBattleEventArgs ne = (StartBattleEventArgs)args;
        if (ne == null||ne.Id == -1)
        {
            return;
        }

        Debug.Log("hero:"+ne.heroFsm.Count);
        Debug.Log("enemy:"+ne.enemyFsm.Count);
        CreatBattle(ne.playPos, ne.heroFsm, ne.enemyFsm);
    }


    List<Transform> GetPointsToList(Transform tran)
    {
        int count = tran.childCount;
        List<Transform> tempList = new List<Transform>();
        for (int i = 0; i < count; i++)
        {
             tempList.Add(tran.GetChild(i));
        }
        return tempList;
    }

}
