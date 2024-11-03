using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionUI : MonoBehaviour
{
    [SerializeField] private string playerPrefabResourcePath = "CharacterSprites"; // プレイヤープレハブのリソースパス
    public Image characterImage; // キャラクターの画像を表示するImageコンポーネント
    public TextMeshProUGUI nameText; // 名前を表示するテキストメッシュプロの配列
    [SerializeField] private string[] playerPrefabIdentifiers; // 各プレハブの識別子
    [SerializeField] private string[] playerNameKeys; // 各入力フィールドの名前キー

    // Startメソッドで初期化処理を行う
    void Start()
    {
        // UI を更新するメソッドを呼び出す
        UpdateCharacterUI(PlayerPrefs.GetString("SelectedCharacterIdentifier", ""));

    }

    public void UpdateCharacterUI(string selectedCharacterIdentifier)
    {
        // Resourcesフォルダーからすべてのプレイヤープレハブをロードする
        GameObject[] playerPrefabs = Resources.LoadAll<GameObject>(playerPrefabResourcePath);

        // プレイヤープレハブがロードされているか確認する
        if (playerPrefabs == null || playerPrefabs.Length == 0)
        {
            Debug.LogError("Failed to load player prefabs!");
            return;
        }
        else
        {
            Debug.Log("Player prefabs loaded successfully. Number of player prefabs: " + playerPrefabs.Length);
        }

        // 選択されたキャラクターの識別子を取得する
        string selectedCharacter = PlayerPrefs.GetString("SelectedCharacterIdentifier", "");

        // playerPrefabIdentifiersとplayerNameKeysの要素数が一致しているか確認する
        if (playerPrefabIdentifiers.Length != playerNameKeys.Length)
        {
            Debug.LogError("The number of elements in playerPrefabIdentifiers and playerNameKeys arrays must be the same.");
            return;
        }

        // 選択されたキャラクターに対応するプレイヤープレハブの識別子を見つける
        string selectedPrefabIdentifier = "";
        for (int i = 0; i < playerPrefabIdentifiers.Length; i++)
        {
            if (playerPrefabIdentifiers[i] == selectedCharacter)
            {
                selectedPrefabIdentifier = playerNameKeys[i];
                break;
            }
        }

        // 対応するプレイヤープレハブの名前を表示する
        if (!string.IsNullOrEmpty(selectedPrefabIdentifier))
        {
            string savedName = PlayerPrefs.GetString(selectedPrefabIdentifier, "No Name");
            nameText.text = savedName;
        }
        else
        {
            Debug.LogError("No player prefab identifier found for selected character: " + selectedCharacter);
        }

        // 選択されたプレイヤープレハブが見つかった場合は、そのプレハブのSpriteRendererコンポーネントを取得し、画像を表示する
        GameObject selectedPlayerPrefab = null;
        foreach (var prefab in playerPrefabs)
        {
            CharacterIdentifier identifier = prefab.GetComponent<CharacterIdentifier>();
            if (identifier != null && identifier.characterIdentifier == selectedCharacter)
            {
                selectedPlayerPrefab = prefab;
                break;
            }
        }

        if (selectedPlayerPrefab != null)
        {
            SpriteRenderer spriteRenderer = selectedPlayerPrefab.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && spriteRenderer.sprite != null)
            {
                // SpriteRendererの画像を表示するSpriteRendererのspriteに設定する
                characterImage.sprite = spriteRenderer.sprite;
            }
            else
            {
                Debug.LogError("SpriteRenderer or sprite not found for selected player prefab: " + selectedPlayerPrefab.name);
            }
        }
        else
        {
            Debug.LogError("Player prefab not found for selected character: " + selectedCharacter);
        }

    }
}
