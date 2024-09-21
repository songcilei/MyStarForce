using System;
using System.Collections;
using System.Collections.Generic;
using ECM2;
using GameFramework.Event;
using StarForce;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;

[RequireComponent(typeof(Collider))]
public class BattleTrigger : MonoBehaviour
{
    private ActorType[] m_ActorTypes;
    private List<PlayerFSM> EntityFsms = new List<PlayerFSM>();
    private int loadEd = 0;
    private int teamCount = 0;
    
    public void SetEnemys(List<ActorType> actorTypes)
    {
        m_ActorTypes = actorTypes.ToArray();
    }

    void Start()
    {
        GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId,LoadSuccess);    
    }

    // Update is called once per frame
    void Update()
    {
        if (loadEd>m_ActorTypes.Length+teamCount)
        {
            GameEntry.BattleSystem.RunBattle(EntityFsms);
            this.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EntityFsms.Clear();
        if (other.GetComponent<PlayerInputSystem>())
        {
            List<TeamBase> teams = GameEntry.TeamComponent.GetTeam();
            teamCount = teams.Count;
            foreach (var team in teams)
            {
                GameEntry.Entity.ShowPlayer(new PlayerFSMData(GameEntry.Entity.GenerateSerialId(),team.TypeId,PlayerType.Hero));
            }

            foreach (var actor in m_ActorTypes)
            {
                GameEntry.Entity.ShowPlayer(new PlayerFSMData(GameEntry.Entity.GetInstanceID(),(int)actor,PlayerType.Enemy));
            }

            Debug.Log("Load Battle");
        }
    }

    private void LoadSuccess(object obj,GameEventArgs args)
    {
        ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)args;
        if (ne.EntityLogicType == typeof(ShowEntitySuccessEventArgs))
        {
            EntityFsms.Add((PlayerFSM)ne.Entity.Logic);
        }

        loadEd++;
    }

}
