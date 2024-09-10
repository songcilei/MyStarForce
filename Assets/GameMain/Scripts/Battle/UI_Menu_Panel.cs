using System;
using System.Collections;
using System.Collections.Generic;
using StarForce;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum UIMenuPanelState
{
    None,
    Atk,
    Skill,
    SkillMenu,
    Prop,
    Def,
    Leave
}

public class UI_Menu_Panel : MonoBehaviour
{
    private UIMenuPanelState _UIMenuPanelState = UIMenuPanelState.None;
    
    private CanvasGroup _canvasGroup;

    private GameControl _gameControl;

    private PlayerFSM player;

    public RectTransform SkillUIPanel;
    private CanvasGroup _skillUiPanelGroup;
    public RectTransform SkillUITemp;
    private IAction currentAction;
    private void Start()
    {
        _canvasGroup = this.GetComponent<CanvasGroup>();
        _gameControl = new GameControl();
        _gameControl.Enable();
        
        _gameControl.Control.SelectTarget.started += SelectPress;
        _gameControl.Control.SelectTarget.canceled += SelectUp;
        if (SkillUIPanel!=null)
        {
            _skillUiPanelGroup = SkillUIPanel.GetComponent<CanvasGroup>();
        }
    }

    private void Update()
    {
        //play effect
        switch (_UIMenuPanelState)
        {
            case UIMenuPanelState.Atk:
                player = GetRayPlayer();
                SelectEffect(player);
                break;
            case UIMenuPanelState.SkillMenu:
                break;
            case UIMenuPanelState.Skill:
                player = GetRayPlayer();
                SelectEffect(player);
                break;
            case UIMenuPanelState.Prop:
                break;
            case UIMenuPanelState.Def:
                break;
            case UIMenuPanelState.Leave:
                break;
            default:
                break;
        }
    }

    private void SelectPress(InputAction.CallbackContext callback)
    {
        Debug.Log("select press");
        switch (_UIMenuPanelState)
        {
            case UIMenuPanelState.Atk:
                Debug.Log("run atk");
                if (player)
                {
                    
                    GameEntry.Event.Fire(this,PlayerAtkEventArgs.Create(player.entityId));
                }

                break;
            case UIMenuPanelState.SkillMenu:
                break;
            case UIMenuPanelState.Skill:
                if (player)
                {
                    currentAction.Execute(player);
                    GameEntry.Event.Fire(this,PlayerSkillEventArgs.Create());
                    HideUIPanel();
                }

                break;
            case UIMenuPanelState.Prop:
                break;
            case UIMenuPanelState.Def:
                break;
            case UIMenuPanelState.Leave:
                break;
            default:
                break;
        }
    }
    private void SelectUp(InputAction.CallbackContext callback)
    {
        switch (_UIMenuPanelState)
        {
            case UIMenuPanelState.Atk:
                break;
            case UIMenuPanelState.SkillMenu:
                break;
            case UIMenuPanelState.Skill:
                break;
            case UIMenuPanelState.Prop:
                break;
            case UIMenuPanelState.Def:
                break;
            case UIMenuPanelState.Leave:
                break;
            default:
                break;
        }
    }
    
    int TempId;
    private void SelectEffect(PlayerFSM player)
    {
        if (player)
        {
            TempId = player.entityId;
            player.ActiveSelectEffect();
        }
        else
        {
            if (TempId!=0)
            {
                PlayerFSM fsm = GameEntry.Entity.GetEntity(TempId).Logic as PlayerFSM;
                fsm.ResetSelectEffect();
                TempId = 0;
            }
        }
    }

    private PlayerFSM GetRayPlayer()
    {
        //Vector3 worldPos = Camera.current.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = BattleMgr.Instance.camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            PlayerFSM player;
            if (hit.collider.gameObject.TryGetComponent<PlayerFSM>(out player))
            {
                if (player.PlayerType == PlayerType.Enemy)
                {
                    return player;
                }
            }
        }

        return null;
    }

    public void ShowUIPanel()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void HideUIPanel()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    private List<RectTransform> TempSkillUIList = new List<RectTransform>();
    public void ShowSkillUIPanel()
    {
        TempSkillUIList.Clear();
        if (_skillUiPanelGroup!=null)
        {
            _skillUiPanelGroup.alpha = 1;
            _skillUiPanelGroup.interactable = true;
            _skillUiPanelGroup.blocksRaycasts = true;
        }

        PlayerFSM currenFsm = GameEntry.BattleSystem.CurrentPlayer;

        if (currenFsm)
        {
            foreach (var skill in currenFsm.SkillList)
            {
                if (SkillUITemp!=null)
                {
                    var obj =Instantiate(SkillUITemp, SkillUIPanel);
                    obj.gameObject.SetActive(true);
                    TempSkillUIList.Add(obj);
                    Button button = obj.GetComponent<Button>();
                    button.name = skill.Key;
                    button.transform.GetChild(0).GetComponent<Text>().text = skill.Key;
                    button.onClick.AddListener(() => { Skill_Action(skill.Value); });
                
                }
            }
        }
    }

    public void HideSkillUIPanel()
    {
        if (_skillUiPanelGroup != null)
        {
            _skillUiPanelGroup.alpha = 0;
            _skillUiPanelGroup.interactable = false;
            _skillUiPanelGroup.blocksRaycasts = false;
        }

        foreach (var skill in TempSkillUIList)
        {
            if (skill!=null)
            {
                GameObject.Destroy(skill.gameObject);
            }
        }
    }




    public void Atk_Action()
    {
        //1: hide ui
        HideUIPanel();
        //2: select atk target  Play Effect  and ButtonAction
        _UIMenuPanelState = UIMenuPanelState.Atk;
        
    }

    public void SkillWin_Aciton()
    {
        //1:show skill window
        ShowSkillUIPanel();
        _UIMenuPanelState = UIMenuPanelState.SkillMenu;
    }
 
    public void Skill_Action(IAction action)
    {
        //1:set action
        
        //2:select skill
        currentAction = action;
        //3:hide menu UI
        HideSkillUIPanel();
        //4:select atk target
        _UIMenuPanelState = UIMenuPanelState.Skill;
        //5:fire evet
    }

    public void Prop_Action()
    {
        //1:show prop window
        //2:select prop
        //3:select hero
    }

    public void Def_Action()
    {
        //1:def
    }

    public void Leave_Action()
    {
        //1:leave
    }

    private void OnDestroy()
    {
        _gameControl.Disable();
    }
}
