using UnityEngine;

public class EnemyWalk2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool movingRight = true;
    private float changeDirectionTimer = 2f;
    private float timer = 0f;

    void Update()
    {
        // タイマーを更新
        timer += Time.deltaTime;

        // 指定時間経過したらランダムで方向を変更
        if (timer >= changeDirectionTimer)
        {
            timer = 0f;
            if (Random.value > 0.5f) // 50%の確率で方向転換する
            {
                ChangeDirectionRandomly();
            }
        }

        // X軸方向の移動
        float moveX = movingRight ? 1f : -1f;
        Vector3 movement = new Vector3(moveX, 0f, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    // ランダムで方向を変更するメソッド
    private void ChangeDirectionRandomly()
    {
        movingRight = !movingRight; // 方向を反転

        // 進行方向に向くように設定
        if (movingRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    // 他のオブジェクトとの衝突を検知
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトのタグが"Enemy"であれば方向転換する
        if (collision.gameObject.CompareTag("Enemy")|| collision.gameObject.CompareTag("Enemy2"))
        {
            ChangeDirectionRandomly();
        }
    }
}