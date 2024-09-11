using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;

public class PackComponent : GameFrameworkComponent
{

    public PackScriptable _packScriptable;
    public string CurrentSelectKey = string.Empty;
    protected override void Awake()
    {
        base.Awake();
    }

    public void AddPack(ProductBase Pbase)
    {
        if (_packScriptable.PackList.ContainsKey(Pbase.name))
        {
            _packScriptable.PackList[Pbase.name].number++;
        }
        else
        {
            _packScriptable.PackList[Pbase.name] = Pbase;
        }
#if UNITY_EDITOR        
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(_packScriptable);
#endif        
    }

    public void ExpendPack(string Key)
    {
        if (_packScriptable.PackList.ContainsKey(Key))
        {
            _packScriptable.PackList[Key].number--;
            if (_packScriptable.PackList[Key].number==0)
            {
                _packScriptable.PackList.Remove(Key);
            }
        }
        else
        {
            Debug.LogError("dont have pack:"+Key);   
        }
    }

    
    public void SetCurrenPack(string key)
    {
        CurrentSelectKey = key;
    }

    public void ClearCreentPackKey()
    {
        CurrentSelectKey = string.Empty;
    }

    public void UsePack(string key,PlayerFSM fsm)
    {
        if (!_packScriptable.PackList.ContainsKey(key))
        {
            return;
        }

        ProductBase Pbase = _packScriptable.PackList[key];
        
        fsm.UseDrug(Pbase);
    }

    public void UsePack(PlayerFSM fsm)
    {
        if (!_packScriptable.PackList.ContainsKey(CurrentSelectKey))
        {
            return;
        }

        ProductBase Pbase = _packScriptable.PackList[CurrentSelectKey];
        
        fsm.UseDrug(Pbase);
    }


//----------------------------------------------------------------
    public void AddLifeDrug()
    {
        ProductBase addLife = new ProductBase()
        {
            name =  "blood drug",
            icon = Texture2D.blackTexture,
            Info = "add life drug!!!",
            type = ProductType.Drug_miniLife,
            shopType = ShopType.Drug,
            life = 1,
            hp = 25,
            attack = 0,
            magic = 0,
            defence = 0,
            lucky = 0,
            value = 10,
        };
        AddPack(addLife);
    }
    
    public void AddMagicDrug()
    {
        ProductBase addMagic = new ProductBase()
        {
            name =  "magic drug",
            icon = Texture2D.blackTexture,
            Info = "add life drug!!!",
            type = ProductType.Drug_miniMagic,
            shopType = ShopType.Drug,
            life = 1,
            hp = 0,
            attack = 0,
            magic = 25,
            defence = 0,
            lucky = 0,
            value = 10,
        };
        AddPack(addMagic);
    }

    public void ExpendLifeDrug()
    {
        ExpendPack("life drug");
    }
    
    public void ExpendMagicDrug()
    {
        ExpendPack("Magic drug");
    }
}
