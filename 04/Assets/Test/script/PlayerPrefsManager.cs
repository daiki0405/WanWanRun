using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    private const string obtainedCharactersKey = "ObtainedCharacters";

    public static void ClearObtainedCharacters()
    {
        PlayerPrefs.DeleteKey(obtainedCharactersKey); // キーに関連付けられたデータを削除
        Debug.Log("獲得したキャラクターリストを削除しました");
    }
}
