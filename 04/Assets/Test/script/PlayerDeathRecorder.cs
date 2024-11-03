using UnityEngine;

public class PlayerDeathRecorder : MonoBehaviour
{
    private Vector3 deathPosition; // プレイヤーが死亡した位置

    // プレイヤーの死亡処理
    public void Die()
    {
        // プレイヤーが死亡した位置を記録
        deathPosition = transform.position;
        // プレイヤーオブジェクトを非アクティブにする
        gameObject.SetActive(false);
    }

    // 死亡位置を取得するメソッド
    public Vector3 GetDeathPosition()
    {
        return deathPosition;
    }
}
