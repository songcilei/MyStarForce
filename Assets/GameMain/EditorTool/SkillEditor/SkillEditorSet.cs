using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/SkillEditor/SkillEditorSet",fileName = "SkillEditorSet")]
public class SkillEditorSet : SerializedScriptableObject
{
    
    public string PrefabPath = "";
    public List<string> PrefabName = new List<string>();
    public int CurrentPrefabIndex;
    public string AnimationPath = "";
    public List<string> AnimationName = new List<string>();
    public int CurrentAnimationIndex;

}
