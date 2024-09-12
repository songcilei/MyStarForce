using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/SkillEditor/SkillEditorTableData",fileName = "SkillEditorTableData")]
public class SkillEditorTableData : SerializedScriptableObject
{
    [DictionaryDrawerSettings]
    public Dictionary<string, SkillEditorTableBase> m_SkillEditorTableBases =
        new Dictionary<string, SkillEditorTableBase>();
}
