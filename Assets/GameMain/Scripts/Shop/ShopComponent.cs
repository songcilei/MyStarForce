using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class ShopComponent : GameFrameworkComponent
    {
        // [SerializeField]
        // private List<ProductBase> ProductList = null;
    
        public ProductScriptable ProductSet;
    
        protected override void Awake()
        {
            base.Awake();
            if (ProductSet == null)
            {
                ProductSet = Resources.Load<ProductScriptable>("ProductAsset");
            }
        }
    
        private void Start()
        {
            GameEntry.Event.Subscribe(ProductSubEventArgs.EventId,ProductSub);
            GameEntry.Event.Subscribe(ProductAddEventArgs.EventId,ProductAdd);
        }
    
    
        private void Update()
        {
            
        }
        
    
        
    
        public void AddProduct(ProductBase product,int number)
        {
            for (int i = 0; i < number; i++)
            {
                ProductSet.ProductList.Add(product);
            }
            
        }
    
        public void DeleteProductForType(ProductBase product)
        {
            Debug.Log(product.name);
            for (int i = ProductSet.ProductList.Count-1; i >0 ; i--)
            {
                Debug.Log(product.name);
                if (ProductSet.ProductList[i].type == product.type)
                {
                    ProductSet.ProductList.RemoveAt(i);
                    break;
                }
            }
        }


        public void ProductSub(object sender,GameEventArgs e)
        {
            ProductSubEventArgs ne = (ProductSubEventArgs)e;
            DeleteProductForType( ne.m_ProductBase);
            Debug.Log("ProductSub!!!!");

        }

        public void ProductAdd(object sender,GameEventArgs e)
        {
            ProductAddEventArgs ne = (ProductAddEventArgs)e;
            AddProduct(ne.m_ProductBase,1);
            Debug.Log("ProductAdd!!!!!");
        }


        public void ClearProduct()
        {
            ProductSet.ProductList.Clear();
        }

        //分发物品到各个代理
        public void DispenseShopAgent(ShopAgent[] shopAgents)
        {
            foreach (var agent in shopAgents)
            {
                agent.Clear();
            }
            for (int i = 0; i < shopAgents.Length; i++)
            {
                foreach (var product in ProductSet.ProductList)
                {
                    if (product.shopType == shopAgents[i].ShopType)
                    {
                        shopAgents[i].ProductBases.Add(product);
                    }
                }
            }
        }


        private void OnDestroy()
        {
            // GameEntry.Event.Unsubscribe(ProductSubEventArgs.EventId,ProductSub);
            // GameEntry.Event.Unsubscribe(ProductAddEventArgs.EventId,ProductAdd);
    #if UNITY_EDITOR
            EditorUtility.SetDirty(ProductSet);
            AssetDatabase.SaveAssets();
    #endif
        }
    }

}
