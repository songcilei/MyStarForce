using System.Collections;
using System.Collections.Generic;
using StarForce;
using UnityEngine;
using UnityGameFramework.Runtime;

public class myCube : EntityLogic
{
    public new string name = null;
    private myCubeData _myCubeData = null;
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        _myCubeData = userData as myCubeData;
        
        name = _myCubeData.GetName();


    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
    }

    protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
    {
        base.OnAttached(childEntity, parentTransform, userData);
    }
}
