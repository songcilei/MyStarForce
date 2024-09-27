using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class ImageChangeCovert : EditorWindow
{

    public Texture2D targetTex;
    
    private bool Inv_R = false;
    private bool Inv_G = false;
    private bool Inv_B = false;
    private bool Inv_A = false;


    public Texture2D CombinTex1;
    public Texture2D CombinTex2;
    public Texture2D CombinTex3;

    private GUIStyle Style;
    
    [MenuItem("TATools/TextureCovert")]
    static void ImageChangeWin()
    {
        var win = EditorWindow.GetWindow<ImageChangeCovert>();
        win.Show();
    }
    

    private void OnGUI()
    {
        Style = new GUIStyle();
        Style.fontSize = 40;
        Style.normal.textColor = Color.white;
        
        GUILayout.BeginVertical(GUI.skin.box);
        GUILayout.Label("    贴图通道反转",Style);
        GUILayout.Label("法线贴图：");
        targetTex = EditorGUILayout.ObjectField(targetTex,typeof(Texture2D),false) as Texture2D;
        
        Inv_R = GUILayout.Toggle(Inv_R,"翻转R通道");
        Inv_G = GUILayout.Toggle(Inv_G,"翻转G通道(OpenGL法线和DX法线 转这个)");
        Inv_B = GUILayout.Toggle(Inv_B,"翻转B通道");
        Inv_A = GUILayout.Toggle(Inv_A,"翻转A通道");

        if (GUILayout.Button("转换贴图",GUILayout.Height(100)))
        {
            CovertTextureChange();
        }
        GUILayout.EndVertical();
        
        GUILayout.Space(30);
        GUILayout.Label("    贴图通道合并",Style);
        GUILayout.BeginVertical(GUI.skin.box);
        GUILayout.Label("合并贴图：");
        GUILayout.Label("NormalMap");
        CombinTex1 = EditorGUILayout.ObjectField(CombinTex1,typeof(Texture2D),false) as Texture2D;
        GUILayout.Label("Roughness");
        CombinTex2 = EditorGUILayout.ObjectField(CombinTex2,typeof(Texture2D),false) as Texture2D;
        GUILayout.Label("Metal");
        CombinTex3 = EditorGUILayout.ObjectField(CombinTex3,typeof(Texture2D),false) as Texture2D;

        if (GUILayout.Button("合并贴图",GUILayout.Height(100)))
        {
            CombineTexture();
        }
        GUILayout.EndVertical();

    }

    private void CombineTexture()
    {
        if (CombinTex1.width!=CombinTex2.width|| CombinTex1.width !=CombinTex3.width)
        {
            EditorUtility.DisplayDialog("错误", "错误！待转换图片的分辨率不相等", "ok");
        }

        if (CombinTex1 == null)
        {
            return;
        }

        SwitchRW(CombinTex1, true);
        if (CombinTex2!=null)
        {
            SwitchRW(CombinTex2, true);
        }

        if (CombinTex3!=null)
        {
            SwitchRW(CombinTex3, true);
        }

        int width = CombinTex1.width;
        int height = CombinTex1.height;
        Color[] colors = CombinTex1.GetPixels(0, 0, width, height);
        Color[] RoughColors = CombinTex2.GetPixels(0, 0, width, height);
        Color[] MetalColors = CombinTex3.GetPixels(0, 0, width, height);
        

        for (int i = 0; i < colors.Length; i++)
        {
            if (CombinTex2!=null)
            {
                colors[i].b = RoughColors[i].r;
            }

            if (CombinTex3!=null)
            {
                colors[i].a = MetalColors[i].r;
            }
        }
        Texture2D newTex = new Texture2D(width,height);
        newTex.SetPixels(colors);
        newTex.Apply();
        CreateTextureToAsset(targetTex,newTex,"_Combine.tga");
        
    }




    private void CovertTextureChange()
    {
        if (targetTex==null)
        {
            return;
        }

        SwitchRW(targetTex,true);
        int width = targetTex.width;
        int height = targetTex.height;
        Color[] colors = targetTex.GetPixels(0, 0, width, height);
        
        for (int i = 0; i < colors.Length; i++)
        {
            if (Inv_R)
            {
                colors[i].r = 1 - colors[i].r;
            }
            if (Inv_G)
            {
                colors[i].g = 1 - colors[i].g;
            }
            if (Inv_B)
            {
                colors[i].b = 1 - colors[i].b;
            }
            if (Inv_A)
            {
                colors[i].a = 1 - colors[i].a;
            }
        }
        // RenderTexture rt = RenderTexture.GetTemporary(width, height);
        Texture2D newTex = new Texture2D(width,height);
        newTex.SetPixels(colors);
        newTex.Apply();

        CreateTextureToAsset(targetTex,newTex,"_Inv.tga");

    }

    private void CreateTextureToAsset(Texture2D targetTex,Texture2D newTex,string name)
    {
        string TexPath = AssetDatabase.GetAssetPath(targetTex);
        string[] sArray = TexPath.Split('.');
        TexPath = sArray[0];

        byte[] bytes = newTex.EncodeToTGA();
        string fileName = TexPath + name;
        System.IO.File.WriteAllBytes(fileName,bytes);
        SwitchRW(targetTex,false);
        AssetDatabase.Refresh();
    }

    private void SwitchRW(Texture2D tex,bool RW)
    {
        string assetPath = AssetDatabase.GetAssetPath(tex);
        TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        importer.isReadable = RW;
        importer.SaveAndReimport();
        // AssetDatabase.Refresh();
    }


}
