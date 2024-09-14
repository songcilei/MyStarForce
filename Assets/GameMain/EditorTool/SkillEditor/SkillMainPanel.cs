using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using Slate;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;
using Path = System.IO.Path;

public class SkillMainPanel : MonoBehaviour
{
    private SkillEditorSet m_SkillEditorSet;
    private SkillEditorTableData m_SkillEditorTableData;
    private List<string> m_SkillNameList = new List<string>();
    public int CurrentSkillIndex=0;

    private Cutscene m_Cutscene;
    public string SkillName;
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string[] GetSkillNameList()
    {
        return m_SkillNameList.ToArray();
    }

    public SkillEditorSet GetEditorSet()
    {
        return m_SkillEditorSet;
    }

    public Cutscene GetCutScene()
    {
        return m_Cutscene;
    }

    public CutsceneGroup GetActorGroup(string name)
    {
        foreach (var group in m_Cutscene.groups)
        {
            if (group.name == name)
            {
                return group;
            }
        }

        var actorGroup = m_Cutscene.AddGroup<ActorGroup>();
        actorGroup.name = name;
        var actionTrack = actorGroup.AddTrack<ActorActionTrack>();
        actionTrack.name = "actorTrack";
        var animaTrack = actorGroup.AddTrack<AnimatorTrack>();
        animaTrack.name = "animaTrack";
        
        return actorGroup;
    }

    public CutsceneTrack GetActorTrack(CutsceneGroup group,string name)
    {
        foreach (var track in group.tracks)
        {
            if (track.name == name)
            {
                return track;
            }
        }
        return null;
    }


    public AnimationClip GetCurrentAnimationClip(SkillEditorTableBase tableBase)
    {
        string clipPath = string.Format("{0}{1}/{2}{3}",GetEditorSet().AnimationPath,tableBase.Prefabname,tableBase.AnimaName,".anim");
        AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(clipPath);
        return clip;
    }


    public SkillEditorTableData GetEditorTableData()
    {
        return m_SkillEditorTableData;
    }

    public void Init()
    {
        Clear();
        InitCutscene();
        m_SkillEditorSet = Resources.Load<SkillEditorSet>("SkillEditorSet");
        m_SkillEditorTableData = Resources.Load<SkillEditorTableData>("SkillEditorTableData");
        foreach (var tableBase in m_SkillEditorTableData.m_SkillEditorTableBases)
        {
            m_SkillNameList.Add(tableBase.Key); 
        }
        InitPrefab();
        InitAnimation();
    }

    public void Clear()
    {
        m_SkillNameList.Clear();
    }

    public void InitCutscene()
    {
        m_Cutscene = GameObject.Find("Cutscene")?.GetComponent<Cutscene>();
        if (m_Cutscene!=null)
        {
            m_Cutscene.Stop();
            m_Cutscene.currentTime = 0;
            //Actor
            List<CutsceneGroup> cutsceneGroups = new List<CutsceneGroup>();
            var groups = m_Cutscene.groups;
            foreach (var group in groups)
            {
                cutsceneGroups.Add(group);
            }
            foreach (var group in cutsceneGroups)
            {
                m_Cutscene.DeleteGroup(group);
            }
            //Director
            List<ActionClip> actionClips = new List<ActionClip>();
            var directorGroup = m_Cutscene.groups[0];
            var tracks = directorGroup.tracks;
            //clear  track
            foreach (var track in tracks)
            {
                actionClips.Clear();
                foreach (var clip in track.clips)
                {
                    actionClips.Add(clip);
                }

                foreach (var clip in actionClips)
                {
                    track.DeleteAction(clip);
                }
            }
            
        }
        else
        {
            Debug.Log("Cutscene is null");
        }
    }

    private void InitPrefab()
    {
        if (m_SkillEditorSet.PrefabPath == null)
        {
            return;
        }
        m_SkillEditorSet.PrefabName.Clear();
        string dataPaht = CombineAssetPath(m_SkillEditorSet.PrefabPath);
        string[] filePahts = GetAllChild(dataPaht,"prefab");
        foreach (var filePath in filePahts)
        {
            
            string name = Path.GetFileName(filePath).Replace(".prefab","");
            m_SkillEditorSet.PrefabName.Add(name);

        }

        if (m_SkillEditorSet.CurrentPrefabIndex==-1)
        {
            m_SkillEditorSet.CurrentPrefabIndex = 0;
        }

        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(m_SkillEditorSet);
    }

    public void InitAnimation()
    {
        if (m_SkillEditorSet.AnimationPath == null)
        {
            return;
        }
        m_SkillEditorSet.AnimationName.Clear();
        string dataPath = CombineAssetPath(m_SkillEditorSet.AnimationPath);
        string naimaFolder = m_SkillEditorSet.PrefabName[m_SkillEditorSet.CurrentPrefabIndex];

        
        if (Directory.Exists(dataPath+naimaFolder))
        {
            string[] filePaths = GetAllChild(dataPath+naimaFolder, "anim");

            foreach (var filePath in filePaths)
            {
                string name = Path.GetFileName(filePath).Replace(".anim","");
                m_SkillEditorSet.AnimationName.Add(name);
            }
            AssetDatabase.SaveAssets();
            EditorUtility.SetDirty(m_SkillEditorSet);
        }
        else
        {
            Debug.LogError("没有找到配套的动画路径："+dataPath+naimaFolder);

        }

    }

    private string CombineAssetPath(string assetPath)
    {
        string path = Application.dataPath.Replace("Assets", "");
        return path + assetPath;
    }

    private string[] GetAllChild(string DataPath,string filter)
    {


        List<string> fileList = new List<string>();
        string[] FilePath =Directory.GetFiles(DataPath);
        string[] ExclusionList = new[] { ".meta" };
        foreach (var file in FilePath)
        {
            bool filterState = false;
            foreach (var exclusion in ExclusionList)
            {
                if (file.Contains(exclusion))
                {
                    filterState = true;
                }
            }

            if (!filterState)
            {
                if (file.Contains(filter))
                {
                    fileList.Add(file);
                }
            }
        }
        return fileList.ToArray();
    }


    public void Debugger()
    {
        InitCutscene();
        InitPrefab();
        InitAnimation();
    }
}
