using System;
using System.Collections;
using System.Collections.Generic;
using StarForce;
using Unity.VisualScripting;
using UnityEngine;
using UnityGameFramework.Runtime;
using AssetUtility = StarForce.AssetUtility;

public class DataTableTest : MonoBehaviour
{
    private DataTableComponent DataTable;
    private string[] DataTableNames;

    private void Start()
    {
        DataTable = UnityGameFramework.Runtime.GameEntry.GetComponent<DataTableComponent>();
        DataTableNames = new string[2] { "Scene","Music" };
        
        //预加载Data tables数据表
        foreach (var dataTableName in DataTableNames)
        {
            //LoadDataTable(dataTableName);
        }
        
    }

    /// <summary>
    /// 加载表数据
    /// </summary>
    /// <param name="dataTableName"></param>
    private void LoadDataTable(string dataTableName)
    {
        string dataTableAssetName = AssetUtility.GetDataTableAsset(dataTableName, false);
        //表名，表名资源路径，用户自定义数据
        DataTable.LoadDataTable(dataTableName,dataTableAssetName,this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            QueryData();
        }
    }

    /// <summary>
    /// 查询表数据
    /// </summary>
    private void QueryData()
    {
        //获取指定表数据列表
        var drScene = DataTable.GetDataTable<DRScene>();
        
        //根据行号查找数据
        DRScene scene1 = drScene.GetDataRow(1);
        Debug.Log("获取第一行数据的ID值："+scene1.Id);

        DRScene scene2 = drScene.GetDataRow(x => x.Id == 1);
        DRScene scene3 = drScene.GetDataRow(x => x.AssetName == "Menu");
        Debug.Log("获取Id等于1 的场景名："+scene2.AssetName);
        Debug.Log("获取场景名等于Menu的id值："+scene3.Id);
        
        //根据条件查找多条数据
        DRScene[] scene4 = drScene.GetDataRows(x => x.Id > 0);
        for (int i = 0; i < scene4.Length; i++)
        {
            Debug.Log("获取id大于0的背景音乐编号："+scene4[i].BackgroundMusicId);
        }
    }


}
