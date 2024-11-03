using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ジャンプ力
    public float jumpPower = 3.0f;
    Rigidbody2D rb;
    Animator anim;
    // 移動速度
    public float speed = 5.0f;
    private float inputWalk = 0;
    bool isJumping;
    // ジャンプ速度
    [SerializeField, Header("ジャンプ速度")]
    private float _JumpSpeed;

    // 体力
    [SerializeField, Header("体力")]
    private int _hp;
    private Rigidbody2D _rigid;
    private SpriteRenderer _spriteRenderer; // プレイヤーのSpriteRendererコンポーネント

    private Coroutine _blinkCoroutine; // 点滅用のコルーチン
    private bool _canTakeDamage = true; // ダメージを受けられる状態かどうかのフラグ

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _rigid = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRendererコンポーネントを取得
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 接地判定
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        // 敵との衝突判定
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Enemy2"))
        {
            if (_canTakeDamage)
            {
                _HitEnemy(collision.gameObject);
            }
            // プレイヤーの位置が敵よりも高い場合にジャンプする
            JumpIfHigher(collision.gameObject);
        }
    }

    void Update()
    {

        // ジャンプ処理
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
        }

        // 横移動
        inputWalk = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputWalk * speed, rb.velocity.y);

        // アニメーション切り替え
        if (inputWalk != 0)
        {
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }

        if (isJumping == true)
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        // 向きを変える
        if (inputWalk > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (inputWalk < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    // ダメージ処理
    public void Damage(int damage)
    {
        if (_canTakeDamage)
        {
            _hp = Mathf.Max(_hp - damage, 0);

            // ダメージを受けた時に点滅処理を開始する
            StartBlinking();

            // 2秒間はダメージを受けられないようにする
            StartCoroutine(DamageCooldown());

            // _Dead() メソッドの呼び出しを遅らせる
            if (_hp <= 0)
            {
                _Dead(); // 0.1秒後に _Dead() メソッドを呼び出す
            }

            // ゲームオーバー条件のチェック
            GameOverPopup.instance.CheckGameOverCondition(); // ダメージを受けた後にゲームオーバー条件のチェックを行う

            // 効果音を再生
            SoundManager.Instance.PlayDamageSound();
        }
    }

    public void SetHP(int newHP)
    {
        _hp = newHP;

        // PlayerHpクラスのインスタンスを取得
        PlayerHp playerHp = FindObjectOfType<PlayerHp>();
        if (playerHp != null)
        {
            // PlayerHpクラスの_ShowHPIconメソッドを呼び出してHPアイコンを更新
            playerHp._ShowHPIcon();
            playerHp._CreateHPIcon();
        }
    }


    // ダメージを受けた後のクールダウン
    private IEnumerator DamageCooldown()
    {
        _canTakeDamage = false;
        yield return new WaitForSeconds(2.0f);
        _canTakeDamage = true;
    }

    // 体力取得
    public int GetHP()
    {
        return _hp;
    }



    // 敵に当たったときの処理
    private void _HitEnemy(GameObject enemy)
    {
        // タグが Enemy2 の場合
        if (enemy.CompareTag("Enemy2"))
        {
            // EnemyDamage スクリプトの PlayerDamage メソッドを呼び出してプレイヤーにダメージを与える
            enemy.GetComponent<EnemyDamage>().PlayerDamage(this);
        }
        // タグが Enemy の場合
        else if (enemy.CompareTag("Enemy"))
        {
            // プレイヤーと敵の高さを比較して、プレイヤーが敵よりも下にいる場合はダメージを与える
            float halfScaleY = transform.lossyScale.y / 2.0f;
            float enemyHalfScaleY = enemy.transform.lossyScale.y / 2.0f;

            // プレイヤーが敵よりも下にいる場合はダメージを与える
            if (transform.position.y - (halfScaleY - 0.1f) < enemy.transform.position.y + (enemyHalfScaleY - 0.1f))
            {
                enemy.GetComponent<EnemyDamage>().PlayerDamage(this);
            }
        }
    }

    // プレイヤーの位置が敵よりも高い場合にジャンプする
    private void JumpIfHigher(GameObject enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            // プレイヤーと敵の高さを比較して、プレイヤーが敵よりも高い場合はジャンプする
            float halfScaleY = transform.lossyScale.y / 2.0f;
            float enemyHalfScaleY = enemy.transform.lossyScale.y / 2.0f;

            if (transform.position.y - (halfScaleY - 0.1f) >= enemy.transform.position.y + (enemyHalfScaleY - 0.1f))
            {
                _rigid.AddForce(Vector2.up * _JumpSpeed, ForceMode2D.Impulse);

                // 猫の鳴き声を再生
                SoundManager.Instance.PlayCatSound();
            }
        }


    }



    // 死亡処理
    private void _Dead()
    {
        if (_hp <= 0)
        {
            // Playerコンポーネントを非アクティブにする
            this.enabled = false;

            // タイムスケールを0にする
            Time.timeScale = 0f;
        }
    }

    // プレイヤーを点滅させる処理
    private void StartBlinking()
    {
        if (_blinkCoroutine != null)
        {
            StopCoroutine(_blinkCoroutine);
        }
        _blinkCoroutine = StartCoroutine(BlinkCoroutine(2.0f));
    }

    private IEnumerator BlinkCoroutine(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f); // 0.1秒間隔で点滅
            timer += 0.1f;
        }
        // 点滅終了後、SpriteRendererを有効にする
        _spriteRenderer.enabled = true;
    }

    private IEnumerator BlinkCoroutine()
    {
        // 一定の間隔で点滅を切り替える
        while (true)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f); // 0.1秒間隔で点滅
        }
    }
}
