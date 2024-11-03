using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour
{
    public Text scoreText; // UIテキストオブジェクト
    private const string playerScoreKey = "PlayerScore"; // PlayerPrefsに保存するスコアのキー
    private int score = 0; // スコアの初期値

    // Start is called before the first frame update
    void Start()
    {
        LoadScore(); // スコアを読み込む
        UpdateScoreUI(); // UIを更新する
    }

    // スコアをUIに表示するためのメソッド
    private void UpdateScoreUI()
    {
        // スコア表示用のテキストが設定されている場合
        if (scoreText != null)
        {
            scoreText.text = "×" + score.ToString(); // スコアをUIに表示する
        }
    }

    // スコアを読み込むためのメソッド
    private void LoadScore()
    {
        score = PlayerPrefs.GetInt(playerScoreKey, 0); // PlayerPrefsからスコアを読み込む
    }
}