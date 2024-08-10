using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework.UI;
using StarForce;
using UnityEngine;
using OpenUIFormSuccessEventArgs = UnityGameFramework.Runtime.OpenUIFormSuccessEventArgs;

namespace StarForce
{
    public class procedureUITest : ProcedureBase
    {
    
        private MenuTest m_MenuTest = null;
        public override bool UseNativeDialog {
            get
            {
                return false;
            }
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId,OnOpenUIFormSuccess);

            //GameEntry.UI.OpenUIForm(UIFormId.MenuTest, this);
        //按数据类型加载 可以初始化Entity参数 其实是根据EntityTable查表Entity ID
            // var cameraData = new VRCameraData(GameEntry.Entity.GenerateSerialId(), 70003);
            // GameEntry.Entity.ShowVRCamera(cameraData);
            
        //按Entity 的名字和路径加载    
            string VRCameraPath = AssetUtility.GetEntityAsset("VRCamera"); 
            GameEntry.Entity.ShowEntity<VRCamera>(GameEntry.Entity.GenerateSerialId(),VRCameraPath,"Effect");
        
        
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId,OnOpenUIFormSuccess);
            
        }


        private void OnOpenUIFormSuccess(object sender,GameEventArgs e)
        {
            Debug.Log("open the UI!!!!!!!!!!!!!");
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData!=this)
            {
                return;
            }

            m_MenuTest = (MenuTest)ne.UIForm.Logic;

        }
    }
}

