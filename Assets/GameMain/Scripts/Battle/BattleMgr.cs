using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMgr : MonoBehaviour
{
    public static BattleMgr Instance;
    public Transform Scene;
    public Transform HeropPints;
    public Transform EnemyPoints;
    public Camera camera;

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
            GameObject.DestroyImmediate(this);
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
    public void CreatBattle(Vector3 playPos)
    {
        if (battlePoints==null)
        {
            Debug.LogError("battlePoint is null!!");
            return;
        }

        BattlePointValue battlePoint = battlePoints.GetNearBattlePoint(playPos);
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
