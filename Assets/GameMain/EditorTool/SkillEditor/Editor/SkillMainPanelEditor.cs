using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

[CustomEditor(typeof(SkillMainPanel))]
public class SkillMainPanelEditor : Editor
{

    private SkillMainPanel m_SkillMainPanel;
    private string SkillName = "";
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        m_SkillMainPanel = target as SkillMainPanel;

        if (m_SkillMainPanel.GetSkillNameList().Length == 0)
        {
            m_SkillMainPanel.Init();
        }
        //-------------------------------------Title
        GUILayout.BeginHorizontal(GUI.skin.box);

        GUILayout.Label("SkillName:");
        if (GUILayout.Button("刷新"))
        {
            m_SkillMainPanel.Init();
        }



        if (GUILayout.Button("导入"))
        {
            ImportTableBase();
        }

        if (GUILayout.Button("删除"))
        {
            bool result = EditorUtility.DisplayDialog(
                "请确认是否删除",
                "确认删除？",
                "Yes",
                "Cancel"
            );
            if (result)
            {
                DeleteTableBase();
            }
        }

        GUILayout.EndHorizontal();
        if (m_SkillMainPanel.GetSkillNameList().Length>0)
        {
            m_SkillMainPanel.CurrentSkillIndex = EditorGUILayout.Popup(m_SkillMainPanel.CurrentSkillIndex,m_SkillMainPanel.GetSkillNameList());
        }

       
        
 //-------------------------------------- Main Panel
        GUILayout.Space(100);
 
        GUILayout.BeginVertical(GUI.skin.box);
        //Check change
        EditorGUI.BeginChangeCheck();


        GUILayout.Label("SkillName");
        SkillName = GUILayout.TextArea(SkillName);
        
        
        GUILayout.Label("角色 Prefab");
        if (m_SkillMainPanel.GetEditorSet().PrefabName.Count>0)
        {
            m_SkillMainPanel.GetEditorSet().CurrentPrefabIndex = EditorGUILayout.Popup(
                m_SkillMainPanel.GetEditorSet().CurrentPrefabIndex, m_SkillMainPanel.GetEditorSet().PrefabName.ToArray());
        }

        GUILayout.Label("角色 动作");
        if (m_SkillMainPanel.GetEditorSet().AnimationName.Count>0)
        {
            m_SkillMainPanel.GetEditorSet().CurrentAnimationIndex = EditorGUILayout.Popup(
                m_SkillMainPanel.GetEditorSet().CurrentAnimationIndex, m_SkillMainPanel.GetEditorSet().AnimationName.ToArray());
        }
        
        if (EditorGUI.EndChangeCheck())
        {
            m_SkillMainPanel.InitAnimation();
        }
        //Check change End
        GUILayout.EndVertical();

        if (GUILayout.Button("保存"))
        {
            SaveTableBase();
        }
        
//-------------------------------------- Debugger
        if (GUILayout.Button("InitCutscene"))
        {
            m_SkillMainPanel.Debugger();
        }
    }


    private void ImportTableBase()
    {
        int index = m_SkillMainPanel.CurrentSkillIndex;
        string key = m_SkillMainPanel.GetSkillNameList()[index];

        var table = m_SkillMainPanel.GetEditorTableData().m_SkillEditorTableBases[key];

        m_SkillMainPanel.GetEditorSet().CurrentPrefabIndex = GetPrefabIndex(table.Prefabname);
        m_SkillMainPanel.GetEditorSet().CurrentAnimationIndex = GetAnimaIndex(table.AnimaName);
        
        
    }

    //save
    private void SaveTableBase()
    {
        SkillEditorTableBase tableBase = new SkillEditorTableBase();
        
        //--------------------------------------------------main
        tableBase.SkillName = SkillName;
        tableBase.Prefabname = m_SkillMainPanel.GetEditorSet()
            .PrefabName[m_SkillMainPanel.GetEditorSet().CurrentPrefabIndex];
        tableBase.AnimaName = m_SkillMainPanel.GetEditorSet()
            .AnimationName[m_SkillMainPanel.GetEditorSet().CurrentAnimationIndex];
        m_SkillMainPanel.GetEditorTableData().m_SkillEditorTableBases[SkillName] = tableBase;
        
        //add save 
        
        //--------------------------------------------------main
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(m_SkillMainPanel.GetEditorTableData());
        
        //刷新状态
        m_SkillMainPanel.Init();
        
        //自动跳转到菜单
        for (int i = 0; i < m_SkillMainPanel.GetSkillNameList().Length; i++)
        {
            if (SkillName == m_SkillMainPanel.GetSkillNameList()[i])
            {
                m_SkillMainPanel.CurrentSkillIndex = i;
            }
        }
    }

    private void DeleteTableBase()
    {
        Debug.LogError("确认删除！！！");
        int index = m_SkillMainPanel.CurrentSkillIndex;
        string key =GetSkillKey(index);
        m_SkillMainPanel.GetEditorTableData().m_SkillEditorTableBases.Remove(key);
        m_SkillMainPanel.Init();
    }


    string GetSkillKey(int index)
    {
        string key =m_SkillMainPanel.GetSkillNameList()[index];
        return key;
    }

    int GetPrefabIndex(string name)
    {
        string[] names = m_SkillMainPanel.GetEditorSet().PrefabName.ToArray();
        for (int i = 0; i < names.Length; i++)
        {
            if (name == names[i])
            {
                return i;
            }
        }

        return -1;
    }

    int GetAnimaIndex(string name)
    {
        string[] names = m_SkillMainPanel.GetEditorSet().AnimationName.ToArray();
        for (int i = 0; i < names.Length; i++)
        {
            if (name == names[i])
            {
                return i;
            }
        }
        return -1;
    }


}
