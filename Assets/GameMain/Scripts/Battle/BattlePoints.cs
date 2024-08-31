using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[Serializable]
public class BattlePointValue
{
    public Transform trans;
    [Button]
    public void CreateBattle()
    {
        Debug.Log("create battle scene!");
    }
}


public class BattlePoints : MonoBehaviour
{

    public List<BattlePointValue> BattlePointsList = new List<BattlePointValue>();

    [Button(ButtonSizes.Large)]
    // [InlineButton("printIndex")]
    public void SetInit()
    {
        BattlePointsList.Clear();
        int count = this.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            BattlePointValue _BattlePointValue = new BattlePointValue();
            Transform tran = this.transform.GetChild(i);
            _BattlePointValue.trans = tran;
            BattlePointsList.Add(_BattlePointValue);
        }
    }
    
    void Awake()
    {
        SetInit();
    }

    public BattlePointValue GetNearBattlePoint(Vector3 point)
    {
        float near = float.PositiveInfinity;
        int index = 0;
        int count = BattlePointsList.Count;
        for (int i = 0; i < count; i++)
        {
            float distance = Vector3.Distance(BattlePointsList[i].trans.position, point); 
            if (distance < near)
            {
                near = distance;
                index = i;
            }
        }

        return BattlePointsList[index];
    }


}
