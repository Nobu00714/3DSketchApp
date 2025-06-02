using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelection : MonoBehaviour
{
    [SerializeField] private GameObject cursor;
    [SerializeField] private int menuNum = 16;
    private LineDrawer lineDrawer;
    private StateManager stateManager;
    private int selectNum;
    private bool firstRelease;
    public GameObject penSizeAndColor;
    [SerializeField] private SmartColorPalette smartColorPalette;
    private ColorPalette colorPalette;
    [SerializeField] private GameObject[] pieVisuals;
    [SerializeField] private Color32 defaultColor;
    void Start()
    {
        stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
        lineDrawer = GameObject.Find("LineDrawer").GetComponent<LineDrawer>();
        colorPalette = this.GetComponent<ColorPalette>();

    }
    void Update()
    {
        if (stateManager.currentState == StateManager.State.BumpUI || stateManager.currentState == StateManager.State.PieUI)
        {
            // カーソルがアクティブな時
            if (cursor.activeSelf)
            {
                selectNum = getSelect();
                //PieMenuのときは、VisualFeedback
                if (stateManager.currentState == StateManager.State.PieUI)
                {
                    for (int i = 0; i < pieVisuals.Length; i++)
                    {
                        if (i == selectNum)
                        {
                            pieVisuals[i].GetComponent<Renderer>().material.color = Color.red;
                        }
                        else
                        {
                            pieVisuals[i].GetComponent<Renderer>().material.color = Color.white;
                        }
                    }
                }
                firstRelease = true;
            }
            else
            {
                // 離したときに選択、音声、視覚フィードバックなど
                if (firstRelease)
                {
                    selectNum = getSelect();
                    lineDrawer.lineColor = colorPalette.itemColors[selectNum];
                    penSizeAndColor.GetComponent<Renderer>().material.color = colorPalette.itemColors[selectNum];
                    // 選択音を鳴らす
                    this.GetComponent<SelectSound>().PlaySelectSound();
                    // PieVisualFeedbackの初期化
                    if (stateManager.currentState == StateManager.State.PieUI)
                    {
                        if (pieVisuals.Length > 0)
                        {
                            for (int i = 0; i < pieVisuals.Length; i++)
                            {
                                pieVisuals[i].GetComponent<Renderer>().material.color = defaultColor;
                            }
                        }
                    }
                    firstRelease = false;
                    this.GetComponent<ColorSelection>().enabled = false;
                }
            }
        }
    }
    private int getSelect()
    {
        float angle = Vector3.SignedAngle(this.transform.up, cursor.transform.position - this.transform.position, - this.transform.forward);
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
