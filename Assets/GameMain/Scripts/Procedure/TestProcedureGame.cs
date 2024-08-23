using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using GameFramework.Procedure;
using StarForce;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;
using ProcedureBase = GameFramework.Procedure.ProcedureBase;

public class TestProcedureGame : ProcedureBase
{

    private readonly Dictionary<GameModeH, GameBaseH> m_games = new Dictionary<GameModeH, GameBaseH>();

    private GameBaseH m_CurrentGame = null;
//editor debugger
    // ChangeLanguageDebuggerWindow changeLanguageDebuggerWindow = null;
    protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnInit(procedureOwner);
        m_games.Add(GameModeH.Normal,new NormalGame());
        //add editor
        // changeLanguageDebuggerWindow = new ChangeLanguageDebuggerWindow();
        // GameEntry.Debugger.RegisterDebuggerWindow("Other/ButtonTest",changeLanguageDebuggerWindow);
//debugger
        TestDebugger testDebugger = new TestDebugger();
        GameEntry.Debugger.RegisterDebuggerWindow("Debug/Shop",testDebugger);
        
//event
        // ProductSubEventArgs e = ProductSubEventArgs.Create();
      //  GameEntry.Event.Fire(this,ProductSubEventArgs.Create(testDebugger));
        
    }

    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);
        GameModeH gameMode = (GameModeH)procedureOwner.GetData<VarByte>("GameMode").Value;
        gameMode = 0;//这里是临时改的
        m_CurrentGame = m_games[gameMode];
        m_CurrentGame.Initialize();
        
    }

    protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        m_CurrentGame.Update(elapseSeconds,realElapseSeconds);
    }

    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);
        m_CurrentGame.Shutdown();
    }
}
