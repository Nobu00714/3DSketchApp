using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public StateManager stateManager;
    public GameObject[] activateUI;
    void Update()
    {
        if(stateManager.currentState == StateManager.State.UI)
        {
            // Activate UI elements
            for(int i = 0; i < activateUI.Length; i++)
            {
                activateUI[i].SetActive(true);
            }
        }
        else
        {
            // Deactivate UI elements
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
