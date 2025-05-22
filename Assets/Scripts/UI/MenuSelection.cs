using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelection : MonoBehaviour
{
    public StateManager stateManager;
    public OVRHand rightHand;
    public OVRHand leftHand;
    public GameObject selectedMenu;
    public GameObject[] UIMenus;
    public GameObject globalCursor;
    public GameObject bumpCursor;
    private bool firstPinch = true;
    void Update()
    {
        if(stateManager.currentState == StateManager.State.UI)
        {
            //右手でピンチしている時
            if (rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index))
            {
                //ひとつ前のフレームでピンチしていなかったら
                if (firstPinch)
                {
                    firstPinch = false;
                    //UIメニューのどれを選択しているかを調べる
                    for (int i = 0; i < UIMenus.Length; i++)
                    {
                        if (Vector3.Distance(globalCursor.transform.position, UIMenus[i].transform.position) < UIMenus[i].transform.localScale.x * 0.04f)
                        {
                            bumpCursor.transform.position = globalCursor.transform.position;
                            bumpCursor.SetActive(true);
                            selectedMenu = UIMenus[i];
                        }
                    }
                }
            }
            else
            {
                bumpCursor.SetActive(false);
                firstPinch = true;
            }
        }
    }
}
