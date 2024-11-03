using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] ground;
    private GameObject player; // プレイヤーオブジェクト
    private float _spawnpoint = 0;
    private float _spawnpointB = 0;
    private int number;
    private bool playerSpawned = false; // プレイヤーオブジェクトが生成されたかどうかのフラグ

    void Start()
    {
        StartCoroutine(WaitForPlayerSpawn()); // プレイヤーオブジェクトの生成を待機する
    }

    IEnumerator WaitForPlayerSpawn()
    {
        while (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player"); // プレイヤーオブジェクトを検出
            yield return null;
        }

        playerSpawned = true; // プレイヤーオブジェクトが生成されたことをフラグで記録
    }

    void Update()
    {
        if (!playerSpawned)
        {
            return; // プレイヤーオブジェクトが生成されるまで待機
        }

        if (player.transform.position.x > _spawnpoint + 10)
        {
            number = Random.Range(0, ground.Length);
            Instantiate(ground[number], new Vector3(_spawnpoint + 50, -8, 0), Quaternion.identity);
            _spawnpoint += 50;
        }

        if (player.transform.position.x < _spawnpointB - 10)
        {
            Instantiate(ground[number], new Vector3(_spawnpointB - 50, -8, 0), Quaternion.identity);
            _spawnpointB -= 50;
        }

        if (player.transform.position.x > _spawnpointB + 50)
        {
            _spawnpointB += 50;
        }

        if (player.transform.position.x < _spawnpoint - 50)
        {
            _spawnpoint -= 50;
        }
    }
}