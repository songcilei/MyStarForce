using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class VRCameraMgr : MonoBehaviour
{


    public static VRCameraMgr _Instance;
    public Camera cam;
    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    public List<CameraValue> objList = new List<CameraValue>();
    void Start()
    {
        foreach (var obj in objList)
        {
            obj.name = obj.obj.name;
            obj.mat = obj.obj.GetComponent<Renderer>().sharedMaterial;
        }

        Init();
    }



    void Init()
    {
        foreach (var obj in objList)
        {
            if (obj.name == "Sphere1")
            {
                obj.mat.SetFloat("_Alpha",1);
                SetChildObj(obj.obj.transform,true);
            }
            else
            {
                obj.mat.SetFloat("_Alpha",0);
                SetChildObj(obj.obj.transform,false);

            }
        }

        cam = GameObject.Find("Camera").GetComponent<Camera>();

    }
    
    


    private RaycastHit hit;
    void Update()
    {
#if !UNITY_EDITOR        
        if (Input.touchCount ==1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
    
                Vector2 screenPos = Input.GetTouch(0).position;

                Ray ray= cam.ScreenPointToRay(screenPos);
                Physics.Raycast(ray, out hit);
                if (hit.transform!= null)
                {
                    if (hit.transform.gameObject.GetComponent<OncheckEvent>())
                    {
                        hit.transform.gameObject.GetComponent<OncheckEvent>().runEvent();
                    }
                }
            }
        }
#else

        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            Debug.DrawLine(ray.origin, ray.direction*1000);
            Physics.Raycast(ray, out hit);
          
            if (hit.transform!= null)
            {
                if (hit.transform.gameObject.GetComponent<OncheckEvent>())
                {
                    hit.transform.gameObject.GetComponent<OncheckEvent>().runEvent();
                }
            }


        }
#endif
    }


    public void DisplayCube(string name)
    {
        foreach (var obj in objList)
        {
            if (obj.name == name)
            {
                obj.mat.DOFloat(1, "_Alpha", 0.5f);
                SetChildObj(obj.obj.transform,true);
            }

            if (obj.name != name)
            {
                obj.mat.DOFloat(0,"_Alpha",0.5f);
                SetChildObj(obj.obj.transform,false);
            }
        }
    }

    public void SetChildObj(Transform obj,bool state)
    {
        int count = obj.transform.childCount;

        for (int i = 0; i < count; i++)
        {
            Transform child = obj.GetChild(i);
            
            child.gameObject.SetActive(state);
            
        }
        
    }



}
