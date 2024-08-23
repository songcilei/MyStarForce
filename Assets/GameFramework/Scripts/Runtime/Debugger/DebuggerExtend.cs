using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityGameFramework.Runtime;

namespace UnityGameFramework.Runtime
{   
    public sealed partial class DebuggerComponent : GameFrameworkComponent
    {
        
        private sealed class ButtonTest : ScrollableDebuggerWindowBase
        {
            protected override void OnDrawScrollableWindow()
            {
                // throw new System.NotImplementedException();
                
                GUILayout.Label("TestButton");
                
                GUILayout.BeginVertical("box");

                if (GUILayout.Button("1111111111",GUILayout.Height(30f)))
                {
                    Debug.Log("3333333333333");    
                    
                }

                GUILayout.EndScrollView();
                
                
            }
        }
    }
}
