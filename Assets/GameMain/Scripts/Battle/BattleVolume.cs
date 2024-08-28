using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Entity;
using GameFramework.Event;
using Sirenix.OdinInspector;
using StarForce;
using UnityEngine;
using ShowEntitySuccessEventArgs = UnityGameFramework.Runtime.ShowEntitySuccessEventArgs;

public class BattleVolume : MonoBehaviour
{
    public List<ActorType> ActorTypes = new List<ActorType>();
    List<UnityGameFramework.Runtime.Entity> EntityFsms = new List<UnityGameFramework.Runtime.Entity>();
    private int loadEd = 0;
    private void Start()
    {
        GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId,LoadSuccess);
    }

    private void Update()
    {
        //临时写的+3 代表主角3个英雄
        if (loadEd>= ActorTypes.Count+3)
        {
            List<PlayerFSM> playerFsms = new List<PlayerFSM>();
            
            GameEntry.BattleSystem.RunBattle(EntityFsms);

            this.gameObject.SetActive(false);
            Debug.Log("run Battle Component!");
        }
    }


    private void OnDisable()
    {
        GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId,LoadSuccess);
    }
    
    
    [Button]
    private void loadEntity()
    {
        GameEntry.Entity.ShowPlayer(new PlayerFSMData(GameEntry.Entity.GenerateSerialId(),80001,PlayerType.Hero));    
        GameEntry.Entity.ShowPlayer(new PlayerFSMData(GameEntry.Entity.GenerateSerialId(),80002,PlayerType.Hero)); 
        GameEntry.Entity.ShowPlayer(new PlayerFSMData(GameEntry.Entity.GenerateSerialId(),80003,PlayerType.Hero));

        foreach (var actor in ActorTypes)
        {
            GameEntry.Entity.ShowPlayer(new PlayerFSMData(GameEntry.Entity.GenerateSerialId(),(int)actor,PlayerType.Enemy));    
        }
    }

    private void LoadSuccess(object sender,GameEventArgs e)
    {
        ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
        EntityFsms.Add(ne.Entity);
        loadEd++;
    }
}
