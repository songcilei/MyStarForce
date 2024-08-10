using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssetTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var VRPrefab = Resources.Load("VRCamera");
        GameObject.Instantiate(VRPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
