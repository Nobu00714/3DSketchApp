using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingUILeftWrist : MonoBehaviour
{
    private UIManager uiManager;
    public OVRSkeleton leftHandSkeleton;
    public Transform UIParent; // UIのTransform
    public OVRSkeleton.BoneId followBone = OVRSkeleton.BoneId.Hand_WristRoot;
    public Transform hmdTransform;   // ユーザーのHMD（通常はCenterEyeAnchor）
    void Start()
    {
        uiManager = this.GetComponent<UIManager>();
    }
    void Update()
    {
        if (leftHandSkeleton.IsDataValid && leftHandSkeleton.IsDataHighConfidence)
        {
            foreach (var bone in leftHandSkeleton.Bones)
            {
                if (bone.Id == followBone)
                {
                    UIParent.position = bone.Transform.position  + new Vector3(+ 0.225f, 0, 0);
                    Vector3 lookDirection = hmdTransform.position - UIParent.position;
                    UIParent.rotation = Quaternion.LookRotation(-lookDirection);                 
                }
            }
        }
    }
}
