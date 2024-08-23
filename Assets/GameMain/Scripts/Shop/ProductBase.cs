using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ProductBase
{
    public string name=null;
    public ProductType type = ProductType.None;//类型
    public ShopType shopType = ShopType.None;
    public int life = 0;//寿命
    public int attack = 0;//攻击力
    public int defence = 0;//防御力
    public int value = 0;//价值
}
