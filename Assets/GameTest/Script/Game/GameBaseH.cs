using System.Collections;
using System.Collections.Generic;
using GameFramework.Entity;
using GameFramework.Event;
using StarForce;
using UnityEngine;
using UnityEngine.PlayerLoop;

using ShowEntitySuccessEventArgs = UnityGameFramework.Runtime.ShowEntitySuccessEventArgs;
using ShowEntityFailureEventArgs = UnityGameFramework.Runtime.ShowEntityFailureEventArgs;


public abstract class GameBaseH
{
    public abstract GameModeH GameMode
    {
        get;
    }

    private myCube _myCube = null;
    private ShopAgent[] shopAgents;
    public BirthPoint[] birthPoints;
    public virtual void Initialize()
    {
        GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId,OnShowEntitySuccess);
        GameEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId,OnShowEntityFailure);

                
        InitSceneLogic();
        
        
        myCubeData _myCubeData = new myCubeData(GameEntry.Entity.GenerateSerialId(),70004);
        GameEntry.Entity.ShowMyCube(_myCubeData);
        
        
    }


    public virtual void Shutdown()
    {
        GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        GameEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId,OnShowEntityFailure);
    }

    public virtual void Update(float elapseSeconds,float realElapseSeconds)
    {
        
    }

    public virtual void OnShowEntitySuccess(object sender,GameEventArgs e)
    {
        ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
        if (ne.EntityLogicType == typeof(myCube))
        {
            _myCube = (myCube)ne.Entity.Logic;
            ;
        }
    }

    public virtual void OnShowEntityFailure(object sender, GameEventArgs e)
    {
        ShowEntityFailureEventArgs ne = (ShowEntityFailureEventArgs)e;
        if (ne.EntityLogicType == typeof(myCube))
        {
            Debug.LogError("dont load entity!!");
        }
    }


    void InitSceneLogic()
    {
        shopAgents = Object.FindObjectsOfType<ShopAgent>();
        
        GameEntry.Shop.SetAgents(shopAgents);
        GameEntry.Shop.DispenseShopAgent(shopAgents);//fen fa 

        birthPoints = Object.FindObjectsOfType<BirthPoint>();
    }
    


}
