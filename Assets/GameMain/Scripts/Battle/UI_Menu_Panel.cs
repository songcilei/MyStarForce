using System;
using System.Collections;
using System.Collections.Generic;
using StarForce;
using UnityEngine;

public enum UIMenuPanelState
{
    None,
    Atk,
    Skill,
    Prop,
    Def,
    Leave
}

public class UI_Menu_Panel : MonoBehaviour
{
    private UIMenuPanelState _UIMenuPanelState = UIMenuPanelState.None;
    
    private CanvasGroup _canvasGroup;

    private GameControl _gameControl;
    
    private void Start()
    {
        _canvasGroup = this.GetComponent<CanvasGroup>();
        _gameControl = new GameControl();
    }


    private void Update()
    {
        switch (_UIMenuPanelState)
        {
            case UIMenuPanelState.Atk:
                PlayerFSM player = GetRayPlayer();
                if (player)
                {
                    player.ActiveSelectEffect();

                    if (_gameControl.Control.Fire.triggered)
                    {
                        
                    }
                }

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

    private PlayerFSM GetRayPlayer()
    {
        //Vector3 worldPos = Camera.current.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.current.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            PlayerFSM player;
            if (hit.collider.gameObject.TryGetComponent(out player))
            {
                if (player.PlayerType == PlayerType.Enemy)
                {
                    return player;
                }
            }
        }

        return null;
    }

    private void ShowUIPanel()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    private void HideUIPanel()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }


    public void Atk_Action()
    {
        //1: hide ui
        HideUIPanel();
        //2: select atk target
        _UIMenuPanelState = UIMenuPanelState.Atk;
        //GameEntry.Event.Fire(this,PlayerAtkEventArgs.Create());
    }

    public void Skill_Action()
    {
        //1:show skill window
        //2:select skill
        //3:hide menu UI
        //4:select atk target
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

}
