using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField, Header("振動する時間")]
    private float _shakeTime;

    [SerializeField, Header("振動の大きさ")]
    private float _shakeMagnitude;

    private GameObject _player;
    private float _shakeCount;
    private int _currentPlayerHP;
    private bool _playerFound = false;
    private bool _isShaking = false; // 振動中かどうかのフラグ

    // Update is called once per frame
    void Update()
    {
        // プレイヤーオブジェクトがまだ見つかっていない場合は、検索を続ける
        if (!_playerFound)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            if (_player != null)
            {
                // プレイヤーオブジェクトが見つかったら、フラグを設定して振動のチェックを開始する
                _playerFound = true;
                _currentPlayerHP = _player.GetComponent<Player>().GetHP();
            }
            else
            {
                // プレイヤーオブジェクトがまだ見つからない場合は、メッセージをログに出力する
                Debug.LogWarning("Player object not found! Camera shake will not be triggered.");
            }
        }

        if (_playerFound)
        {
            _ShakeCheck();
        }
    }

    private void _ShakeCheck()
    {
        int newHP = _player.GetComponent<Player>().GetHP();
        if (_currentPlayerHP != newHP)
        {
            _currentPlayerHP = newHP;
            if (!_isShaking && Time.timeScale != 0) // 振動中でなく、かつタイムスケールが０でない場合のみ振動を開始
            {
                StartCoroutine(_Shake());
            }
        }
    }


    IEnumerator _Shake()
    {
        _isShaking = true; // 振動中フラグを設定

        float elapsedTime = 0f; // 経過時間を初期化

        Vector3 initPos = transform.position; // 初期位置を保存

        while (elapsedTime < _shakeTime)
        {
            // 振動中の座標をランダムに計算
            float x = initPos.x + Random.Range(-_shakeMagnitude, _shakeMagnitude);
            float y = initPos.y + Random.Range(-_shakeMagnitude, _shakeMagnitude);
            transform.position = new Vector3(x, y, initPos.z);

            elapsedTime += Time.deltaTime; // 経過時間を更新

            yield return null;
        }

        transform.position = initPos; // 振動終了時に初期位置に戻す
        _isShaking = false; // 振動中フラグを解除
    }
}
