using System.Collections;
using System.Collections.Generic;
using Slate;
using Slate.ActionClips;
using StarForce;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

[CustomEditor(typeof(SkillMainPanel))]
public class SkillMainPanelEditor : Editor
{

    private SkillMainPanel m_SkillMainPanel;

    public string SkillName
    {
        get
        {
            return m_SkillMainPanel.SkillName;
        }
        set
        {
            m_SkillMainPanel.SkillName = value;
        }
    }
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
            m_SkillMainPanel.Init();
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
        //--------------------------------------------------main
        int index = m_SkillMainPanel.CurrentSkillIndex;
        string key = m_SkillMainPanel.GetSkillNameList()[index];
        SkillName = key;
        var table = m_SkillMainPanel.GetEditorTableData().m_SkillEditorTableBases[key];
        if (table==null)
        {
            Debug.Log("table is null!!!!!");
        }
        
        m_SkillMainPanel.GetEditorSet().CurrentPrefabIndex = GetPrefabIndex(table.Prefabname);
        m_SkillMainPanel.GetEditorSet().CurrentAnimationIndex = GetAnimaIndex(table.AnimaName);
        //add actorGroup import ---------------------------------------------

        var actorGroup = m_SkillMainPanel.GetActorGroup("actorGroup",true,true);
      
        //prefab clip
        //var actorTrack = m_SkillMainPanel.GetActorTrack(actorGroup, "actorTrack");
        string prefabPath = string.Format("{0}{1}{2}",m_SkillMainPanel.GetEditorSet().PrefabPath,table.Prefabname,".prefab");
        actorGroup.actor = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        //animation clip
        var Animtrack = m_SkillMainPanel.GetActorTrack(actorGroup,"animaTrack");
        
        AnimationClip clip = m_SkillMainPanel.GetCurrentAnimationClip(table);
        var actorClip = Animtrack.AddAction<PlayAnimatorClip>(0);
        actorClip.animationClip = clip;

        
        //add EffectGroup import-----------------------------------------------
        var effectList = table.Effect;
        for (int i = 0; i < effectList.Count; i++)
        {
            string groupName = effectList[i].TrackName;
            CutsceneGroup group = m_SkillMainPanel.GetActorGroup(groupName,true,false);
            var obj = AssetDatabase.LoadAssetAtPath<GameObject>(effectList[i].EffectPath);
            if (obj != null && group!=null)
            {
                group.actor = obj;
                if (effectList[i].HasClip)
                {
                    var particle = group.tracks[0].AddAction<m_SampleParticleSystem>(effectList[i].StartTime);
                    particle.particles = obj.GetComponent<ParticleSystem>();

                    group.tracks[0].clips[0].startTime = effectList[i].StartTime;
                    group.tracks[0].clips[0].endTime = effectList[i].EndTime;
                    group.tracks[0].clips[0].length = effectList[i].Length;
                    group.tracks[0].clips[0].blendIn = effectList[i].BlendIn;
                    group.tracks[0].clips[0].blendOut = effectList[i].BlendOut;
                }
            }
        }

        Selection.activeObject = m_SkillMainPanel.gameObject;
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
        
        //add effect
        CutsceneGroup[] groups = m_SkillMainPanel.GetEffectGroup("Effect");
        for (int i = 0; i < groups.Length; i++)
        {
            string path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(groups[i].actor);
            SkillEditorEffect effect = new SkillEditorEffect();
            effect.TrackName = groups[i].name;
            effect.EffectPath = path;
            if (groups[i].tracks[0].clips.Count>0)
            {
                effect.HasClip = true;
                effect.StartTime = groups[i].tracks[0].clips[0].startTime;
                effect.EndTime = groups[i].tracks[0].clips[0].endTime;
                effect.Length = groups[i].tracks[0].clips[0].length;
                effect.BlendIn = groups[i].tracks[0].clips[0].blendIn;
                effect.BlendOut = groups[i].tracks[0].clips[0].blendOut;
            }
            else
            {
                effect.HasClip = false;
            }

            tableBase.Effect.Add(effect);

        }
        
        //add save ---------------------------------------------
        
        
        m_SkillMainPanel.GetEditorTableData().m_SkillEditorTableBases[SkillName] = tableBase;

        
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
        
        //重新导入
        ImportTableBase();
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
