using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using GameFramework.Resource;
using StarForce;
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
        
        
        PlayerFSMData playerFsmData = userData as PlayerFSMData;
        entityId = playerFsmData.EntityId;
        
        string modelPath = AssetUtility.GetNPCModelAsset(playerFsmData.ModelName);
        GameEntry.Resource.LoadAsset(modelPath,LoadAssetCallbacks_Model);

        string iconPath = AssetUtility.GetPlayerHeadIconAsset(playerFsmData.Icon);
        GameEntry.Resource.LoadAsset(iconPath,LoadAssetSuccessCallback_Icon);

        
        


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
        Model = Instantiate(obj);
        modelLoadEd = true;
    }
    private void LoadIconSuccess(string assetName, object asset, float duration, object userData)
    {
        HeadIcon = asset as Texture2D;
        IconLoadEd = true;
    }

}
