using GameFramework;
using GameFramework.ObjectPool;
using StarForce;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlayer : MonoBehaviour
{
    //创建对象名称编号
    int CreateNums = 0;
    private IObjectPool<OPGame> m_OPPool;
    List<OPGame> UsingOPGames;
    void Start()
    {
        m_OPPool = GameEntry.ObjectPool.CreateSingleSpawnObjectPool<OPGame>(Utility.Text.Format("OP Pool ({0})", name), 20, 10, 0);
        m_OPPool.AutoReleaseInterval = 5;
        UsingOPGames = new List<OPGame>();
    }
    [ContextMenu("CreateItem")]
    public void CreateItem()
    {
        for (int i = 0; i < 10; i++)
        {
            ModelInfor ModelInfor = new ModelInfor(i);
            OPGame OPGame = OPGame.Create("模型" + CreateNums, CreateNums, ModelInfor);
            //创建对象
            m_OPPool.Register(OPGame, true);
            UsingOPGames.Add(OPGame);
            if (CreateNums == 2)
            {
                CreateNums = 0;
            }
            else
            {
                CreateNums++;
            }
            if (i==0)
            {
                m_OPPool.SetLocked(OPGame, true);
                m_OPPool.SetPriority(OPGame, 10);
            }
            ModelInfor Model = (ModelInfor)OPGame.Target;
            Debug.LogError(Model.GetID());
        }
        
    }
    [ContextMenu("SpawnItem")]
    public void SpawnItem()
    {
        //获取对象
        OPGame OPGame = m_OPPool.Spawn("模型" + 0);
        UsingOPGames.Add(OPGame);
        Debug.LogError("获取的对象："+OPGame.Name);
    }
    [ContextMenu("UnspawnItem")]
    public void UnspawnItem()
    {
        if (UsingOPGames.Count>3)
        {
            for (int i = 2; i >= 0; i--)
            {
                OPGame OPGame = UsingOPGames[i];
                //回收对象
                m_OPPool.Unspawn(OPGame);
                UsingOPGames.Remove(OPGame);
                Debug.LogError("释放的对象：" + OPGame.Name);
            }
        }
    }
}