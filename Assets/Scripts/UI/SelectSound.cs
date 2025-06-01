using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSound : MonoBehaviour
{
    [SerializeField] private AudioClip selectSound;
    public void PlaySelectSound()
    {
        AudioSource.PlayClipAtPoint(selectSound, transform.position);
    }
}
