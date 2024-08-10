using GameFramework;
using GameFramework.ObjectPool;
using UnityEngine;

namespace StarForce
{
    public class ModelInfoObject : ObjectBase
    {
        private int m_ModelID;
        
        public ModelInfoObject()
        {
        }
    
        public static ModelInfoObject Create(string name,int ID, ModelInfor Model)
        {
            ModelInfoObject InstanceOP = ReferencePool.Acquire<ModelInfoObject>();
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
    
}
