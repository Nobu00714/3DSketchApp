using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelection : MonoBehaviour
{
    [SerializeField] private StateManager stateManager;
    [SerializeField] private GameObject cursor;
    [SerializeField] private int menuNum = 16;
    public int selectNum;
    public Color32[] itemColors;
    private bool firstRelease;
    public LineDrawer lineDrawer;
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
                if(firstRelease)
                {
                    selectNum = getSelect();
                    lineDrawer.lineColor = itemColors[selectNum];
                    firstRelease = false;
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
