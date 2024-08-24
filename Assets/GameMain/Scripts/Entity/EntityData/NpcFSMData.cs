using System.Collections;
using System.Collections.Generic;
using StarForce;
using UnityEngine;

public class NpcFSMData : EntityData
{
    public Vector3 InitPosition;
    public NpcFSMData(int entityId, int typeId,BirthPoint[] birthPoints) : base(entityId, typeId)
    {
        int index = Mathf.FloorToInt(Random.Range(0, birthPoints.Length));
        InitPosition = birthPoints[index].transform.position;

    }
}
