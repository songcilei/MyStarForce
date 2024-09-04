using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using ECM2;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{

    private GameControl m_GameControl;
    private Character _character;
    private Vector3 camFroward;
    private bool Jump = false;
    private void Awake()
    {
        m_GameControl = new GameControl();
        m_GameControl.Enable();
        _character = GetComponent<Character>();
//Jump-----------------------         
        m_GameControl.Control.Jump.started += JumpStart;
        m_GameControl.Control.Jump.canceled += JumpEnd;
    }

    private void Start()
    {
        _character.camera = CameraMgr.Instance.cam;
        CameraMgr.Instance.initCamera(this);
        
    }

    private void Update()
    {
//move--------------------        
        Vector2 inputMove = m_GameControl.Control.move.ReadValue<Vector2>();
        Vector3 movementDirection = Vector3.zero;
        movementDirection += Vector3.right * inputMove.x;
        movementDirection += Vector3.forward * inputMove.y;
        if (_character.camera)
        {
            movementDirection = movementDirection.relativeTo(_character.cameraTransform);
        }
        _character.SetMovementDirection(movementDirection);

        if (Jump)
        {
            _character.Jump();
        }


    }

    private void OnDisable()
    {
        m_GameControl.Disable();
    }

    private void JumpStart(InputAction.CallbackContext action)
    {
        _character.Jump();
    }

    private void JumpEnd(InputAction.CallbackContext action)
    {
        _character.StopJumping();
    }

}
