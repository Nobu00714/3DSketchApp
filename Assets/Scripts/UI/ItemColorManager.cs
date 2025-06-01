using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColorManager : MonoBehaviour
{
    public ColorSelection colorSelection;
    public BrightnessSelection brightnessSelection;
    public HueSelection hueSelection;
    public ColorPalette colorPalette;
    public GameObject[] items;
    void Update()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = colorPalette.itemColors[i];
        }
        // if (colorSelection != null)
        // {
        //     for (int i = 0; i < items.Length; i++)
        //     {
        //         items[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = colorPalette.itemColors[i];
        //     }
        // }
        // if (brightnessSelection != null)
        // {
        //     for (int i = 0; i < items.Length; i++)
        //     {
        //         items[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = colorPalette.itemColors[i];
        //     }
        // }
        // if (hueSelection != null)
        // {
        //     for (int i = 0; i < items.Length; i++)
        //     {
        //         items[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = hueSelection.itemColors[i];
        //     }
        // }
        
    }
}
