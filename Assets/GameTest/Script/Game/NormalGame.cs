using System.Collections;
using System.Collections.Generic;
using StarForce;
using Unity.VisualScripting;
using UnityEngine;
using AssetUtility = StarForce.AssetUtility;

public class NormalGame : GameBaseH
{
    private float m_ElapseSeconde=0;

    // Update is called once per frame
    public override GameModeH GameMode
    {
        get
        {
            return GameModeH.Normal;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        
    }

    public override void Shutdown()
    {
        base.Shutdown();
    }

    public override void Update(float elapseSeconds, float realElapseSeconds)
    {
        base.Update(elapseSeconds, realElapseSeconds);
        m_ElapseSeconde += elapseSeconds;
        if (m_ElapseSeconde>1)
        {
            m_ElapseSeconde = 0;

            // mSphereData data = new mSphereData(GameEntry.Entity.GenerateSerialId(), 70005);
            // GameEntry.Entity.ShowMySphere(data);

            if (birthPoints.Length == 0)
            {
                Debug.LogError("brith Points is 0!!!!");
                return;
            }
            
            NpcFSMData data = new NpcFSMData(GameEntry.Entity.GenerateSerialId(), 70006,birthPoints);
            GameEntry.Entity.ShowMyNpc(data);
        }
    }
}
