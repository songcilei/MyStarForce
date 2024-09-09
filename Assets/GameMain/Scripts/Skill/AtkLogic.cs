using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUp25 :  IAtkBuffer
{
    public void Execute(PlayerFSM fsm, params object[] parameters)
    {
        BufferState bufferState = new BufferState("AttackUp25",3,fsm.PlayerFsmData.Atk*0.25f,0,0,0);
        fsm.AddBuffer(bufferState);
    }

}

public class AttackUp50 :  IAtkBuffer 
{
    public void Execute(PlayerFSM fsm, params object[] parameters)
    {
        BufferState bufferState = new BufferState("AttackUp50",3,fsm.PlayerFsmData.Atk*0.5f,0,0,0);
        fsm.AddBuffer(bufferState);
    }

}

public class AttackUp75:  IAtkBuffer 
{
    public void Execute(PlayerFSM fsm, params object[] parameters)
    {
        BufferState bufferState = new BufferState("AttackUp75",3,fsm.PlayerFsmData.Atk*0.75f,0,0,0);
        fsm.AddBuffer(bufferState);
    }

}

public class AttackUp100 :  IAtkBuffer 
{
    public void Execute(PlayerFSM fsm, params object[] parameters)
    {
        BufferState bufferState = new BufferState("AttackUp100",3,fsm.PlayerFsmData.Atk,0,0,0);
        fsm.AddBuffer(bufferState);
    }

}
