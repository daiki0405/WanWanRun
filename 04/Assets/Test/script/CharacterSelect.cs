using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    // キャラクターごとのスプライトを格納する配列
    public Sprite[] characterSprites;

    // 選択されたキャラクターの情報を受け取り、ゲームシーンに遷移するメソッド
    public void SelectCharacter(int characterIndex)
    {
        // 選択されたキャラクターの情報をPlayerPrefsに保存する
        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);
        PlayerPrefs.Save();

        // ゲームシーンに遷移する
        SceneManager.LoadScene("Game");
    }
}