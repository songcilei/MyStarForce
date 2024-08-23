using System.Collections;
using System.Collections.Generic;
using StarForce;
using UnityEngine;

public class mSphereData : EntityData
{
    [SerializeField]
    private string name = null;
    public mSphereData(int entityId, int typeId) : base(entityId, typeId)
    {
        name = "my is  a  Spherer!!";
        Debug.Log(name);
        
    }

    public string GetName()
    {
        return name;
    }
}
