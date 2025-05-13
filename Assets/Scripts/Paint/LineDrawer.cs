using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public StateManager stateManager;
    public OVRHand rightHand;
    public OVRHand leftHand;
    public GameObject rightIndexTip;
    public GameObject leftIndexTip;
    private GameObject lineObjectPrefab;
    private List<Vector3> positionList = new List<Vector3>();
    private GameObject lineInstance;
    public float linewidth = 0.01f;
    public Color32 lineColor;
    void Start()
    {
        lineObjectPrefab = Resources.Load<GameObject>("LinerendererPrefab"); // Load the prefab from Resources folder
    }
    void Update()
    {
        //現在の状態がDrawの時のみ描画する
        if(stateManager.currentState == StateManager.State.Draw)
        {
            //右手の人差し指の腹の座標を取得
            var rightIndexPos = rightIndexTip.transform.position;
            //右手でピンチしている時
            if(rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index))
            {
                //参照中のラインインスタンスがない場合は作る
                if(lineInstance == null)
                {
                    lineInstance = (GameObject)Instantiate(lineObjectPrefab, Vector3.zero, Quaternion.identity);
                    lineInstance.transform.parent = this.transform; // Set the parent to this object
                }
                //描画するラインの座標を追加
                positionList.Add(rightIndexPos);
                LineRenderer lineRenderer = lineInstance.GetComponent<LineRenderer>();
                lineRenderer.positionCount = positionList.Count;
                lineRenderer.SetPositions(positionList.ToArray());
                lineRenderer.startWidth = linewidth;
                lineRenderer.endWidth = linewidth;
                lineInstance.GetComponent<Renderer>().material.color = lineColor;
            }
            else
            {
                //ラインインスタンスの参照と座標のリストを削除
                lineInstance = null;
                positionList.Clear();
            }
        }
    }
}
