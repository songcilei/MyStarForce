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

            GameEntry.UI.OpenUIForm(UIFormId.MenuTest, this);

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

