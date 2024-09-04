using System;
using System.Collections;
using System.Collections.Generic;
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
