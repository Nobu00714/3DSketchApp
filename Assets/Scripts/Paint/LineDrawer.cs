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
    private LineRenderer currentLineRenderer; // LineRendererをキャッシュするための変数

    [Tooltip("全体の線の太さの倍率")]
    public float lineWidth = 0.01f;
    public Color32 lineColor;

    [Tooltip("線の太さの変化を定義するカーブ。X軸:線の正規化された長さ(0-1), Y軸:太さの倍率(0-1)")]
    public AnimationCurve lineWidthCurve;

    [Tooltip("新しいポイントを追加する最小距離の閾値")]
    public float minDistanceThreshold = 0.005f; //  <-- この行を追加

    void Start()
    {
        lineObjectPrefab = Resources.Load<GameObject>("LinerendererPrefab"); // Load the prefab from Resources folder

        if (lineWidthCurve == null || lineWidthCurve.keys.Length == 0)
        {
            lineWidthCurve = new AnimationCurve(
                new Keyframe(0, 0, 0, 2),
                new Keyframe(0.05f, 1, 0, 0),
                new Keyframe(0.95f, 1, 0, 0),
                new Keyframe(1, 0, -2, 0)
            );
        }
    }

    void Update()
    {
        if (stateManager.currentState == StateManager.State.Draw)
        {
            var rightIndexPos = rightIndexTip.transform.position;

            if (rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index))
            {
                if (lineInstance == null) // 新しい線を開始する場合
                {
                    lineInstance = Instantiate(lineObjectPrefab, Vector3.zero, Quaternion.identity);
                    lineInstance.transform.parent = this.transform;
                    currentLineRenderer = lineInstance.GetComponent<LineRenderer>();
                    positionList.Clear(); // 新しい線のためにリストをクリア

                    if (currentLineRenderer != null)
                    {
                        // LineRendererの初期設定
                        currentLineRenderer.widthCurve = lineWidthCurve;
                        currentLineRenderer.widthMultiplier = lineWidth;

                        // 色の設定 (LinerendererPrefabのマテリアルに依存)
                        Renderer rend = lineInstance.GetComponent<Renderer>(); // LineRendererと同じGameObjectにある前提
                        if (rend != null)
                        {
                            rend.material.color = lineColor;
                        }
                        // 代替: currentLineRenderer.startColor = lineColor; currentLineRenderer.endColor = lineColor;


                        // 最初のポイントを無条件に追加
                        positionList.Add(rightIndexPos);
                        currentLineRenderer.positionCount = 1;
                        currentLineRenderer.SetPosition(0, rightIndexPos);
                    }
                }
                else // 既存の線にポイントを追加する場合
                {
                    if (currentLineRenderer != null)
                    {
                        // リストが空でなく、最後のポイントが存在する場合のみ距離をチェック
                        if (positionList.Count > 0)
                        {
                            float distance = Vector3.Distance(positionList[positionList.Count - 1], rightIndexPos);
                            if (distance > minDistanceThreshold)
                            {
                                positionList.Add(rightIndexPos);
                                currentLineRenderer.positionCount = positionList.Count;
                                currentLineRenderer.SetPositions(positionList.ToArray());
                            }
                        }
                        else
                        {
                            // lineInstanceが存在するがpositionListが空、という状況は通常考えにくいが、
                            // 安全策として最初のポイントとして追加
                            positionList.Add(rightIndexPos);
                            currentLineRenderer.positionCount = 1;
                            currentLineRenderer.SetPosition(0, rightIndexPos);
                        }
                    }
                }
            }
            else // ピンチしていない場合
            {
                if (lineInstance != null)
                {
                    // 描画を終了し、次のために参照をクリア
                    lineInstance = null;
                    currentLineRenderer = null; // キャッシュしたLineRendererもクリア
                    // positionListは次の描画開始時にクリアされるので、ここでは何もしなくても良い
                }
            }
        }
    }
}