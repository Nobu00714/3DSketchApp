using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidthSelection : MonoBehaviour
{
    [SerializeField] private GameObject cursor;
    [SerializeField] private int menuNum = 16;
    [SerializeField] public float[] penWidths;
    private LineDrawer lineDrawer;
    private StateManager stateManager;
    private int selectNum;
    private bool firstRelease;
    public GameObject penSizeAndColor;
    void Start()
    {
        stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
        lineDrawer = GameObject.Find("LineDrawer").GetComponent<LineDrawer>();
    }
    void Update()
    {
        if(stateManager.currentState == StateManager.State.UI)
        {
            if(cursor.activeSelf)
            {
                selectNum = getSelect();
                firstRelease = true;
            }
            else
            {
                if (firstRelease)
                {
                    selectNum = getSelect();
                    lineDrawer.lineWidth = penWidths[selectNum] * 0.01f;
                    penSizeAndColor.transform.localScale = new Vector3(penWidths[selectNum] * 0.01f, penWidths[selectNum] * 0.01f, penWidths[selectNum] * 0.001f);
                    firstRelease = false;
                    this.GetComponent<WidthSelection>().enabled = false;
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
