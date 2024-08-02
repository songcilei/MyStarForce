using System;
using System.Collections;
using System.Collections.Generic;
using StarForce;
using UnityEngine;

public class iConfigTest : MonoBehaviour
{
    private void Awake()
    {
        LoadConfig();
    }

    private void LoadConfig()
    {
        //这部分如果走了默认的 流程  就自动了 不用额外再写一次
        //全局配置路径
        string configAssetName = "Assets/GameMain/Configs/DefaultConfig.txt";
        //读取
        GameEntry.Config.ReadData(configAssetName);//如果读取了的情况下再次读取可能会出错
    }

    void Start()
    {
        //获取GameID
        int GameID = GameEntry.Config.GetInt("Scene.Main");
        Debug.Log("GameID:"+GameID);
    }

    
    void Update()
    {
        
    }
}
