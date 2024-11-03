using UnityEngine;
using System.Collections;
using TMPro;

public class GameOverPopup : MonoBehaviour
{
    public static GameOverPopup instance;
    private GameObject playerObject; // Playerオブジェクトへの参照
    public GameObject gameOverPopup; // ゲームオーバーポップアップの参照
    public TextMeshProUGUI distanceText; // テキストメッシュプロ

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1.0f); // 1秒待機

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Playerオブジェクトを検索し、参照を取得
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject == null)
        {
            Debug.LogError("Playerオブジェクトが見つかりませんでした。");
        }
    }

    // ゲームオーバー条件のチェック
    public void CheckGameOverCondition()
    {
        Debug.Log("CheckGameOverCondition method is called."); // メソッドが呼び出されたことをデバッグログで表示

        if (playerObject != null)
        {
            // PlayerオブジェクトからPlayerコンポーネントを取得
            Player player = playerObject.GetComponent<Player>();

            // Playerコンポーネントが取得できた場合、体力が0以下かどうかをチェックし、条件を満たした場合はゲームオーバーポップアップを表示する
            if (player != null && player.GetHP() <= 0)
            {
                ShowGameOverPopup();
            }
        }
        else
        {
            Debug.LogError("Playerオブジェクトの参照がありません。");
        }
    }


    // ゲームオーバーポップアップを表示する関数
    private void ShowGameOverPopup()
    {
        // デバッグメッセージを表示
        Debug.Log("Game Over Popup will be shown.");

        // BGMを停止する
        BackgroundMusic.Instance.StopBGM();

        // ゲームオーバーサウンドを再生
        SoundManager.Instance.PlayGameOverSound();

        // ゲームオーバーポップアップをアクティブにする
        gameOverPopup.SetActive(true);

        // CharacterMovementクラスのGameOverメソッドを呼び出す
        CharacterMovement characterMovement = FindObjectOfType<CharacterMovement>();
        if (characterMovement != null)
        {
            characterMovement.GameOver();

            // 現在の移動距離を取得してテキストに表示
            float currentDistance = CharacterMovement.currentDistance;
            float maxDistance = PlayerPrefs.GetFloat("MaxDistance", 0f); // PlayerPrefsから最高記録を取得
            if (distanceText != null)
            {
                distanceText.text = "\n 今回の記録：" + currentDistance.ToString("F1") + "m\n最高記録：" + maxDistance.ToString("F1") + "m";
            }
        }
        else
        {
            Debug.LogError("CharacterMovementクラスが見つかりませんでした。");
        }
    }
}
