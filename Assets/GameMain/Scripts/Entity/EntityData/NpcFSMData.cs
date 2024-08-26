using System.Collections;
using System.Collections.Generic;
using StarForce;
using UnityEngine;

public class NpcFSMData : EntityData
{
    public Vector3 InitPosition;
    public ProductBase NeedProduct;
    public int EntityId;
    public float FSMDistance;
    public NpcFSMData(int entityId, int typeId,BirthPoint[] birthPoints) : base(entityId, typeId)
    {
        int index = Mathf.FloorToInt(Random.Range(0, birthPoints.Length));
        InitPosition = birthPoints[index].transform.position;
        NeedProduct = GameEntry.Shop.GetRandomProduct();
        EntityId = entityId;


        var drEntityTable =GameEntry.DataTable.GetDataTable<DREntity>();
        DREntity drEntity = drEntityTable.GetDataRow(typeId);
        FSMDistance = drEntity.FSMDistance;
        //Debug.Log("array:"+drEntity.TestArray);
    }
    
    
    
    
}
