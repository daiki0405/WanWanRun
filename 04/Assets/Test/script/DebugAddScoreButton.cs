using UnityEngine;

public class DebugAddScoreButton : MonoBehaviour
{
    public ScoreManager scoreManager; // スコアを管理するスクリプトへの参照

    // ボタンを押したときに呼び出される処理
    public void AddScore()
    {
        scoreManager.AddScore(1000); // スコアを1000追加するメソッドを呼び出す
    }
}