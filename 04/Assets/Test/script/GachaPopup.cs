using UnityEngine;
using UnityEngine.UI;

public class GachaPopup : MonoBehaviour
{
    public GameObject popupPanel;
    public Text resultText;
    public Image characterImage;

    // 閉じるボタン
    public Button closeButton;

    private void Start()
    {
        // 閉じるボタンに ClosePopup メソッドを登録
        closeButton.onClick.AddListener(ClosePopup);

        // resultTextの値をデバッグログに出力
        Debug.Log("resultTextの値: " + resultText);
    }

    public void ShowGachaResult(string characterName)
    {
        // ログを追加してメソッドが呼び出されたことを確認する
        Debug.Log("ShowGachaResult メソッドが呼び出されました");

        // Resourcesフォルダ内のプレハブからキャラクターオブジェクトを読み込む
        GameObject characterPrefab = Resources.Load<GameObject>("CharacterSprites/" + characterName);
        if (characterPrefab != null)
        {
            // プレハブからキャラクターオブジェクトを生成
            GameObject characterObject = Instantiate(characterPrefab);

            // キャラクターオブジェクトから必要なコンポーネントを取得（例：スプライトレンダラー）
            SpriteRenderer spriteRenderer = characterObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // スプライトが見つかった場合は設定する
                characterImage.sprite = spriteRenderer.sprite;
            }
            else
            {
                // スプライトが見つからない場合はエラーメッセージを出力する
                Debug.LogError("キャラクターのスプライトが見つかりません：" + characterName);
            }
        }
        else
        {
            // プレハブが見つからない場合はエラーメッセージを出力する
            Debug.LogError("キャラクタープレハブが見つかりません：" + characterName);
        }

        // characterNameの値をデバッグログで確認
        Debug.Log("characterName: " + characterName);

        // ポップアップのテキストを設定
        resultText.text = characterName + "\nを獲得しました！";
        // ポップアップパネルをアクティブにする
        popupPanel.SetActive(true);
    }

    // ポップアップを閉じる
    public void ClosePopup()
    {
        popupPanel.SetActive(false); // ポップアップを非表示にする
    }
}
