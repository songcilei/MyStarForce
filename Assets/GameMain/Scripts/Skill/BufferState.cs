using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferState
{
    public string m_BufferName;
    public int m_BufferLife = 0;
    public int m_atk = 0;
    public int m_def = 0;
    public float m_spd = 0;
    public float m_poison = 0;
    public BufferState(string bufferName,int bufferLife, float atk,float def,float spd,float poison)
    {
        m_BufferName = bufferName;
        m_BufferLife = bufferLife;
        m_atk = (int)atk;
        m_def = (int)def;
        m_spd = spd;
        m_poison = poison;
    }
}
