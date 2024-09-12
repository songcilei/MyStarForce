using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Slate;
using Slate.ActionClips;
using UnityEngine;

public class testSlate : MonoBehaviour
{
    public AnimationClip clip;
    public GameObject AddGroup;
    
    private Cutscene m_Cutscene;
    [Button]
    void TestSlatea()
    {
        m_Cutscene = this.GetComponent<Cutscene>();
        var list = m_Cutscene.groups;
        foreach (var obj in list)
        {
            Debug.Log("groupName:"+obj.name);
        }
        
        var group = m_Cutscene.AddGroup(typeof(ActorGroup),AddGroup);

        ActorActionTrack track = group.AddTrack<ActorActionTrack>();
        track.AddAction<PlayAnimationClip>(0);
        group.AddTrack<AnimationTrack>();
    }
    
    [Button]
    void PlayCutScene()
    {
        m_Cutscene = this.GetComponent<Cutscene>();
        m_Cutscene.Play();
    }

}


