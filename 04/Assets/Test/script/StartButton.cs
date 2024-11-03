using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public float delay = 0.01f; // シーン切り替えの遅延時間（秒）
    public BackgroundMusic bgm; // BackgroundMusicスクリプトへの参照

    public void StartGame()
    {
        // シーン切り替えの遅延時間を待機してからシーンを切り替える
        Invoke("LoadStartGameScene", delay);
    }

    // CharacterSerectシーンを読み込むメソッド
    private void LoadStartGameScene()
    {
        SceneManager.LoadScene("Game"); // スタートボタンが押されたらキャラクターセレクトに移動する

    }
}