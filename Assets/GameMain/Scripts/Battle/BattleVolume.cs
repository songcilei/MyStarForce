using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Entity;
using GameFramework.Event;
using Sirenix.OdinInspector;
using StarForce;
using UnityEditor;
using UnityEngine;
using ShowEntitySuccessEventArgs = UnityGameFramework.Runtime.ShowEntitySuccessEventArgs;

public class BattleVolume : MonoBehaviour
{
    public List<ActorType> ActorTypes = new List<ActorType>();
    List<PlayerFSM> EntityFsms = new List<PlayerFSM>();
    private int loadEd = 0;
    private void Start()
    {
        GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId,LoadSuccess);
    }

    private void Update()
    {
        Debug.Log(loadEd);
        //临时写的+3 代表主角3个英雄
        if (loadEd>= ActorTypes.Count+3)
        {
            // List<PlayerFSM> playerFsms = new List<PlayerFSM>();
            
            GameEntry.BattleSystem.RunBattle(EntityFsms);
            Debug.Log("run Battle Component!");
            this.gameObject.SetActive(false);
        }
    }


    private void OnDestroy()
    {

    }
    
    
    [Button]
    public void loadEntity()
    {

        loadEd = 0;
        EntityFsms.Clear();
        this.gameObject.SetActive(true);
        List<TeamBase> teams = GameEntry.TeamComponent.GetTeam();
        var table = GameEntry.DataTable.GetDataTable<DREntity>();
        foreach (var team in teams)
        {
            GameEntry.Entity.ShowPlayer(new PlayerFSMData(GameEntry.Entity.GenerateSerialId(),team.TypeId,PlayerType.Hero));
        }
        
        // GameEntry.Entity.ShowPlayer(new PlayerFSMData(GameEntry.Entity.GenerateSerialId(),80001,PlayerType.Hero));    
        // GameEntry.Entity.ShowPlayer(new PlayerFSMData(GameEntry.Entity.GenerateSerialId(),80002,PlayerType.Hero)); 
        // GameEntry.Entity.ShowPlayer(new PlayerFSMData(GameEntry.Entity.GenerateSerialId(),80003,PlayerType.Hero));

        foreach (var actor in ActorTypes)
        {
            GameEntry.Entity.ShowPlayer(new PlayerFSMData(GameEntry.Entity.GenerateSerialId(),(int)actor,PlayerType.Enemy));    
        }

        Debug.Log("loadEntity");
        

    }

    [Button]
    public void AddRandomPlayerToTeam()
    {
        GameEntry.Event.Fire(this,AddRandomTeamListArgs.Create());
    }
    [Button]
    public void RemoveTeamList()
    {
        GameEntry.Event.Fire(this,ClearTeamListArgs.Create());
    }

    [Button]
    public void NextPalyerFSM()
    {
        this.gameObject.SetActive(true);
        loadEd = 0;
        GameEntry.BattleSystem._battleType = BattleType.OnLeave;
    }
    
    [Button]
    public void logEntityInfo()
    {
        int entityCount = GameEntry.Entity.EntityCount;
        Debug.Log("entityCount:"+entityCount);

        UnityGameFramework.Runtime.Entity[] entities = GameEntry.Entity.GetAllLoadedEntities();
        for (int i = 0; i < entities.Length; i++)
        {
            int id =entities[i].Id;
            Debug.LogError("loaded id :"+id);
        }
    }

    [Button]
    public void nextPlayerState()
    {
        GameEntry.BattleSystem.nextPlayerFsm();
    }


    private void LoadSuccess(object sender,GameEventArgs e)
    {
        Debug.Log("success event load");
        ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
        if (ne.EntityLogicType == typeof(PlayerFSM))
        {
            EntityFsms.Add((PlayerFSM)ne.Entity.Logic);
        }

        loadEd++;
    }
}
