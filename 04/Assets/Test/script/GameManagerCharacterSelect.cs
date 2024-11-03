using UnityEngine;

public class GameManagerCharacterSelect : MonoBehaviour
{
    private GameObject[] playerPrefabs; // プレイヤープレハブの配列

    [SerializeField] private GameOverManager gameOverManager; // GameOverManagerへの参照

    [SerializeField] private string playerPrefabResourcePath = "CharacterSprites"; // プレイヤープレハブのリソースパス


    // Startメソッドで初期化処理を行う
    void Start()
    {
        // Resourcesフォルダーからすべてのプレイヤープレハブをロードする
        playerPrefabs = Resources.LoadAll<GameObject>(playerPrefabResourcePath);

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
        string selectedCharacter = PlayerPrefs.GetString("SelectedCharacterIdentifier", "DefaultCharacter");
        Debug.Log("Selected character: " + selectedCharacter);

        // プレイヤーを生成する
        if (playerPrefabs.Length > 0)
        {
            // 選択されたキャラクターに対応するプレイヤープレハブを取得
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
                // プレイヤーを生成する
                GameObject playerInstance = Instantiate(selectedPlayerPrefab);
                playerInstance.SetActive(true);
            }
            else
            {
                Debug.LogError("Failed to find player prefab for selected character: " + selectedCharacter);
            }
        }
    }


}
