using GameFramework;
using GameFramework.ObjectPool;
using UnityEngine;

public class OPGame : ObjectBase
{
    private int m_ModelID;

    public OPGame()
    {
    }

    public static OPGame Create(string name,int ID, ModelInfor Model)
    {
        OPGame InstanceOP = ReferencePool.Acquire<OPGame>();
        InstanceOP.Initialize(name, Model);
        InstanceOP.m_ModelID = ID;
        return InstanceOP;
    }

    public override void Clear()
    {
        base.Clear();
        m_ModelID = -1;
    }

    protected override void Release(bool isShutdown)
    {
        // throw new System.NotImplementedException();
    }
}
public class ModelInfor
{
    private int ID;
    private Mesh Mesh;
    private Texture2D Texture2D;
    public ModelInfor(int ID)
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