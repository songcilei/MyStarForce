using System.Collections;
using System.Collections.Generic;
using StarForce;
using UnityEngine;

public class PlayerControlEntityData : EntityData
{
    public int EntityId;
    public string ModelName;
    public string Icon;
    public PlayerControlEntityData(int entityId, int typeId) : base(entityId, typeId)
    {
        this.EntityId = entityId;
        DREntity drEntity = GameEntry.DataTable.GetDataTable<DREntity>().GetDataRow(typeId);
        int modelId = drEntity.ModelID[0];
        DRNpcModel rdNpc = GameEntry.DataTable.GetDataTable<DRNpcModel>().GetDataRow(modelId);
        ModelName = rdNpc.AssetName;
        Icon = rdNpc.Icon;
    }
}
