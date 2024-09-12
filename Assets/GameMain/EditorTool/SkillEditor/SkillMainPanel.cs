using System.Collections;
using System.Collections.Generic;
using Slate;
using UnityEngine;

public class SkillMainPanel : MonoBehaviour
{
    private SkillEditorSet m_SkillEditorSet;
    private SkillEditorTableData m_SkillEditorTableData;
    private List<string> m_SkillNameList;
    public int CurrentSkillIndex=0;

    private Cutscene m_Cutscene;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string[] GetSkillNameList()
    {
        return m_SkillNameList.ToArray();
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

    public void Debugger()
    {
        InitCutscene();
    }
}
