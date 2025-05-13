using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public enum State
    {
        UI,
        Draw
    }
    public State currentState;
    public void stateUI()
    {
        currentState = State.UI;
    }
    public void stateDraw()
    {
        currentState = State.Draw;
    }
}
