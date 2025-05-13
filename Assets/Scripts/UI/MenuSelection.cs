using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelection : MonoBehaviour
{
    public StateManager stateManager;
    public OVRHand rightHand;
    public OVRHand leftHand;
    public GameObject globalCursor;
    public GameObject colorMenu;
    public GameObject colorMenuCursor;
    void Update()
    {
        if(stateManager.currentState == StateManager.State.UI)
        {
            //右手でピンチしている時
            if(rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index))
            {
                if(Vector3.Distance(globalCursor.transform.position, colorMenu.transform.position) < colorMenu.transform.localScale.x * 0.05f)
                {
                    colorMenuCursor.transform.position = globalCursor.transform.position;
                    colorMenuCursor.SetActive(true);
                }
            }
            else
            {
                colorMenuCursor.SetActive(false);
            }
        }
    }
}
