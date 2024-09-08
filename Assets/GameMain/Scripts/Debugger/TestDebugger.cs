using System.Collections;
using System.Collections.Generic;
using GameFramework.Debugger;
using StarForce;
using UnityEngine;

public class TestDebugger : IDebuggerWindow
{
    private Vector2 m_ScrollPosition = Vector2.zero;
    
    public void Initialize(params object[] args)
    {
    }

    public void Shutdown()
    {
    }

    public void OnEnter()
    {
    }

    public void OnLeave()
    {
    }

    public void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        
    }

    public void OnDraw()
    {
        GUILayout.BeginScrollView(m_ScrollPosition);
        if (GUILayout.Button("Add Weapon",GUILayout.Height(30)))
        {
            WeaponProduct weaponProduct = new WeaponProduct(ProductType.Weapon_Sword1,1);
            // GameEntry.Shop.AddProduct(weaponProduct,2);
            GameEntry.Event.Fire(this,ProductAddEventArgs.Create(weaponProduct));
        }

        if (GUILayout.Button("Clear Weapon",GUILayout.Height(30)))
        {
            WeaponProduct weaponProduct = new WeaponProduct(ProductType.Weapon_Sword1,1);
            // GameEntry.Shop.ClearProduct();
            GameEntry.Event.Fire(this,ProductSubEventArgs.Create(weaponProduct));
        }

        if (GUILayout.Button("Add Item",GUILayout.Height(30)))
        {
            WeaponProduct ItemProduct = new WeaponProduct(ProductType.Prop_book1,1);
            GameEntry.Event.Fire(this,ProductAddEventArgs.Create(ItemProduct));
        }

        if (GUILayout.Button("Clear Item",GUILayout.Height(30)))
        {
            WeaponProduct ItemProduct = new WeaponProduct(ProductType.Prop_book1,1);
            GameEntry.Event.Fire(this,ProductSubEventArgs.Create(ItemProduct));
        }

        if (GUILayout.Button("clear all",GUILayout.Height(30)))
        {
            GameEntry.Shop.ClearProduct();
        }

        
        
        GUILayout.EndScrollView();

    }
    
    
    
}
