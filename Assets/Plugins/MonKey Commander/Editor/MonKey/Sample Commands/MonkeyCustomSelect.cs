using System.Collections;
using System.Collections.Generic;
using MonKey;
using UnityEditor;
using UnityEngine;

namespace MonKey.Editor.Commands
{

    public static class MonkeyCustomSelect
    {
        [Command("Select Texture Folder",
            Help = "null",
            QuickName = "T")]

        public static void MkSelectTexture()
        {
            // Selection.activeObject = GameOb0ject.Find("")
            string path = "Assets/GameMain/Art/Textures";
            UnityEngine.Object folder = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
            EditorGUIUtility.PingObject(folder);
        }
        
        
        [Command("Select Mesh Folder",
            Help = "null",
            QuickName = "Me")]

        public static void MkSelectMesh()
        {
            // Selection.activeObject = GameOb0ject.Find("")
            string path = "Assets/GameMain/Art/Meshes";
            UnityEngine.Object folder = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
            EditorGUIUtility.PingObject(folder);
        }
        
        [Command("Select Material Folder",
            Help = "null",
            QuickName = "M")]

        public static void MkSelectMaterial()
        {
            // Selection.activeObject = GameOb0ject.Find("")
            string path = "Assets/GameMain/Art/Materials";
            UnityEngine.Object folder = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
            EditorGUIUtility.PingObject(folder);
        }
        
        [Command("Select Prefab Folder",
            Help = "null",
            QuickName = "O")]

        public static void MkSelectPrefab()
        {
            // Selection.activeObject = GameOb0ject.Find("")
            string path = "Assets/GameMain/Object";
            UnityEngine.Object folder = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
            EditorGUIUtility.PingObject(folder);
        }
        
    }

}
