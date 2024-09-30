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
    public ActorType[] m_ActorTypes;
    private List<int> EntityIDList = new List<int>();
    private int loadEd = 0;
    private int teamCount = 0;
    
    public void SetEnemys(List<ActorType> actorTypes)
    {
        m_ActorTypes = actorTypes.ToArray();
    }
    private void OnTriggerEnter(Collider other)
    {
        EntityIDList.Clear();
        if (other.GetComponent<PlayerInputSystem>())
        {
            if (m_ActorTypes.Length == 0)
            {
                Debug.LogError("EntityCount is 0.");
            }

            foreach (var actor in m_ActorTypes)
            {
                EntityIDList.Add((int)actor);
            }
            GameEntry.BattleSystem.RunBattle(EntityIDList);
            //
            // List<TeamBase> teams = GameEntry.TeamComponent.GetTeam();
            // teamCount = teams.Count;
            // foreach (var team in teams)
            // {
            //     GameEntry.Entity.ShowPlayer(new PlayerFSMData(GameEntry.Entity.GenerateSerialId(),team.TypeId,PlayerType.Hero));
            // }
            //
            // foreach (var actor in m_ActorTypes)
            // {
            //     GameEntry.Entity.ShowPlayer(new PlayerFSMData(GameEntry.Entity.GetInstanceID(),(int)actor,PlayerType.Enemy));
            // }

            Debug.Log("Load Battle");
        }
    }
}
