using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHitSE : MonoBehaviour
{
    [SerializeField] private AudioClip HitAudio;
    [SerializeField] private MenuSelection menuSelection;
    [SerializeField] private GameObject globalCursor;
    private bool firstHit = true;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Cursor") && firstHit)
        {
            //カーソルがメニュー中心から十分遠いのなら音を鳴らす
            if (Vector3.Distance(globalCursor.transform.position, menuSelection.selectedMenu.transform.position) > menuSelection.selectedMenu.transform.localScale.x * 0.03f)
            {
                AudioSource.PlayClipAtPoint(HitAudio, transform.position);
            }
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Cursor"))
        {
            firstHit = true;
        }
    }
}