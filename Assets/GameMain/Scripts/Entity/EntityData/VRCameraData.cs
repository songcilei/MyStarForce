using System.Collections;
using System.Collections.Generic;
using StarForce;
using UnityEngine;

public class VRCameraData : EntityData
{
    private float attack;
    public VRCameraData(int entityId, int typeId) : base(entityId, typeId)
    {
        var dtEntity =GameEntry.DataTable.GetDataTable<DREntity>();
        var attack = dtEntity.GetDataRow(typeId);
        // this.attack = attack.;
    }
}
