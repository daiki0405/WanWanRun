using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    // シングルトンパターン
    private static GameOverManager _instance;
    public static GameOverManager Instance { get { return _instance; } }
    private GameObject playerObject;


    private void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        // Player タグを持つオブジェクトを検索し、参照を取得
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject == null)
        {
            Debug.LogError("Player object not found!");
        }
    }

    // プレイヤーを再スタートするメソッド
    public void RestartPlayer()
    {
        // Player オブジェクトが存在し、Player コンポーネントがあればアクティブにする
        if (playerObject != null)
        {
            Player playerComponent = playerObject.GetComponent<Player>();
            if (playerComponent != null)
            {
                // _hp を1にリセットする
                playerComponent.SetHP(1);

                playerComponent.enabled = true;
            }
            else
            {
                Debug.LogError("Player component not found on the player object!");
            }
        }
        // BGMを再開する
        BackgroundMusic.Instance.ResumeBGM();

        // タイムスケールを1に戻す
        Time.timeScale = 1f;

        // GameOverPopup UIを検索して非アクティブにする
        GameObject gameOverPopup = GameObject.Find("GameOverPopup");
        if (gameOverPopup != null)
        {
            gameOverPopup.SetActive(false);
        }
        else
        {
            Debug.LogError("GameOverPopup object not found.");
        }

    }


}
