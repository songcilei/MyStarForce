using System;
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
    public PlayerFSMData PlayerFsmData
    {
        get
        {
            return m_playerFsmData;
        }
    }
    public Texture2D HeadIcon;
    public GameObject Model;
    private LoadAssetCallbacks LoadAssetCallbacks_Model;
    private LoadAssetCallbacks LoadAssetSuccessCallback_Icon;

    private bool modelLoadEd = false;
    private bool IconLoadEd = false;
    private string modelName;
    private string entityName;
    private string icon;
    public float Radius;//模型检测半径
    public bool IsDef = false;//防御模式
    public Vector3 InitActionPos;//battle场景初始化位置
    public Quaternion InitActionRotation;//battle场景初始化旋转
    private IFsm<PlayerFSM> playFsm;
    private string FsmName;
    private GameObject modelInstance;
    private PlayerFSMData m_playerFsmData;
    public Dictionary<string,BufferState> BufferList = new Dictionary<string, BufferState>();
    public Dictionary<string, IAction> SkillList = new Dictionary<string, IAction>();
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);

        LoadAssetCallbacks_Model = new LoadAssetCallbacks(LoadModelSuccess);
        LoadAssetSuccessCallback_Icon = new LoadAssetCallbacks(LoadIconSuccess);
        
    }

    public override void OnAction()
    {
        base.OnAction();
        GameEntry.BattleSystem.SetFreeTimeState(true);
        //buffer loop
        BufferLifeSubOne();
        playFsm.CastChangeState<PlayerMenu>();
        InitActionPos = this.transform.position;
        InitActionRotation = this.transform.rotation;
        Debug.Log("name:"+name +"::::Action");
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        //-----------------------------Init
        Model = transform.Find("Model").gameObject;
        
        m_playerFsmData = userData as PlayerFSMData;
        entityId = m_playerFsmData.EntityId;
        PlayerType = m_playerFsmData.PlayerType;
        Spd = m_playerFsmData.Spd;
        modelName = m_playerFsmData.ModelName;
        entityName = m_playerFsmData.EntityName;
        icon = m_playerFsmData.Icon;
        Radius = m_playerFsmData.Radius;
        TimelineInitPos = m_playerFsmData.TimelineInitPos;
        //-------------------------------------
        Level = m_playerFsmData.Level;
        Atk = m_playerFsmData.Atk;
        Mag = m_playerFsmData.Mag;
        Def = m_playerFsmData.Def;
        Mdf = m_playerFsmData.Mdf;
        Hp = m_playerFsmData.Hp;
        Mp = m_playerFsmData.Mp;
        Luck = m_playerFsmData.Luck;
        
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
            new PlayerLeaveTarget(),
            new PlayerDmg()
        };
        FsmName = entityName + entityId;
        playFsm = GameEntry.Fsm.CreateFsm(FsmName,this, PlayerCoreFSM);
        playFsm.Start<PlayerIdle>();
        
        //--------------------------------------------- Load Asset
        string modelPath = AssetUtility.GetNPCModelAsset(modelName);
        GameEntry.Resource.LoadAsset(modelPath,LoadAssetCallbacks_Model);
        Debug.Log("loading model!");
        name = modelName + entityId;
        string iconPath = AssetUtility.GetPlayerHeadIconAsset(icon);
        GameEntry.Resource.LoadAsset(iconPath,LoadAssetSuccessCallback_Icon);
        
        
        //----------------------------------------------Init Skill
        var table = GameEntry.DataTable.GetDataTable<DRSkill>();
        foreach (var id in m_playerFsmData.Skills)
        {
            DRSkill drSkill = table.GetDataRow(id);
            Type type = Type.GetType(drSkill.SkillClass);
            IAction action = type.Assembly.CreateInstance(drSkill.SkillClass) as IAction;
            SkillList[drSkill.SkillName] = action;
        }
        
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        GameObject.Destroy(modelInstance);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        if (modelLoadEd && IconLoadEd)
        {
            // Debug.Log("load ed   Model and Icon");
        }
    }

    protected void OnDestroy()
    {

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

    public void AddBuffer(BufferState buff)
    {
        BufferList[buff.m_BufferName] = buff;
    }

    public void RemoveBuffer(string key)
    {
        BufferList.Remove(key);
    }

    public void LeaveFsm()
    {
        playFsm.CastChangeState<PlayerLeave>();
    }

    public void StopFsm()
    {
        GameEntry.Fsm.DestroyFsm<PlayerFSM>(FsmName);
    }

    private void LoadModelSuccess(string assetName, object asset, float duration, object userData)
    {
        var obj = asset as GameObject;
        modelInstance = Instantiate(obj,Model.transform);
        modelInstance.transform.localPosition = Vector3.zero;
        modelInstance.transform.rotation = quaternion.identity;
        modelInstance.transform.localScale = Vector3.one;
        Debug.Log("log success for model!!");
        modelLoadEd = true;
    }
    private void LoadIconSuccess(string assetName, object asset, float duration, object userData)
    {
        HeadIcon = asset as Texture2D;
        IconLoadEd = true;
    }

    public void ActiveSelectEffect()
    {
        
    }

    public void ResetSelectEffect()
    {
        
    }

    public void BufferLifeSubOne()
    {
        foreach (var buffer in BufferList)
        {
            buffer.Value.m_BufferLife -= 1;
            if (buffer.Value.m_BufferLife<=0)
            {
                // BufferList.Remove(buffer.Key);
                RemoveBuffer(buffer.Key);
            }
        }
    }


    public void UseDrug(ProductBase Pbase)
    {
        if (!Pbase.type.ToString().Contains("Drug"))
        {
            return;
        }

        Atk += Pbase.attack;
        Hp += Pbase.hp;
        Mag += Pbase.magic;
        Def += Pbase.defence;
        Luck += Pbase.lucky;
        


    }

}
