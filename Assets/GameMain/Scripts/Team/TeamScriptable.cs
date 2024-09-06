using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/CreateTeamScriptable",fileName = "TeamAsset")]
public class TeamScriptable : ScriptableObject
{
    public List<TeamBase> Team = new List<TeamBase>();
    public List<TeamBase> TeamList = new List<TeamBase>();

}
