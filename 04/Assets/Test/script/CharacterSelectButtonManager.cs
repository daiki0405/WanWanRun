using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterSelectButtonManager : MonoBehaviour
{
    public List<Button> characterButtons; // キャラクターセレクトボタンのリスト

    private void Start()
    {
        // PlayerPrefsから獲得したキャラクターリストを読み込む
        LoadObtainedCharacters();
    }

    // PlayerPrefsから獲得したキャラクターリストを読み込む
    private void LoadObtainedCharacters()
    {
        Debug.Log("LoadObtainedCharactersメソッドが呼ばれました");

        foreach (Button button in characterButtons)
        {
            // ボタンのテキスト（キャラクター名）を取得
            string characterName = button.gameObject.name;

            // PlayerPrefsにキャラクターが含まれているかチェックし、アクティブにする
            if (PlayerPrefs.HasKey("ObtainedCharacters") && PlayerPrefs.GetString("ObtainedCharacters").Contains(characterName))
            {
                button.gameObject.SetActive(true);
                Debug.Log(characterName + "のボタンをアクティブにしました。");
            }
            else
            {
                button.gameObject.SetActive(false);
                Debug.Log(characterName + "のボタンを非アクティブにしました。");
            }
        }
    }

    // ボタンをアクティブにする
    public void SetActiveButton(string characterName)
    {
        Debug.Log("SetActiveButtonメソッドが呼ばれました。キャラクター名：" + characterName);

        foreach (Button button in characterButtons)
        {
            // ボタンのテキスト（キャラクター名）と引数で渡されたキャラクター名が一致したらアクティブにする
            if (button.GetComponentInChildren<Text>().text == characterName)
            {
                button.gameObject.SetActive(true);
                Debug.Log(characterName + "のボタンをアクティブにしました。");
                break;
            }
        }
    }
}
