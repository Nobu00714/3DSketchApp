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
    private LineRenderer currentLineRenderer;
    private List<GameObject> drawnLineInstances = new List<GameObject>();
    public Color32 lineColor;

    [Tooltip("全体の線の太さの倍率")]
    public float lineWidth = 0.01f;

    [Tooltip("線の太さの変化を定義するカーブ。X軸:線の正規化された長さ(0-1), Y軸:太さの倍率(0-1)")]
    public AnimationCurve lineWidthCurve;

    [Tooltip("新しいポイントを追加する最小距離の閾値")]
    public float minDistanceThreshold = 0.005f;

    void Start()
    {
        lineObjectPrefab = Resources.Load<GameObject>("LinerendererPrefab");

        // インスペクターでlineWidthCurveが設定されていなければ、
        // lineWidthに基づいてデフォルトカーブを生成
        if (lineWidthCurve == null || lineWidthCurve.keys.Length == 0)
        {
            // Start時はlineWidthCurveを一度だけ設定するように変更
            // Update内で線を描き始めるたびにGenerateWidthCurveを呼ぶように変更
            // もしStartで固定のカーブを使いたいなら、ここでのGenerateWidthCurve呼び出しは適切
            // lineWidthCurve = GenerateWidthCurve(lineWidth); // これは元のコードのまま
        }
    }

    void Update()
    {
        if (stateManager.currentState == StateManager.State.Draw)
        {
            var rightIndexPos = rightIndexTip.transform.position;

            if (rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index))
            {
                // ラインインスタンスがないなら生成する
                if (lineInstance == null)
                {
                    lineInstance = Instantiate(lineObjectPrefab, Vector3.zero, Quaternion.identity);
                    lineInstance.transform.parent = this.transform;

                    // ★★★ 生成されたlineInstanceをリストに追加 ★★★
                    drawnLineInstances.Add(lineInstance);

                    currentLineRenderer = lineInstance.GetComponent<LineRenderer>();
                    positionList.Clear();

                    if (currentLineRenderer != null)
                    {
                        // Startで設定したカーブか、インスペクターで設定したカーブを使用
                        // lineWidthCurveがStartでlineWidthに基づいて生成されるなら、そのままで良い
                        // もしlineWidthの変更に動的に追従させたいなら、ここでGenerateWidthCurveを呼ぶ
                        if (lineWidthCurve == null || lineWidthCurve.keys.Length == 0) // フォールバック
                        {
                             currentLineRenderer.widthCurve = GenerateWidthCurve(lineWidth);
                        }
                        else
                        {
                             currentLineRenderer.widthCurve = lineWidthCurve; // インスペクターまたはStartで設定されたもの
                        }
                        currentLineRenderer.widthMultiplier = lineWidth;


                        Renderer rend = lineInstance.GetComponent<Renderer>();
                        if (rend != null)
                        {
                            rend.material.color = lineColor;
                        }

                        positionList.Add(rightIndexPos);
                        currentLineRenderer.positionCount = 1;
                        currentLineRenderer.SetPosition(0, rightIndexPos);
                    }
                }
                else // 既存の線にポイントを追加する場合
                {
                    if (currentLineRenderer != null)
                    {
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
                    // 現在描画中の線は終了したので、次の描画のために参照をクリア
                    // GameObject自体はdrawnLineInstancesリストに残るので、シーンからは消えない
                    lineInstance = null;
                    currentLineRenderer = null;
                }
            }
        }
    }

    /// <summary>
    /// 線の太さ変化をより自然にするためのカーブを生成（端を細くしすぎない）
    /// </summary>
    /// <param name="currentLineWidth">現在のlineWidthに応じて端の細さを調整</param>
    /// <returns>太さのカーブ</returns>
    private AnimationCurve GenerateWidthCurve(float currentLineWidth)
    {
        // 太くするほどエッジ（細くなる部分）を短くする（現在のロジック）
        // lineWidthが大きいほどedgeRatioは小さくなる
        float edgeRatio = Mathf.Clamp(0.5f / Mathf.Max(currentLineWidth, 0.001f), 0.01f, 0.2f); // 0除算を避ける

        return new AnimationCurve(
            new Keyframe(0, 0f),                // 完全に細くしすぎない（0→0.2）
            new Keyframe(edgeRatio, 1f),
            new Keyframe(1f - edgeRatio, 1f),
            new Keyframe(1, 0f)
        );
    }

    // ★★★ 追加: 一番最後に描画/開始された線を削除する関数 ★★★
    public void DeleteLastDrawnLine()
    {
        if (drawnLineInstances.Count > 0)
        {
            // リストの最後の要素（一番最後に生成された線）を取得
            GameObject lineToDelete = drawnLineInstances[drawnLineInstances.Count - 1];
            
            // リストからその参照を削除
            drawnLineInstances.RemoveAt(drawnLineInstances.Count - 1);

            if (lineToDelete != null) // オブジェクトがまだ存在する場合
            {
                // もし削除する線が現在描画中の線 (lineInstance) であれば、
                // lineInstance と currentLineRenderer も null にして描画状態をリセット
                if (lineInstance == lineToDelete)
                {
                    lineInstance = null;
                    currentLineRenderer = null;
                    // positionList は次に新しい線を描き始める (lineInstance == null のブロックに入る) 際にクリアされます。
                }
                Destroy(lineToDelete); // GameObjectをシーンから破棄
                Debug.Log("最後に描画/開始された線を削除しました。残りの線: " + drawnLineInstances.Count);
            }
        }
        else
        {
            Debug.Log("削除できる線がありません。");
        }
    }
}