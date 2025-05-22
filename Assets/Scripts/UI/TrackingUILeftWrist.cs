using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingUILeftWrist : MonoBehaviour
{
    public OVRSkeleton leftHandSkeleton;
    public Transform UIParent; // UIのTransform
    public Transform[] menuUIs;   // メニューUIのTransform
    public OVRSkeleton.BoneId followBone = OVRSkeleton.BoneId.Hand_WristRoot;
    public Transform hmdTransform;   // ユーザーのHMD（通常はCenterEyeAnchor）
    void Update()
    {
        if (leftHandSkeleton.IsDataValid && leftHandSkeleton.IsDataHighConfidence)
        {
            foreach (var bone in leftHandSkeleton.Bones)
            {
                if (bone.Id == followBone)
                {
                    UIParent.position = bone.Transform.position;
                    
                    for(int i = 0; i < menuUIs.Length; i++)
                    {
                        Vector3 lookDirection = hmdTransform.position - menuUIs[i].position;
                        if (lookDirection.sqrMagnitude > 0.001f)
                        {
                            menuUIs[i].rotation = Quaternion.LookRotation(lookDirection);
                        }
                    }                    
                }
            }
        }
    }
}
