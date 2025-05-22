using UnityEngine;

public class SpawnObjectOnHit : MonoBehaviour
{
    [Header("生成するオブジェクト")]
    [Tooltip("衝突地点に生成するオブジェクトのプレハブを指定してください。")]
    public GameObject objectToSpawnPrefab;

    [Header("生成時の設定")]
    [Tooltip("生成するオブジェクトの向きを衝突面の法線に合わせるか。オフの場合は(0,0,0)の回転で生成されます。")]
    public bool alignToSurfaceNormal = true;

    // このメソッドは、このコライダー/リジッドボディが他のコライダー/リジッドボディとの衝突を開始したときに呼び出されます。
    void OnCollisionEnter(Collision collision)
    {
        // 生成するプレハブが設定されているか確認
        if (objectToSpawnPrefab == null)
        {
            Debug.LogWarning("生成するオブジェクトのプレハブが設定されていません。", this);
            return;
        }

        // 衝突情報から最初の接触点を取得
        if (collision.contacts.Length > 0)
        {
            ContactPoint contactPoint = collision.contacts[0]; // 最初の接触情報を取得

            // 接触点のワールド座標
            Vector3 spawnPosition = contactPoint.point;

            // 生成するオブジェクトの回転
            Quaternion spawnRotation;
            if (alignToSurfaceNormal)
            {
                // 接触面の法線ベクトルから回転を生成 (オブジェクトの「上向き」が法線方向を向くように)
                // LookRotationの第二引数でアップベクトルを指定するとより安定することがあります。
                spawnRotation = Quaternion.LookRotation(contactPoint.normal);
            }
            else
            {
                // デフォルトの回転 (0,0,0)
                spawnRotation = Quaternion.identity;
            }

            // 衝突地点にオブジェクトを生成
            GameObject spawnedObject = Instantiate(objectToSpawnPrefab, spawnPosition, spawnRotation);

            // (任意) 生成したオブジェクトに何か処理を加えたい場合はここで行う
            //例: Debug.Log(spawnedObject.name + " を " + spawnPosition + " に生成しました。");
        }
        else
        {
            // 通常、OnCollisionEnterが呼ばれる時点でcontactsは空ではないはずですが、念のため
            Debug.LogWarning("衝突情報に接触点が含まれていませんでした。", this);
        }
    }

    // (任意) 衝突地点や法線を視覚的にデバッグ表示したい場合
    /*
    void OnDrawGizmosSelected()
    {
        // このスクリプトがアタッチされたオブジェクトが選択されているときにのみGizmoを描画
        // 最後に衝突した点の情報を保持しておく必要があるため、この例では不完全です。
        // 衝突のたびに情報を保存し、それを描画するなどの工夫が必要です。
    }
    */
}