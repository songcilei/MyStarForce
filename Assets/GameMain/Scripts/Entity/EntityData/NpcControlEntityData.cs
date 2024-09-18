using System.Collections;
using System.Collections.Generic;
using StarForce;
using UnityEngine;

public class NpcControlEntityData : EntityData
{
    public int EntityId;
    public string ModelName;
    public string patrolPathName;
    public NpcControlEntityData(int entityId, int typeId,string patrolPathName = null) : base(entityId, typeId)
    {
        EntityId = entityId;

        var dtEntity = GameEntry.DataTable.GetDataTable<DREntity>();
        var drTable = dtEntity.GetDataRow(typeId);
        var dtNpcModel = GameEntry.DataTable.GetDataTable<DRNpcModel>();
        var drNpc = dtNpcModel.GetDataRow(drTable.ModelID[0]);
        ModelName = drNpc.AssetName;

        if (patrolPathName==null)
        {
            this.patrolPathName = null;
        }
        else
        {
            this.patrolPathName = patrolPathName;
        }

    }
}
