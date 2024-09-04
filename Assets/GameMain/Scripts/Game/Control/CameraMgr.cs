using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using ECM2.Examples;
using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    public static CameraMgr Instance;
    public Camera cam;
    public Transform camVir;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void initCamera(PlayerInputSystem play)
    {
        var ss = this.GetComponentInChildren<SimpleCameraController>();
            ss.target = play.transform;
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
