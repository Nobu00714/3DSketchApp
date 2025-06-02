using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public enum State
    {
        BumpUI,
        PieUI,
        Draw
    }
    public State currentState;
    public void stateBumpUI()
    {
        currentState = State.BumpUI;
    }
    public void statePieUI()
    {
        currentState = State.PieUI;
    }
    public void stateDraw()
    {
        currentState = State.Draw;
    }
}
