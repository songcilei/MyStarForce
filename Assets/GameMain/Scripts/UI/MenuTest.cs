using System.Collections;
using System.Collections.Generic;
using StarForce;
using UnityEngine;

public class MenuTest : UGuiForm
{
    void Start()
    {
        
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);

        Debug.Log("On Open UI!!!");
    }

    void Update()
    {
        
    }
}
