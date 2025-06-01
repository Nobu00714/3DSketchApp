using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelection : MonoBehaviour
{
    private StateManager stateManager;
    private UIManager uiManager;
    public RuleBaseAudioManager ruleBaseAudioManager;
    public OVRHand rightHand;
    public OVRHand leftHand;
    public GameObject selectedMenu;
    public GameObject globalCursor;
    public GameObject bumpCursor;
    private bool firstPinch = true;

    void Start()
    {
        stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
        uiManager = this.GetComponent<UIManager>();
    }
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
                    for (int i = 0; i < uiManager.menuUI.Length; i++)
                    {
                        if (Vector3.Distance(globalCursor.transform.position, uiManager.menuUI[i].transform.position) < uiManager.menuUI[i].transform.localScale.x * 0.04f)
                        {
                            // カーソルの位置リセットして表示
                            bumpCursor.transform.position = globalCursor.transform.position;
                            bumpCursor.SetActive(true);
                            // 選択したメニューを指定してSelectionスクリプトを有効化
                            selectedMenu = uiManager.menuUI[i];
                            if(selectedMenu.GetComponent<ColorSelection>() != null)
                            {
                                selectedMenu.GetComponent<ColorSelection>().enabled = true;
                            }
                            if(selectedMenu.GetComponent<WidthSelection>() != null)
                            {
                                selectedMenu.GetComponent<WidthSelection>().enabled = true;
                            } 
                            if(selectedMenu.GetComponent<BrightnessSelection>() != null)
                            {
                                selectedMenu.GetComponent<BrightnessSelection>().enabled = true;
                            }   
                            if(selectedMenu.GetComponent<HueSelection>() != null)
                            {
                                selectedMenu.GetComponent<HueSelection>().enabled = true;
                            }  
                            // 音声フィードバックを有効化
                            ruleBaseAudioManager.audioTF = true;
                        }
                    }
                }
            }
            else    // 右手のピンチが解除されたら
            {
                // カーソルを非表示
                bumpCursor.SetActive(false);
                // 音声フィードバックを無効化
                ruleBaseAudioManager.audioTF = false;
                firstPinch = true;
            }
        }
    }
}
