using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 _initPos; // カメラの初期位置

    // Start is called before the first frame update
    void Start()
    {
        _initPos = transform.position; // カメラの初期位置を設定
    }

    // Update is called once per frame
    void Update()
    {
        _FollowPlayer(); // プレイヤーに追従する
    }

    // プレイヤーに追従するメソッド
    private void _FollowPlayer()
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player"); // プレイヤーオブジェクトを見つける
        if (_player != null)
        {
            float x = _player.transform.position.x; // プレイヤーのx座標を取得
            x = Mathf.Clamp(x, _initPos.x, Mathf.Infinity); // カメラが動く範囲を制限
            transform.position = new Vector3(x, transform.position.y, transform.position.z); // カメラの位置を更新
        }
    }
}