using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/SkillEditor/SkillEditorSet",fileName = "SkillEditorSet")]
public class SkillEditorSet : SerializedScriptableObject
{
    
    public string ModelPath = "";
    public List<string> ModelName = new List<string>();
    public string AnimationPath = "";
    public List<string> AnimationName = new List<string>();

    
}
