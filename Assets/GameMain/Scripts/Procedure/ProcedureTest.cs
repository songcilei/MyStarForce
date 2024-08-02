using System.Collections;
using System.Collections.Generic;
using GameFramework.DataTable;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class ProcedureTest : ProcedureBase
    {
        public override bool UseNativeDialog {
            get
            {
                return false;
            }
        }


        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);

        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {   
            base.OnEnter(procedureOwner);
            Debug.Log("into   test Scene!!!!");

            int sceneId = procedureOwner.GetData<VarInt32>("NextSceneId");
            Debug.Log("sceneId:"+sceneId);
            IDataTable<DRScene> dtScene = GameEntry.DataTable.GetDataTable<DRScene>();
            DRScene drScene = dtScene.GetDataRow(sceneId);
            
            string ScenePath = AssetUtility.GetSceneAsset(drScene.AssetName);
//卸载场景
            string[] loadedSceneAssetNames = GameEntry.Scene.GetLoadedSceneAssetNames();
            for (int i = 0; i < loadedSceneAssetNames.Length; i++)
            {
                GameEntry.Scene.UnloadScene(loadedSceneAssetNames[i]);
            }
            
            
            GameEntry.Scene.LoadScene(ScenePath,Constant.AssetPriority.SceneAsset,this);
            
            ChangeState<procedureUITest>(procedureOwner);
            
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
        }
    }
}

