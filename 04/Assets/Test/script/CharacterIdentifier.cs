using UnityEngine;

public class CharacterIdentifier : MonoBehaviour
{
    public string characterIdentifier; // キャラクターの識別子

    // 識別子を取得するメソッド
    public string GetCharacterIdentifier()
    {
        return characterIdentifier;
    }
}