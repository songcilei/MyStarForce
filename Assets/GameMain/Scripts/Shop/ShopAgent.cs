using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAgent : MonoBehaviour
{
    public ShopType ShopType = ShopType.None;
    public List<ProductBase> ProductBases = new List<ProductBase>();



    public void Clear()
    {
        ProductBases.Clear();
    }

}
