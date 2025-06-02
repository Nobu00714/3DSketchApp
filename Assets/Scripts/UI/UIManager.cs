using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public StateManager stateManager;
    public GameObject[] activateBumpUI;
    public GameObject[] activatePieUI;
    public GameObject[] bumpMenuUI;
    public GameObject[] pieMenuUI;
    void Update()
    {
        if(stateManager.currentState == StateManager.State.BumpUI)
        {
            // Activate UI elements
            for(int i = 0; i < activateBumpUI.Length; i++)
            {
                activateBumpUI[i].SetActive(true);
            }
        }
        else if(stateManager.currentState == StateManager.State.PieUI)
        {
            // Activate UI elements
            for(int i = 0; i < activatePieUI.Length; i++)
            {
                activatePieUI[i].SetActive(true);
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
