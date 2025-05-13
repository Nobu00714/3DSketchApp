using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject realCursor;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rb.velocity = (realCursor.transform.position - this.transform.position)/Time.deltaTime * 1.0f;
    }
}
