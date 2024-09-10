using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ProductBase
{
    public string name=null;
    public Texture2D icon;
    public string Info;
    public int number;
    public ProductType type = ProductType.None;//类型
    public ShopType shopType = ShopType.None;
    public int life = 1;//寿命
    public int hp = 25;
    public int attack = 1;//攻击力
    public int magic = 1;
    public int defence = 1;//防御力
    public int lucky = 0;
    public int value = 1;//价值
}
