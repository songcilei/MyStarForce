using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using GameFramework.Resource;
using StarForce;
using Unity.Mathematics;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;

public class PlayerFSM : PlayerBase
{
    public PlayerFSMData PlayerFsmData = null;
    public Texture2D HeadIcon;
    public GameObject Model;
    private LoadAssetCallbacks LoadAssetCallbacks_Model;
    private LoadAssetCallbacks LoadAssetSuccessCallback_Icon;

    private bool modelLoadEd = false;
    private bool IconLoadEd = false;
    private string modelName;
    private string icon;
    public float Radius;//模型检测半径
    public bool IsDef = false;//防御模式
    public Vector3 InitActionPos;//battle场景初始化位置
    public Quaternion InitActionRotation;//battle场景初始化旋转
    private IFsm<PlayerFSM> playFsm;
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);

        LoadAssetCallbacks_Model = new LoadAssetCallbacks(LoadModelSuccess);
        LoadAssetSuccessCallback_Icon = new LoadAssetCallbacks(LoadIconSuccess);

        Model = transform.Find("Model").gameObject;
        
        PlayerFSMData playerFsmData = userData as PlayerFSMData;
        entityId = playerFsmData.EntityId;
        PlayerType = playerFsmData.PlayerType;
        Spd = playerFsmData.Spd;
        modelName = playerFsmData.ModelName;
        icon = playerFsmData.Icon;
        Radius = playerFsmData.Radius;
        //-------------------------------------
        Level = playerFsmData.Level;
        Atk = playerFsmData.Atk;
        Mag = playerFsmData.Mag;
        Def = playerFsmData.Def;
        Mdf = playerFsmData.Mdf;
        Hp = playerFsmData.Hp;
        Mp = playerFsmData.Mp;
        Luck = playerFsmData.Luck;
        
        
        //---------------------------FSM
        FsmState<PlayerFSM>[] PlayerCoreFSM =
        {
            new PlayerAtk(),
            new PlayerDef(),
            new PlayerIdle(),
            new PlayerMenu(),
            new PlayerProp(),
            new PlayerSkill(),
            new PlayerLeave(),
            new PlayerMoveTarget(),
            new PlayerDmg()
        };

        playFsm = GameEntry.Fsm.CreateFsm(this, PlayerCoreFSM);
        playFsm.Start<PlayerIdle>();



    }

    public override void OnAction()
    {
        base.OnAction();
        GameEntry.BattleSystem.SetFreeTimeState(true);
        playFsm.CastChangeState<PlayerMenu>();
        InitActionPos = this.transform.position;
        InitActionRotation = this.transform.rotation;
        Debug.Log("name:"+name +"::::Action");
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        
        string modelPath = AssetUtility.GetNPCModelAsset(modelName);
        GameEntry.Resource.LoadAsset(modelPath,LoadAssetCallbacks_Model);
        Debug.Log("loading model!");
        name = modelName + entityId;
        
        string iconPath = AssetUtility.GetPlayerHeadIconAsset(icon);
        GameEntry.Resource.LoadAsset(iconPath,LoadAssetSuccessCallback_Icon);
        
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

    public void Damage(int atk)
    {
        //防御模式
        if (IsDef)
        {
            atk = atk / 2;
        }

        Hp -= atk;
        playFsm.CastChangeState<PlayerDmg>();
    }

    private void LoadModelSuccess(string assetName, object asset, float duration, object userData)
    {
        var obj = asset as GameObject;
        GameObject model = Instantiate(obj,Model.transform);
        model.transform.localPosition = Vector3.zero;
        model.transform.rotation = quaternion.identity;
        model.transform.localScale = Vector3.one;
        Debug.Log("log success for model!!");
        modelLoadEd = true;
    }
    private void LoadIconSuccess(string assetName, object asset, float duration, object userData)
    {
        HeadIcon = asset as Texture2D;
        IconLoadEd = true;
    }

}
