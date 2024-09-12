using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillMainPanel))]
public class SkillMainPanelEditor : Editor
{

    private int index=0;
    private SkillMainPanel m_SkillMainPanel;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        m_SkillMainPanel = target as SkillMainPanel;

 //-------------------------------------Title
        GUILayout.BeginHorizontal(GUI.skin.box);

        GUILayout.Label("SkillName:");
        if (GUILayout.Button("刷新"))
        {
            m_SkillMainPanel.Init();
        }

        GUILayout.Button("保存");

        GUILayout.Button("打开");

        GUILayout.EndHorizontal();
        
        
 //-------------------------------------- Main Panel
        GUILayout.BeginVertical(GUI.skin.box);
        //Check change
        EditorGUI.BeginChangeCheck();
        
        m_SkillMainPanel.CurrentSkillIndex = EditorGUILayout.Popup(m_SkillMainPanel.CurrentSkillIndex,m_SkillMainPanel.GetSkillNameList());


        if (EditorGUI.EndChangeCheck())
        {
            
        }
        //Check change End
        GUILayout.EndVertical();
        
//-------------------------------------- Main Panel

    }
}
