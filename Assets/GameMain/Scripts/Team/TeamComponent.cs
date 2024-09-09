using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using StarForce;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;
using Random = UnityEngine.Random;

public class TeamComponent : GameFrameworkComponent
{


    public TeamScriptable teamScriptable;
    protected override void Awake()
    {
        base.Awake();
        
    }

    private void Start()
    {
        GameEntry.Event.Subscribe(AddRandomTeamListArgs.EventId,AddRandomPlayerToTeamList);
        GameEntry.Event.Subscribe(ClearTeamListArgs.EventId,ClearTeamList);
    }

    private void OnDisable()
    {
        GameEntry.Event.Unsubscribe(AddRandomTeamListArgs.EventId,AddRandomPlayerToTeamList);
        GameEntry.Event.Unsubscribe(ClearTeamListArgs.EventId,ClearTeamList);
    }


    public List<TeamBase> GetTeam()
    {
        List<TeamBase> teamBases = new List<TeamBase>();
        foreach (var team in teamScriptable.Team)
        {
            if (team.TypeId!=0)
            {
                teamBases.Add(team);
            }
        }

        return teamBases;
    }

    public TeamBase GetPlayer(int typeId)
    {
        foreach (var team in teamScriptable.TeamList)
        {
            if (team.TypeId == typeId)
            {
                return team;
            }
        }

        return null;
    }


    public void AddRandomPlayerToTeamList(object obj,GameEventArgs args)
    {
        Debug.Log("Add Random Player To Team List");
        int length= Enum.GetValues(typeof(PlayerId)).Length;
        int i = Random.Range(0, length);
        PlayerId playerId = (PlayerId)Enum.GetValues(typeof(PlayerId)).GetValue(i);
        Debug.Log(playerId);
        CreateToTeamList((int)playerId);
    }

    public void ClearTeamList(object obj,GameEventArgs args)
    {
        Debug.Log("clear team list!!");
        teamScriptable.TeamList.Clear();
        for (int i = 0; i < teamScriptable.Team.Count; i++)
        {
            teamScriptable.Team[i].TypeId = 0;
        }
    }


    public void CreateToTeamList(int typeId)
    {
//check teamList has        
        foreach (var teambase in teamScriptable.TeamList)
        {
            if (teambase.TypeId == typeId)
            {
                return;
            }
        }
//create
        var table = GameEntry.DataTable.GetDataTable<DREntity>();
        DREntity drEntity = table.GetDataRow(typeId);
        TeamBase teamBase = new TeamBase();
        teamBase.TypeId = typeId;
        teamBase.PlayerId = (PlayerId)typeId;
        
        teamBase.Level = drEntity.Level;
        teamBase.Grow = drEntity.Grow;
        teamBase.Skills = drEntity.Skills;
        teamBase.TimelineInitPos = 0.8f;
        //--------------------------------
        teamBase.Spd = drEntity.Spd;
        teamBase.Atk = drEntity.Atk;
        teamBase.Mag = drEntity.Mag;
        teamBase.Def = drEntity.Def;
        teamBase.Mdf = drEntity.Mdf;
        teamBase.Hp = drEntity.Hp;
        teamBase.Mp = drEntity.Mp;
        teamBase.Luck = drEntity.Luck;
        teamScriptable.TeamList.Add(teamBase);
//add team
        int TeamCount = teamScriptable.Team.Count;
        for (int i = 0; i < TeamCount; i++)
        {
            if (teamScriptable.Team[i].TypeId==0)
            {
                teamScriptable.Team[i] = teamBase;
                break;
            }
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(teamScriptable);
        AssetDatabase.SaveAssets();
#endif        
    }

}
