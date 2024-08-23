using System.Collections;
using System.Collections.Generic;
using StarForce;
using UnityEngine;

public class myCubeData : EntityData
{
    
    [SerializeField]
    string m_name = null;
    
    public myCubeData(int entityId, int typeId) : base(entityId, typeId)
    {
        m_name = "22222222222";
    }

    public string GetName()
    {
        return m_name;
    }

}
