using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public Text distanceText; // UIテキスト

    private float totalDistanceX = 0f; // Xプラス方向の移動距離を記録する変数
    private Vector3 startPosition; // スタート地点の位置を記録する変数
    public static float maxDistance = 0f; // 最高記録の移動距離
    public static float currentDistance = 0f; // 現在の移動距離

    private void Start()
    {
        // プレイヤーオブジェクトを検索してTransformを取得する
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            // スタート地点の位置をプレイヤーの初期位置に設定
            startPosition = playerObject.transform.position;
        }
        else
        {
            Debug.LogError("Playerタグがついているオブジェクトが見つかりませんでした。");
        }

        // 最高記録をPlayerPrefsから読み込む
        maxDistance = PlayerPrefs.GetFloat("MaxDistance", 0f);
    }

    private void Update()
    {
        // 移動距離を更新してテキストに表示
        UpdateDistanceText();
    }

    // スタート地点からの移動距離を取得するメソッド
    private float GetDistanceFromStart() 
    {
        // プレイヤーオブジェクトを検索してTransformを取得する
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            // 現在位置とスタート地点の差分を計算し、X方向の値を取得
            float distanceX = playerObject.transform.position.x - startPosition.x;
            return distanceX;
        }
        else
        {
            Debug.LogError("Playerタグがついているオブジェクトが見つかりませんでした。");
            return 0f;
        }
    }

    // 移動距離を更新してテキストに表示するメソッド
    private void UpdateDistanceText()
    {
        // 現在の移動距離を計算
        totalDistanceX = GetDistanceFromStart();

        // テキストに移動距離を表示
        if (distanceText != null)
        {
            distanceText.text = " 移動距離\n　" + totalDistanceX.ToString("F1") +"m"; // F1は小数点以下1桁まで表示するためのフォーマット指定
        }
    }

    // ゲームオーバー時の処理
    public void GameOver()
    {
        // 現在の移動距離を記録
        currentDistance = totalDistanceX;

        // 最高記録を更新
        if (currentDistance > maxDistance)
        {
            maxDistance = currentDistance;
            // 以前使用されていたキャラクターの識別子を保存
            PlayerPrefs.SetString("PreviousSelectedCharacter", PlayerPrefs.GetString("SelectedCharacterIdentifier", "DefaultCharacter"));

        }

        // 最高記録をPlayerPrefsに保存
        PlayerPrefs.SetFloat("MaxDistance", maxDistance);
        PlayerPrefs.Save(); // 重要：変更を保存するために必要
    }
}
