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
    public int typeId;
    public string patrolPathName;

    public List<ActorType> m_ActorTypes = new List<ActorType>();

    void Start()
    {

        GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId,SuccessLoad);
        StartCoroutine(PlayRun());
    }


    IEnumerator PlayRun()
    {

        yield return new WaitForSeconds(1);
        
        GameEntry.Entity.ShowNpcControlEntity(new NpcControlEntityData(GameEntry.Entity.GenerateSerialId(),typeId,patrolPathName));
        
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
            entity.transform.position = this.transform.position;
            var trigger = entity.gameObject.AddComponent<BattleTrigger>();
            trigger.SetEnemys(m_ActorTypes);
        }
    }
}
