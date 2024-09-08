using GameFramework.Debugger;
using UnityEngine;

namespace StarForce
{
    
    public class BattleDebugger : IDebuggerWindow
    {
        private Vector2 m_ScrollPosition = Vector2.zero;
        public void Initialize(params object[] args)
        {
        }

        public void Shutdown()
        {
        }

        public void OnEnter()
        {
        }

        public void OnLeave()
        {
        }

        public void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
        }

        private BattleVolume battleVolume;
        public void OnDraw()
        {
            GUILayout.BeginScrollView(m_ScrollPosition);

            if (GUILayout.Button("Start Battle"))
            {
                battleVolume =Object.FindObjectOfType<BattleVolume>();
                battleVolume.loadEntity();
            }

            if (GUILayout.Button("Leave Battle"))
            {
 
                battleVolume.NextPalyerFSM();
            }

            if (GUILayout.Button("add Player"))
            {
                BattleVolume battleVolume =Object.FindObjectOfType<BattleVolume>();
                battleVolume.AddRandomPlayerToTeam();
            }

            if (GUILayout.Button("Clear play List"))
            {
                BattleVolume battleVolume =Object.FindObjectOfType<BattleVolume>();
                battleVolume.RemoveTeamList();
            }

            if (GUILayout.Button("next play state"))
            {
                GameEntry.BattleSystem.nextPlayerFsm();
            }



            GUILayout.EndScrollView();
        }
    }
}