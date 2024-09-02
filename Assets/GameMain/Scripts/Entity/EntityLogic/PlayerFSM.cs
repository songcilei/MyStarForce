using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using GameFramework.Resource;
using StarForce;
using Unity.Mathematics;
using UnityEngine;

public class PlayerFSM : PlayerBase
{
    public PlayerFSMData PlayerFsmData = null;
    public Texture2D HeadIcon;
    public GameObject Model;
    private LoadAssetCallbacks LoadAssetCallbacks_Model;
    private LoadAssetCallbacks LoadAssetSuccessCallback_Icon;

    private bool modelLoadEd = false;
    private bool IconLoadEd = false;
    
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);

        LoadAssetCallbacks_Model = new LoadAssetCallbacks(LoadModelSuccess);
        LoadAssetSuccessCallback_Icon = new LoadAssetCallbacks(LoadIconSuccess);

        Model = transform.Find("Model").gameObject;
        
        PlayerFSMData playerFsmData = userData as PlayerFSMData;
        entityId = playerFsmData.EntityId;
        PlayerType = playerFsmData.PlayerType;
        TimeSpeed = playerFsmData.TimeSpeed;
        string modelPath = AssetUtility.GetNPCModelAsset(playerFsmData.ModelName);
        GameEntry.Resource.LoadAsset(modelPath,LoadAssetCallbacks_Model);

        name = playerFsmData.ModelName + entityId;
        
        string iconPath = AssetUtility.GetPlayerHeadIconAsset(playerFsmData.Icon);
        GameEntry.Resource.LoadAsset(iconPath,LoadAssetSuccessCallback_Icon);

    }

    public override void OnAction()
    {
        base.OnAction();
        GameEntry.BattleSystem.SetFreeTimeState(true);
        Debug.Log("name:"+name +"::::Action");
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        if (modelLoadEd && IconLoadEd)
        {
            Debug.Log("load ed   Model and Icon");
        }
    }
    
    private void LoadModelSuccess(string assetName, object asset, float duration, object userData)
    {
        var obj = asset as GameObject;
        GameObject model = Instantiate(obj,Model.transform);
        model.transform.localPosition = Vector3.zero;
        model.transform.rotation = quaternion.identity;
        model.transform.localScale = Vector3.one;
        
        modelLoadEd = true;
    }
    private void LoadIconSuccess(string assetName, object asset, float duration, object userData)
    {
        HeadIcon = asset as Texture2D;
        IconLoadEd = true;
    }

}
