using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterButton : MonoBehaviour
{
    public string characterIdentifier; // キャラクターの識別子
    public CharacterSelectionUI characterSelectionUI; // CharacterSelectionUI の参照を保持する変数


    // ボタンが押されたときの処理
    public void OnButtonClick()
    {
        // 選択されたキャラクターの識別子を PlayerPrefs に保存する
        PlayerPrefs.SetString("SelectedCharacterIdentifier", characterIdentifier);
        // CharacterSelectionUI のメソッドを呼び出して UI を更新する
        characterSelectionUI.UpdateCharacterUI(characterIdentifier);

    }
}


