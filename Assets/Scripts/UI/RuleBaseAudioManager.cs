using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleBaseAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip HitAudio;
    [SerializeField] private MenuSelection menuSelection;
    [SerializeField] private GameObject globalCursor;
    private float nowDistance;
    private float previousDistance;
    private bool isPenetrated = false;
    private int penetrateArea;
    private float nowAngle;
    private float previousAngle;
    public bool audioTF = false;
    void Update()
    {
        if(audioTF == false)
        {
            return;
        }
        nowDistance = Vector3.Distance(globalCursor.transform.position, menuSelection.selectedMenu.transform.position);
        nowAngle = Vector3.SignedAngle(menuSelection.selectedMenu.transform.up, globalCursor.transform.position - menuSelection.selectedMenu.transform.position, -menuSelection.selectedMenu.transform.forward);
        if (nowAngle >= 360f)
        {
            nowAngle -= 360f;
        }
        if (nowAngle <= 0f)
        {
            nowAngle += 360f;
        }

        if (!isPenetrated && nowDistance > menuSelection.selectedMenu.transform.localScale.x * 0.05f && previousDistance <= menuSelection.selectedMenu.transform.localScale.x * 0.05f)
        {
            isPenetrated = true;
            AudioSource.PlayClipAtPoint(HitAudio, transform.position);
            penetrateArea = (int)(nowAngle / 45f);
        }

        if (isPenetrated)
        {
            if (nowAngle <= penetrateArea * 45f && previousAngle > penetrateArea * 45f)
            {
                AudioSource.PlayClipAtPoint(HitAudio, transform.position);
            }
            if (nowAngle >= penetrateArea * 45f + 45f && previousAngle < penetrateArea * 45f + 45f)
            {
                AudioSource.PlayClipAtPoint(HitAudio, transform.position);
            }
        }


        if (nowDistance <= menuSelection.selectedMenu.transform.localScale.x * 0.05f)
        {
            isPenetrated = false;
        }

        previousDistance = nowDistance;
        previousAngle = nowAngle;
    }
}
