using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public class PatrolPath
{
    public string name = string.Empty;
    public List<Vector3> points = new List<Vector3>();
}

[ExecuteInEditMode]
public class PatrolPoints : MonoBehaviour
{
    public static PatrolPoints Instance;
    public List<PatrolPath> PatrolList = new List<PatrolPath>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        OnInit();
    }

    private void Update()
    {
        OnInit();
    }


    [Button("Refresh",ButtonSizes.Gigantic)]
    void OnInit()
    {
        PatrolList.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            PatrolPath patrol = new PatrolPath();
            patrol.name = transform.GetChild(i).name;
            int childC = transform.GetChild(i).childCount;
            for (int j = 0; j < childC; j++)
            {
                patrol.points.Add(transform.GetChild(i).GetChild(j).position);
            }
            PatrolList.Add(patrol);
        }
    }

    public PatrolPath GetPatrolPath(string name)
    {
        foreach (var list in PatrolList)
        {
            if (list.name == name)
            {
                return list;
            }
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        foreach (var list in PatrolList)
        {
            for (int i = 0; i < list.points.Count; i++)
            {
                Gizmos.color = Color.yellow;;
                Gizmos.DrawWireSphere(list.points[i],1);
                if (i+1<list.points.Count)
                {
                    Gizmos.color = Color.white;;
                    Gizmos.DrawLine(list.points[i],list.points[i+1]);
                }
            }
        }
    }
}
