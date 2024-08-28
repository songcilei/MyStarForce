using System.Collections;
using System.Collections.Generic;
using GameFramework.Resource;
using StarForce;
using UnityEngine;

public class PlayerFSMData : EntityData
{

    public int EntityId;
    public string ModelName;
    public float TimeSpeed=0;
    public string Icon;
    public PlayerType PlayerType = PlayerType.None;
    public float TimelineInitPos = 0;
    public PlayerFSMData(int entityId, int typeId,PlayerType playerType) : base(entityId, typeId)
    {
        this.EntityId = entityId;

        DREntity drEntity = GameEntry.DataTable.GetDataTable<DREntity>().GetDataRow(typeId);
        int modelId = drEntity.ModelID[0];
        DRNpcModel drNpcModel = GameEntry.DataTable.GetDataTable<DRNpcModel>().GetDataRow(modelId);
        
        this.ModelName = drNpcModel.AssetName;
        this.TimeSpeed =drEntity.TimeSpeed;
        
        this.Icon = drNpcModel.Icon;
        
        this.PlayerType = playerType;
        TimelineInitPos = 0;
    }
}
