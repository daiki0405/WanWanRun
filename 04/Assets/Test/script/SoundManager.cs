using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // 静的なインスタンス変数

    // 他のコードから参照できるようにするため、インスタンスを公開
    public static SoundManager Instance { get { return instance; } }

    public AudioClip oyatuDestroySound; // おやつが破壊されたときに再生する効果音

    private AudioSource audioSource;

    public AudioClip damageSound; // ダメージを受けたときに再生する効果音

    public AudioClip jumpSound; // ジャンプ時に再生する効果音

    public AudioClip catSound; // 猫の鳴き声の効果音

    public AudioClip gameOverSound; // ゲームオーバー時に再生する効果音


    private void Awake()
    {
        // インスタンスを設定
        instance = this;
    }

    private void Start()
    {
        // AudioSourceを取得
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // AudioSourceがアタッチされていない場合は警告をログに出力
            Debug.LogWarning("AudioSource component is not attached to the game object.");
        }
    }

    // おやつが削除されたときに呼び出されるメソッド
    public void PlayOyatuDestroySound()
    {
        if (audioSource != null && oyatuDestroySound != null)
        {
            // 効果音を再生
            audioSource.PlayOneShot(oyatuDestroySound);
        }
        else
        {
            Debug.LogWarning("Failed to play oyatu destroy sound. AudioSource or AudioClip is null.");
        }
    }

    // 新しい効果音を再生するためのメソッド
    public void PlayDamageSound()
    {
        if (audioSource != null && damageSound != null)
        {
            // 効果音を再生
            audioSource.PlayOneShot(damageSound);
        }
        else
        {
            Debug.LogWarning("Failed to play damage sound. AudioSource or AudioClip is null.");
        }
    }

    // 新しい効果音を再生するためのメソッド
    public void PlayJumpSound()
    {
        if (audioSource != null && jumpSound != null)
        {
            // 効果音を再生
            audioSource.PlayOneShot(jumpSound);
        }
        else
        {
            Debug.LogWarning("Failed to play jump sound. AudioSource or AudioClip is null.");
        }
    }

    // 新しい効果音を再生するためのメソッド
    public void PlayCatSound()
    {
        if (audioSource != null && catSound != null)
        {
            // 効果音を再生
            audioSource.PlayOneShot(catSound);
        }
        else
        {
            Debug.LogWarning("Failed to play cat sound. AudioSource or AudioClip is null.");
        }

    }

    // 新しい効果音を再生するためのメソッド
    public void PlayGameOverSound()
    {
        if (audioSource != null && gameOverSound != null)
        {
            // 効果音を再生
            audioSource.PlayOneShot(gameOverSound);
        }
        else
        {
            Debug.LogWarning("Failed to play game over sound. AudioSource or AudioClip is null.");
        }
    }


}