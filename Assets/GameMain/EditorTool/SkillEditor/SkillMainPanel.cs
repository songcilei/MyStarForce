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
        m_Cutscene = GameObject.Find("Cusscene")?.GetComponent<Cutscene>();
        
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
        if (m_Cutscene!=null)
        {
            var groups = m_Cutscene.groups;
            foreach (var group in groups)
            {
                if (group.name.Contains("DIRECTOR"))
                {
                    
                }
            }

            ;
        }
    }
}
