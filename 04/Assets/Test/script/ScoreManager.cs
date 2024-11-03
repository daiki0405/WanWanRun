using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // UIテキストオブジェクト
    private int score = 0; // スコアの初期値

    public int Score // スコアへのアクセスを提供するプロパティ
    {
        get { return score; }
        set { score = value; }
    }

    private const string playerScoreKey = "PlayerScore"; // PlayerPrefsに保存するスコアのキー

    // ゲーム開始時に呼び出される処理
    private void Start()
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

    // スコアを保存するためのメソッド
    private void SaveScore()
    {
        PlayerPrefs.SetInt(playerScoreKey, score); // スコアをPlayerPrefsに保存する
        PlayerPrefs.Save(); // 変更を保存する
    }

    // スコアを読み込むためのメソッド
    private void LoadScore()
    {
        score = PlayerPrefs.GetInt(playerScoreKey, 0); // PlayerPrefsからスコアを読み込む
    }

    // スコアを更新するためのメソッド
    public void UpdateScore()
    {
        score++; // スコアを増やす
        UpdateScoreUI(); // スコアを更新してUIを更新する
        SaveScore(); // スコアを保存する
    }

    // スコアを消費するためのメソッド
    public void UseScore(int amount)
    {
        score -= amount; // スコアを減算する
        UpdateScoreUI(); // スコアを更新してUIを更新する
        SaveScore(); // スコアを保存する
    }

    // スコアを追加するためのメソッド
    public void AddScore(int amount)
    {
        score += amount; // スコアを加算する
        UpdateScoreUI(); // スコアを更新してUIを更新する
        SaveScore(); // スコアを保存する
    }
}