using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueSelection : MonoBehaviour
{
    [SerializeField] private GameObject cursor;
    [SerializeField] private int menuNum = 16;
    private ColorPalette colorPalette;
    private LineDrawer lineDrawer;
    private StateManager stateManager;
    private int selectNum;
    private bool firstRelease;
    public GameObject penSizeAndColor;
    [SerializeField] private SmartColorPalette smartColorPalette;
    void Start()
    {
        stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
        lineDrawer = GameObject.Find("LineDrawer").GetComponent<LineDrawer>();
        colorPalette = this.GetComponent<ColorPalette>();
    }
    void Update()
    {
        if (stateManager.currentState == StateManager.State.UI)
        {
            if (cursor.activeSelf)
            {
                selectNum = getSelect();
                firstRelease = true;
            }
            else
            {
                if (firstRelease)
                {
                    selectNum = getSelect();
                    lineDrawer.lineColor = colorPalette.itemColors[selectNum];
                    penSizeAndColor.GetComponent<Renderer>().material.color = colorPalette.itemColors[selectNum];
                    smartColorPalette.GetBrightnessVariations(colorPalette.itemColors[selectNum]);
                    // 選択音を鳴らす
                    this.GetComponent<SelectSound>().PlaySelectSound();
                    firstRelease = false;
                    this.GetComponent<HueSelection>().enabled = false;
                }
            }
        }
    }
    private int getSelect()
    {
        float angle = Vector3.SignedAngle(this.transform.up, cursor.transform.position - this.transform.position, -this.transform.forward);
        if(angle>=360f)
        {
            angle -= 360f;
        }
        if(angle<=0f)
        {
            angle += 360f;
        }
        int select = (int)(angle/(360f/(float)menuNum));
        return select;
    }
}
