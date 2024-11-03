using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private ScoreManager scoreManager; // スコアを管理するスクリプトへの参照

    private void Start()
    {
        // ゲームオブジェクトからScoreManagerスクリプトへの参照を取得する
        scoreManager = FindObjectOfType<ScoreManager>();

        // もしScoreManagerが見つからなかった場合は警告を表示する
        if (scoreManager == null)
        {
            Debug.LogWarning("ScoreManager not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 衝突したオブジェクトがOyatuタグを持つ場合
        if (collision.CompareTag("Oyatu"))
        {
            // 衝突したオブジェクトを破棄する
            Destroy(collision.gameObject);

            // 効果音を再生
            SoundManager oyatuSound = FindObjectOfType<SoundManager>();
            if (oyatuSound != null)
            {
                oyatuSound.PlayOyatuDestroySound();
            }

            // スコアを更新する
            if (scoreManager != null)
            {
                scoreManager.UpdateScore();
            }
        }
    }

}