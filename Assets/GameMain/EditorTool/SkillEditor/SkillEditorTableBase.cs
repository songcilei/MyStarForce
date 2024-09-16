using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[Serializable]
public class SkillEditorTableBase
{
    public string SkillName = string.Empty;
    
    
    public string Prefabname = string.Empty;
    public string AnimaName = string.Empty;


    public string ActorClipName = string.Empty;

    public List<SkillEditorEffect> Effect = new List<SkillEditorEffect>();
}


public class SkillEditorEffect
{
    public string TrackName = string.Empty;
    public string EffectPath = string.Empty;
    public bool HasClip = true;
    public float StartTime = 0;
    public float EndTime = 1;
    public float Length = 1;
    public float BlendIn = 0;
    public float BlendOut = 0;
}