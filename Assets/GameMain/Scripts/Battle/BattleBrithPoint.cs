using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameFramework.Event;
using StarForce;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;

public class BattleBrithPoint : MonoBehaviour
{
    
    void Start()
    {
        
        
        GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId,SuccessLoad);
        StartCoroutine(PlayRun());
        
    }


    IEnumerator PlayRun()
    {

        yield return new WaitForSeconds(1);
        
        GameEntry.Entity.ShowNpcControlEntity(new NpcControlEntityData(GameEntry.Entity.GenerateSerialId(),80007,"path1"));
        
    }

    void Update()
    {
    }


    private void SuccessLoad(object obj,GameEventArgs args)
    {
        ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)args;
        if (ne.EntityLogicType == typeof(NpcControlEntity))
        {
            var entity = (NpcControlEntity)ne.Entity.Logic;
            entity.transform.position = Vector3.zero;
        }
    }
}
