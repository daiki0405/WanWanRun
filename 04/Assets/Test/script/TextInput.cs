using UnityEngine;
using TMPro;

public class TextInput : MonoBehaviour
{
    public TMP_InputField[] inputFields; // 保存する入力フィールドの配列
    public string[] playerNameKeys; // 各入力フィールドの名前キー

    void Start()
    {
        // 各入力フィールドごとに保存された名前を読み込む
        for (int i = 0; i < inputFields.Length; i++)
        {
            if (PlayerPrefs.HasKey(playerNameKeys[i]))
            {
                string savedName = PlayerPrefs.GetString(playerNameKeys[i]);
                inputFields[i].text = savedName;
                Debug.Log("Name loaded (Input Field " + (i + 1) + "): " + savedName);
            }
            else
            {
                Debug.Log("No saved name found for Input Field " + (i + 1));
            }
        }
    }

    // 各入力フィールドの内容を保存するメソッド
    public void SaveName(int inputFieldIndex)
    {
        if (inputFieldIndex >= 0 && inputFieldIndex < inputFields.Length)
        {
            string playerName = inputFields[inputFieldIndex].text;

            // 入力された名前を保存
            PlayerPrefs.SetString(playerNameKeys[inputFieldIndex], playerName);
            PlayerPrefs.Save();

            Debug.Log("Name saved (Input Field " + (inputFieldIndex + 1) + "): " + playerName);
        }
        else
        {
            Debug.LogWarning("Invalid input field index: " + inputFieldIndex);
        }
    }
}
