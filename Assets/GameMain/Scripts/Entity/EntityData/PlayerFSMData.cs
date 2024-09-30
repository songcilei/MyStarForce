using System.Collections;
using System.Collections.Generic;
using GameFramework.Resource;
using StarForce;
using UnityEngine;

public class PlayerFSMData : EntityData
{

    public int EntityId;
    public string EntityName;
    public string ModelName;
    public string Icon;
    public PlayerType PlayerType = PlayerType.None;
    public float TimelineInitPos = 0;
    public float Radius = 0;

    public int[] Skills;
//-----------------------------------------------
    public float Spd=0;
    public int Atk = 0;
    public int Mag = 0;
    public int Def = 0;
    public int Mdf = 0;
    public int Hp = 0;
    public int Mp = 0;
    public int Level = 0;
    public int Luck = 0;
    
    
    public PlayerFSMData(int entityId, int typeId,PlayerType playerType) : base(entityId, typeId)
    {
        this.EntityId = entityId;

        if (playerType == PlayerType.Enemy)
        {
            DREntity drEntity = GameEntry.DataTable.GetDataTable<DREntity>().GetDataRow(typeId);
            // int modelId = drEntity.ModelID[0];
            // DRNpcModel drNpcModel = GameEntry.DataTable.GetDataTable<DRNpcModel>().GetDataRow(modelId);
        
            this.ModelName = drEntity.AssetName;
            this.EntityName = drEntity.AssetName;
            this.Icon = drEntity.Icon;
            this.PlayerType = playerType;
            this.TimelineInitPos = 0f;
            this.Radius = drEntity.Radius;
            //-------------------------------------
            this.Spd = drEntity.Spd;
            this.Atk = drEntity.Atk;
            this.Mag = drEntity.Mag;
            this.Def = drEntity.Def;
            this.Mdf = drEntity.Mdf;
            this.Hp = drEntity.Hp;
            this.Mp = drEntity.Mp;
            this.Luck = drEntity.Luck;
            this.Skills = drEntity.Skills;
        }

        if (playerType == PlayerType.Hero)
        {
            TeamBase teamBase = GameEntry.TeamComponent.GetPlayer(typeId);
            
            DREntity drEntity = GameEntry.DataTable.GetDataTable<DREntity>().GetDataRow(typeId);
            int modelId = drEntity.ModelID[0];
            DRNpcModel drNpcModel = GameEntry.DataTable.GetDataTable<DRNpcModel>().GetDataRow(modelId);
        
            this.ModelName = drNpcModel.AssetName;
            this.EntityName = drEntity.AssetName;
            this.Icon = drNpcModel.Icon;
            this.PlayerType = playerType;
            this.TimelineInitPos = teamBase.TimelineInitPos;
            this.Radius = drNpcModel.Radius;
            //-------------------------------------
            this.Spd = teamBase.Spd;
            this.Atk = teamBase.Atk;
            this.Mag = teamBase.Mag;
            this.Def = teamBase.Def;
            this.Mdf = teamBase.Mdf;
            this.Hp = teamBase.Hp;
            this.Mp = teamBase.Mp;
            this.Level = teamBase.Level;
            this.Luck = teamBase.Luck;
            this.Skills = teamBase.Skills;

        }


    }
}
