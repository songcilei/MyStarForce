using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    void Execute(PlayerFSM fsm,params object[] parameters);
}

public interface IAtkBuffer : IAction
{
}

public interface IDefBuffer : IAction
{
}

public interface ISpdBuffer : IAction
{
}

public interface IMag : IAction
{
}

public interface IRestore : IAction
{
}