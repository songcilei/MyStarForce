using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;

public class PlayerFSM : PlayerBase
{
    public PlayerFSMData PlayerFsmData = null;
    
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);

        PlayerFSMData playerFsmData = userData as PlayerFSMData;

    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
    }
}
