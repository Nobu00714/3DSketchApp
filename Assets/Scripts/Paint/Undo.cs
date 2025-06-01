using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undo : MonoBehaviour
{
    [SerializeField] private OVRHand rightHand;
    private StateManager stateManager;
    private bool firstPinch = true;
    private LineDrawer lineDrawer;
    void Start()
    {
        stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
        lineDrawer = this.GetComponent<LineDrawer>();
    }
    void Update()
    {
        if (stateManager.currentState == StateManager.State.Draw)
        {
            if (rightHand.GetFingerIsPinching(OVRHand.HandFinger.Middle))
            {
                if (firstPinch)
                {
                    firstPinch = false;
                    // Undo action
                    lineDrawer.DeleteLastDrawnLine();
                }
            }
            else
            {
                // Reset the first pinch flag when the pinch is released
                firstPinch = true;
            }
        }
    }
}
