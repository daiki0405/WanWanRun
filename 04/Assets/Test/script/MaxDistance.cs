using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaxDistance : MonoBehaviour
{
    public TMP_Text maxDistanceText; // 最高記録を表示するUIテキスト
    public Image characterImage; // 表示するキャラクターのイメージ

    private void Start()
    {
        // タイトル画面のUIテキストに最高記録を表示する
        DisplayMaxDistance();

        // タイトル画面で使用されていたキャラクターのプレハブを表示する
        DisplayPreviousSelectedCharacter();
    }

    // 最高記録を表示するメソッド
    private void DisplayMaxDistance()
    {
        // PlayerPrefsから最高記録を読み込む
        float maxDistance = PlayerPrefs.GetFloat("MaxDistance", 0f);

        // UIテキストに最高記録を表示
        if (maxDistanceText != null)
        {
            maxDistanceText.text = "最高記録: " + maxDistance.ToString("F1") + "m";
        }
        else
        {
            Debug.LogError("UIテキストがアタッチされていません。");
        }
    }

    private void DisplayPreviousSelectedCharacter()
    {
        // Resourcesフォルダーからすべてのキャラクタープレハブをロードする
        GameObject[] characterPrefabs = Resources.LoadAll<GameObject>("CharacterSprites");

        // プレハブがロードされているか確認する
        if (characterPrefabs == null || characterPrefabs.Length == 0)
        {
            Debug.LogError("Failed to load character prefabs!");
            return;
        }
        else
        {
            Debug.Log("Character prefabs loaded successfully. Number of character prefabs: " + characterPrefabs.Length);
        }

        // 以前に選択されたキャラクターの識別子を取得する
        string previousSelectedCharacter = PlayerPrefs.GetString("PreviousSelectedCharacter", "DefaultCharacter");
        Debug.Log("Selected character: " + previousSelectedCharacter);

        // 選択されたキャラクターのプレハブを見つける
        GameObject selectedCharacterPrefab = null;
        foreach (var prefab in characterPrefabs)
        {
            CharacterIdentifier identifier = prefab.GetComponent<CharacterIdentifier>();
            if (identifier != null && identifier.characterIdentifier == previousSelectedCharacter)
            {
                selectedCharacterPrefab = prefab;
                break;
            }
        }

        // プレハブが見つかった場合は、そのプレハブのSpriteRendererコンポーネントからスプライトを取得し、characterImageに表示する
        if (selectedCharacterPrefab != null)
        {
            SpriteRenderer spriteRenderer = selectedCharacterPrefab.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && spriteRenderer.sprite != null)
            {
                // SpriteRendererの画像をcharacterImageのspriteに設定する
                characterImage.sprite = spriteRenderer.sprite;
            }
            else
            {
                Debug.LogError("SpriteRendererまたはspriteが見つかりませんでした。");
            }
        }
        else
        {
            Debug.LogError("選択されたキャラクターのプレハブが見つかりませんでした。");
        }
    }
}
