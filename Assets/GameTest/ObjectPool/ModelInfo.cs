using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInfor : MonoBehaviour
{
    private int ID;
    private Mesh Mesh;
    private Texture2D Texture2D;
    public ModelInfor(int ID,GameObject o)
    {
        this.ID = ID;
        Mesh = null;
        Texture2D = null;
    }
    public ModelInfor()
    {
        this.ID = -1;
        Mesh = null;
        Texture2D = null;
    }
    public Mesh GetMesh()
    {
        return Mesh;
    }
    public Texture2D GetTexture2D()
    {
        return Texture2D;
    }
    

    
    public int GetID()
    {
        return ID;
    }
}