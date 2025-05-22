using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject globalCursor;
    public GameObject menu;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (Vector3.Distance(globalCursor.transform.position, menu.transform.position) < menu.transform.localScale.x * 0.04f)
        {
            this.transform.position = globalCursor.transform.position;
            Debug.Log("Tracking");
        }
        else
        {
            rb.velocity = (globalCursor.transform.position - this.transform.position) / Time.deltaTime * 1.0f;
        }
    }
}
