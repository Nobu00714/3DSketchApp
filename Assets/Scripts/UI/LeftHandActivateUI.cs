using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandActivateUI : MonoBehaviour
{
    public OVRHand leftHand;
    void Update()
    {
        //左手中指でピンチしている時PieUIを表示
        if (leftHand.GetFingerIsPinching(OVRHand.HandFinger.Middle))
        {
            this.GetComponent<StateManager>().statePieUI();
        }
        //左手人差し指でピンチしている時BumpUIを表示
        else if (leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            this.GetComponent<StateManager>().stateBumpUI();
        }
        else
        {
            //どちらの指もピンチしていない時はDraw状態に戻す
            this.GetComponent<StateManager>().stateDraw();
        }
    }
}
