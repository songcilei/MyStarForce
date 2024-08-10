using GameFramework;
using GameFramework.ObjectPool;
using StarForce;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPlayer : MonoBehaviour
{
    //创建对象名称编号
    int CreateNums = 0;
    private IObjectPool<ModelInfoObject> m_OPPool;
  
    List<ModelInfoObject> UsingModelInfoObjectList;
    //这个是从外面挂载过来的 GameObject
    [SerializeField] private ModelInfor ModelInfor = null;
    void Start()
    {
        m_OPPool = GameEntry.ObjectPool.CreateSingleSpawnObjectPool<ModelInfoObject>(Utility.Text.Format("OP Pool ({0})", name), 20, 10, 0);
        m_OPPool.AutoReleaseInterval = 5;
        UsingModelInfoObjectList = new List<ModelInfoObject>();
        string cPaht = "Assets/GameTest/ObjectPool/Cube.prefab";
        ModelInfor = AssetDatabase.LoadAssetAtPath<ModelInfor>(cPaht);
        
    }
    [ContextMenu("CreateItem")]
    public void CreateItem()
    {
        for (int i = 0; i < 10; i++)
        {
            //创建实例化物体 
            ModelInfor ins = Instantiate(ModelInfor);
            //将实例化物体外面套一层用于Pool类型的  Object 物体 其实是方便扩展  不用也行
            ModelInfoObject _ModelInfoObject = ModelInfoObject.Create("模型" + CreateNums, CreateNums, ins);
            //创建对象
            m_OPPool.Register(_ModelInfoObject, true);
            UsingModelInfoObjectList.Add(_ModelInfoObject);
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
                m_OPPool.SetLocked(_ModelInfoObject, true);
                m_OPPool.SetPriority(_ModelInfoObject, 10);
            }
            ModelInfor Model = (ModelInfor)_ModelInfoObject.Target;

            Debug.LogError(Model.GetID());
        }
        
    }
    [ContextMenu("SpawnItem")]
    public void SpawnItem()
    {
        //获取对象  这个对象必须是 未使用(!IsInUse)的  被标记了回收的
        ModelInfoObject _ModelInfoObject = m_OPPool.Spawn("模型" + 0);
        UsingModelInfoObjectList.Add(_ModelInfoObject);
        Debug.LogError("获取的对象："+_ModelInfoObject.Name);
    }
    [ContextMenu("UnspawnItem")]
    public void UnspawnItem()
    {
        if (UsingModelInfoObjectList.Count>3)
        {
            for (int i = 2; i >= 0; i--)
            {
                ModelInfoObject _ModelInfoObject = UsingModelInfoObjectList[i];
                //回收对象
                m_OPPool.Unspawn(_ModelInfoObject);
                UsingModelInfoObjectList.Remove(_ModelInfoObject);
                Debug.LogError("释放的对象：" + _ModelInfoObject.Name);
            }
        }
    }
}