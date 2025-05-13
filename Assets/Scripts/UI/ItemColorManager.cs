using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColorManager : MonoBehaviour
{
    public ColorSelection colorSelection;
    public GameObject[] items;
    void Start()
    {
        for(int i = 0; i < items.Length; i++)
        {
            items[i].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = colorSelection.itemColors[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
