using System.Collections;
using System.Collections.Generic;
using ECM2;
using GameFramework.Resource;
using StarForce;
using Unity.Mathematics;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;

public class PlayerControlEntity : EntityLogic
{
    private PlayerControlEntityData playerControlEntityData;

    private Transform Model;
    private string ModelName;
    private string Icon;
    private LoadAssetCallbacks LoadAssetCallBack_Model;
    private LoadAssetCallbacks LoadAssetCallBack_Icon;
    private Character _character;
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        Model = transform.Find("Model");
        playerControlEntityData =  userData as PlayerControlEntityData;
        ModelName = playerControlEntityData.ModelName;
        Icon = playerControlEntityData.Icon;

        LoadAssetCallBack_Model = new LoadAssetCallbacks(LoadModelSuccess);
        LoadAssetCallBack_Icon = new LoadAssetCallbacks(LoadIconSuccess);
        TryGetComponent<Character>(out _character);
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        string modelPath = AssetUtility.GetNPCModelAsset(ModelName);
        GameEntry.Resource.LoadAsset(modelPath,LoadAssetCallBack_Model);

        string iconPath = AssetUtility.GetPlayerHeadIconAsset(Icon);
        GameEntry.Resource.LoadAsset(iconPath, LoadAssetCallBack_Icon);

    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
    }

    private void LoadModelSuccess(string assetName,object asset,float duration,object userData)
    {
        var obj = asset as GameObject;
        GameObject model = Instantiate(obj, Model.transform);
        model.transform.localPosition = Vector3.zero;
        model.transform.rotation = quaternion.identity;
        model.transform.localScale = Vector3.one;
        _character.InitCacheComponent();//初始化Chara状态
        Debug.Log("log success for model");
        AnimationControl animationControl;
        if ( model.TryGetComponent<AnimationControl>(out animationControl))
        {
            animationControl.InitAnimation();
        }
        
        Character character = this.GetComponent<Character>();
        
    }

    private void LoadIconSuccess(string assetName,object asset,float duration,object userData)
    {
        var icon = asset as Texture2D;
    }



}
