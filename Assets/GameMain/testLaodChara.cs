using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Slate;
using UnityEngine;
using UnityEngine.Events;

public class testLaodChara : MonoBehaviour
{
    public GameObject chara;
    private Cutscene m_cutScene;
    private UnityAction Action;
    void Start()
    {
        m_cutScene = this.GetComponent<Cutscene>();
        m_cutScene.length = 10;//时间轴总长度
        m_cutScene.Stop();
    }

    [Button]
    void setTest()
    {
        m_cutScene.groups[1].actor = chara;
        m_cutScene.Play(() =>
        {
            // m_cutScene.Stop();
            Debug.Log("play  end!!!!");
        });
    }

}
