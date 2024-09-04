using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using StarForce;
using UnityEngine;

public class WeaponProduct : ProductBase
{

    public WeaponProduct (ProductType type,int value,int attack = 0,int defence =0,int life= 1,string name = "")
    {
        //get table value
        IDataTable<DRProduct> productTable = GameEntry.DataTable.GetDataTable<DRProduct>();
        int typeId = (int)type;
        DRProduct drProduct= productTable.GetDataRow(typeId);
        if (drProduct == null)
        {
            return;
        }
        //Enum to string
        foreach (ShopType sT in Enum.GetValues(typeof(ShopType)))
        {
            string shopTypeStr = sT.ToString();
            string productTypeStr = type.ToString();
            Debug.Log(shopTypeStr+"::"+type.ToString());
            if (productTypeStr.Contains(shopTypeStr))
            {
                this.shopType = sT;
                break;
            }
        }
        
        
        this.name = drProduct.Name;
        this.type = type;
        this.life = drProduct.Life;
        this.attack = drProduct.Attack;
        this.defence = drProduct.Defence;
        this.value = drProduct.Value;

    }
}
