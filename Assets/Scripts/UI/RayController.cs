using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    [SerializeField] private GameObject IndexTip;
    [SerializeField] private GameObject IndexMiddle;
    [SerializeField] private GameObject menuPlaneObject;
    void Start()
    {
        
    }
    void Update()
    {
        UnityEngine.Vector3 handForward = IndexTip.transform.position - IndexMiddle.transform.position;
        handForward.Normalize();
        UnityEngine.Vector3 handPos = IndexTip.transform.position;

        var n = - menuPlaneObject.transform.forward;
        var x = menuPlaneObject.transform.position;
        var x0 = handPos;
        var m = handForward;
        var h = UnityEngine.Vector3.Dot(n, x);

        var intersectPoint = x0 + (h - UnityEngine.Vector3.Dot(n, x0)) / UnityEngine.Vector3.Dot(n,m) * m;
        
        this.GetComponent<LineRenderer>().SetPosition(0, handPos);
        this.GetComponent<LineRenderer>().SetPosition(1, intersectPoint);
    }
}
