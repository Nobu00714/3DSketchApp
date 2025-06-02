using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public GameObject globalCursor;
    private Rigidbody rb;
    private MenuSelection menuSelection;
    private StateManager stateManager;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        menuSelection = GameObject.Find("UI").GetComponent<MenuSelection>();
        stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
    }
    void FixedUpdate()
    {
        if (stateManager.currentState == StateManager.State.BumpUI || stateManager.currentState == StateManager.State.PieUI)
        {
            if (menuSelection.selectedMenu == null)
            {
                return; // 選択されていない場合は何もしない
            }
            else
            {
                if (Vector3.Distance(globalCursor.transform.position, menuSelection.selectedMenu.transform.position) < menuSelection.selectedMenu.transform.localScale.x * 0.04f)
                {
                    this.transform.position = globalCursor.transform.position;
                }
                else
                {
                    rb.velocity = (globalCursor.transform.position - this.transform.position) / Time.deltaTime * 1.0f;
                }    
            }
        }

    }
}
