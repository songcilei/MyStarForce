using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using StarForce;
using UnityEngine;

public enum SkillKey
{
    _buff,
    _atk,//伤害技能
    _range,//范围技能
    
    
}
public enum AIState
{
    None,
    BufferSKill,
    Skill,
    Normal,
}


public class AILogic
{

    [LabelText("max:6")]
    public int AIlevel = 0;


    private AIState m_AIState = AIState.None;
    private PlayerFSM m_Fsm;
    private int HP;
    private int Magic;
    private Dictionary<string, IAction> m_SkillList = new Dictionary<string, IAction>();
    private List<PlayerFSM> heroList;

    public AILogic(int aIlevel)
    {
        AIlevel = aIlevel;
    }

    public void AI_Init(PlayerFSM fsm)
    {
        if (fsm)
        {
            m_Fsm = fsm;
        }

        HP = fsm.Hp;
        Magic = fsm.Mag;
        m_SkillList = fsm.SkillList;
    }


    public void AI_Update()
    {
        heroList = GameEntry.BattleSystem.GetLiveHeroList();
        switch (m_AIState)
        {
            case AIState.None:
                break;
            case AIState.BufferSKill:
                ComputeBufferSkill(m_Fsm);
                break;
            case AIState.Skill:
                ComputeSkill(m_Fsm);
                break;
            case AIState.Normal:
                ComputeNormal(m_Fsm);
                break;
            default:
                break;
        }
    }

    //start think
    public void StartThink()
    {
        m_AIState = AIState.BufferSKill;
    }

    public void ComputeBufferSkill(PlayerFSM fsm)
    {
        if (m_SkillList.Count==0)
        {
            return;
        }
        //身上没有buffer
        if (fsm.BufferList.Count == 0)
        {
            if (ThinkSuccess())
            {
                IAction action =GetRandomSkill(SkillKey._buff);
                action.Execute(fsm);
                GameEntry.Event.Fire(this,PlayerSkillEventArgs.Create());
                m_AIState = AIState.None;
            }
        }
        m_AIState = AIState.Skill;
    }

    public void ComputeSkill(PlayerFSM fsm)
    {
        if (m_SkillList.Count==0)
        {
            return;
        }

        if (heroList.Count>2)
        {
            //范围技能
            if (ThinkSuccess())
            {
                IAction action = GetRandomSkill(SkillKey._range);
                PlayerFSM hero = GetSoloLiveHero();
                action.Execute(fsm,hero.entityId);
                GameEntry.Event.Fire(this,PlayerSkillEventArgs.Create());
                m_AIState = AIState.None;
            }
        }

        if (ThinkSuccess())
        {//单体技能
            IAction action = GetRandomSkill(SkillKey._atk);
            PlayerFSM hero = GetSoloLiveHero();
            action.Execute(fsm,hero.entityId);
            GameEntry.Event.Fire(this,PlayerSkillEventArgs.Create());
            m_AIState = AIState.None;
        }
        m_AIState = AIState.Normal;

    }

    public void ComputeNormal(PlayerFSM fsm)
    {
        PlayerFSM liveHero = GetSoloLiveHero();
        GameEntry.Event.Fire(this,PlayerAtkEventArgs.Create(liveHero.entityId));
        m_AIState = AIState.None;
    }


    public IAction GetRandomSkill(SkillKey key)
    {
        List<IAction> actions = new List<IAction>();
        foreach (var skill in m_SkillList)
        {
            if (skill.Key.Contains(key.ToString()))
            {
                actions.Add(skill.Value);
            }
        }

        if (actions.Count>0)
        {
            int index = Random.Range(0, actions.Count);
            return actions[index];
        }
        return null;
    }


    //思考成功
    private bool ThinkSuccess()
    {
        int rand = (int)Mathf.Floor(Random.Range(0, 6/AIlevel));
        if (rand == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private PlayerFSM GetSoloLiveHero()
    {
        int index = Random.Range(0, heroList.Count);
        return heroList[index];
    }
    
}
