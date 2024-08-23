using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable/CreateProductScriptable",fileName = "ProductAsset")]
public class ProductScriptable : ScriptableObject
{
    
    public List<ProductBase> ProductList = new List<ProductBase>();
}
