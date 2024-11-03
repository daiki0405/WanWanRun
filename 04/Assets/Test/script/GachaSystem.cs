using UnityEngine;
using System.Collections.Generic;

public class GachaSystem : MonoBehaviour
{
    public List<GameObject> characterPrefabs; // ガチャで入手できるキャラクターのプレハブのリスト
    public List<GameObject> obtainedCharacters; // ガチャで獲得したキャラクターのプレハブのリスト
    public Transform spawnPoint; // キャラクターの生成位置
    public GachaPopup gachaPopup; // ポップアップを管理するスクリプトの参照
    public CharacterSelectButtonManager buttonManager; // CharacterSelectButtonManagerのインスタンスを格納する変数

    // PlayerPrefsでキャラクターリストを保存するためのキー
    private const string obtainedCharactersKey = "ObtainedCharacters";

    private void Start()
    {
        Debug.Log("Startメソッドが呼ばれました");

        // ゲームを再開した際に保存されたキャラクターリストを復元する
        LoadObtainedCharacters();

        // CharacterSelectButtonManager ゲームオブジェクトを検索して参照を取得する
        buttonManager = GameObject.FindObjectOfType<CharacterSelectButtonManager>();
        if (buttonManager == null)
        {
            Debug.LogError("CharacterSelectButtonManager スクリプトが見つかりませんでした。");
        }

        // GachaPopup Manager オブジェクトをアクティブにする
        gachaPopup.gameObject.SetActive(true);

        // ポップアップを非表示にする
        gachaPopup.popupPanel.SetActive(false);

        Debug.Log("Startメソッドが終了しました");
    }

    public void SpinGacha()
    {
        // キャラクターリストからランダムにキャラクタープレハブを選択する
        int randomIndex = Random.Range(0, characterPrefabs.Count);
        GameObject selectedCharacterPrefab = characterPrefabs[randomIndex];

        // ガチャでキャラクターを獲得した場合は、獲得リストに追加する
        if (!obtainedCharacters.Contains(selectedCharacterPrefab))
        {
            obtainedCharacters.Add(selectedCharacterPrefab);
            Debug.Log("ガチャでキャラクターを獲得しました：" + selectedCharacterPrefab.name);

            // キャラクターセレクトシーンのボタンUIをアクティブにする
            if (buttonManager != null)
            {
                buttonManager.SetActiveButton(selectedCharacterPrefab.name);
            }
        }
        else
        {
            Debug.Log("すでに獲得済みのキャラクターです：" + selectedCharacterPrefab.name);
        }

        // ポップアップに獲得したキャラクター名を表示する
        gachaPopup.ShowGachaResult(selectedCharacterPrefab.name);

        // 獲得したキャラクタープレハブを生成する
        GameObject spawnedCharacter = Instantiate(selectedCharacterPrefab, spawnPoint.position, Quaternion.identity);
        spawnedCharacter.name = selectedCharacterPrefab.name; // プレハブの名前を設定する

        // 獲得したキャラクターリストを保存する
        SaveObtainedCharacters();
    }

    // 保存されたキャラクターリストを読み込む
    private void LoadObtainedCharacters()
    {
        Debug.Log("LoadObtainedCharactersメソッドが呼ばれました");

        if (PlayerPrefs.HasKey(obtainedCharactersKey))
        {
            // PlayerPrefsから保存された文字列を取得
            string characterListString = PlayerPrefs.GetString(obtainedCharactersKey);
            Debug.Log("保存されたキャラクターリスト：" + characterListString);

            // 文字列を分割してキャラクターリストに変換
            string[] characterNames = characterListString.Split(',');

            // キャラクターのプレハブを読み込んでリストに追加する
            foreach (string characterName in characterNames)
            {
                GameObject characterPrefab = Resources.Load<GameObject>("CharacterSprites/" + characterName);
                if (characterPrefab != null)
                {
                    obtainedCharacters.Add(characterPrefab);
                }
                else
                {
                    Debug.LogError("プレハブが見つかりません：" + characterName);
                }
            }

            // 読み込んだキャラクターリストを確認
            Debug.Log("読み込んだキャラクターリスト：");
            foreach (GameObject characterPrefab in obtainedCharacters)
            {
                Debug.Log(characterPrefab.name);
            }
        }
        else
        {
            Debug.Log("保存されたキャラクターリストが見つかりませんでした。");
        }

        Debug.Log("LoadObtainedCharactersメソッドが終了しました");
    }

    // ガチャで獲得したキャラクターリストをPlayerPrefsに保存する
    private void SaveObtainedCharacters()
    {
        Debug.Log("SaveObtainedCharactersメソッドが呼ばれました");

        List<string> characterNames = new List<string>();
        foreach (GameObject characterPrefab in obtainedCharacters)
        {
            characterNames.Add(characterPrefab.name);
        }
        string charactersString = string.Join(",", characterNames.ToArray());
        PlayerPrefs.SetString(obtainedCharactersKey, charactersString);
        PlayerPrefs.Save();
        Debug.Log("キャラクターリストを保存しました：" + charactersString);

        Debug.Log("SaveObtainedCharactersメソッドが終了しました");
    }
}
