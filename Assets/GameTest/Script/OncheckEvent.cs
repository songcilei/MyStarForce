using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OncheckEvent : MonoBehaviour
{
    public string TargetName;

    public void runEvent()
    {
        VRCameraMgr._Instance.DisplayCube(TargetName);
    }
}
