using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable/CreatePackScriptable",fileName = "PackAsset")]
public class PackScriptable : SerializedScriptableObject
{
    public Dictionary<string, ProductBase> PackList = new Dictionary<string, ProductBase>();
}
