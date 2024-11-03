using UnityEngine;
using System.Collections.Generic;

public class GachaButton : MonoBehaviour
{
    public List<GameObject> characterPrefabs; // ガチャで入手できるキャラクターのプレハブのリスト
    public Transform spawnPoint; // キャラクターの生成位置
    public ScoreManager scoreManager; // スコアを管理するスクリプトへの参照

    // ガチャを回す処理
    public void SpinGacha()
    {
        Debug.Log("ガチャを回す処理が呼び出されました。");
        // スコアが1000以上であればガチャを回す
        if (scoreManager.Score >= 1000) // ScoreManager クラスの Score プロパティにアクセスする
        {
            Debug.Log("スコアが十分です。ガチャを回します。");

            // キャラクターリストからランダムにキャラクタープレハブを選択する
            int randomIndex = Random.Range(0, characterPrefabs.Count);
            Debug.Log("ランダムに選択されたキャラクターのインデックス：" + randomIndex);

            // ガチャで入手できるキャラクターを生成し、指定された位置に配置する
            GameObject characterPrefab = characterPrefabs[randomIndex];
            GameObject spawnedCharacter = Instantiate(characterPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("ガチャからキャラクターを生成しました。");

            // スコアを1000消費する
            scoreManager.UseScore(1000);
            Debug.Log("スコアを消費しました。");
        }
        else
        {
            Debug.Log("スコアが不足しています！");
        }
    }
}