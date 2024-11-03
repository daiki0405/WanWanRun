using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SeiectedCharacter : MonoBehaviour
{
    // シーンの名前
    public string SceneName = "CharacterSelect";

    // CharacterName UIとCharacterImage UIへの参照
    public TMP_Text characterNameText;
    public Image characterImage;

    // ボタンがクリックされたときの処理
    public void OnButtonClick()
    {
        // イベントに登録
        SceneManager.sceneLoaded += GameSceneLoaded;

        // CharacterSelectionシーンに遷移
        SceneManager.LoadScene(SceneName);
    }

    // ゲームシーンがロードされたときの処理
    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // CharacterSelectionシーンから選択されたキャラクターの情報を取得する
        string selectedCharacterName = PlayerPrefs.GetString("SelectedCharacterName", "No Name");
        Sprite selectedCharacterSprite = Resources.Load<Sprite>("CharacterSprites/" + selectedCharacterName);

        // CharacterName UIにキャラクター名を表示する
        characterNameText.text = selectedCharacterName;

        // CharacterImage UIにキャラクターの画像を表示する
        if (selectedCharacterSprite != null)
        {
            characterImage.sprite = selectedCharacterSprite;
        }
        else
        {
            Debug.LogError("Character sprite not found for character: " + selectedCharacterName);
        }

        // イベントから削除
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}
