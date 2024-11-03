using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;
    public AudioClip defaultBGM; // デフォルトのBGM
    private AudioClip lastPlayedBGM; // 前回再生したBGMを保持する変数
    private AudioSource audioSource;

    public static BackgroundMusic Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        // 既にインスタンスが存在する場合、新しいインスタンスを破棄する
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // このインスタンスをシングルトンとして設定し、シーンを切り替えても破棄されないようにする
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        // AudioSourceを取得
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // AudioSourceがアタッチされていない場合は警告をログに出力
            Debug.LogWarning("AudioSource component is not attached to the BackgroundMusic game object.");
        }
    }

    void Start()
    {
        Debug.Log("BackgroundMusic Start method called.");
        // デフォルトのBGMを再生
        PlayDefaultBGM();
    }

    // デフォルトのBGMを再生するメソッド
    public void PlayDefaultBGM()
    {
        if (audioSource != null && defaultBGM != null)
        {
            audioSource.clip = defaultBGM;
            audioSource.Play();
            lastPlayedBGM = defaultBGM; // 前回再生したBGMを更新
            Debug.Log("Playing default BGM.");
        }
        else
        {
            Debug.LogWarning("Failed to play default BGM. AudioSource or AudioClip is null.");
        }
    }

    // Gameシーン用のBGMを再生するメソッド
    public void PlayGameBGM(AudioClip bgmClip)
    {
        if (audioSource != null && bgmClip != null)
        {
            // 前回再生したBGMと同じ場合は再生しない
            if (lastPlayedBGM != bgmClip)
            {
                audioSource.clip = bgmClip;
                audioSource.Play();
                lastPlayedBGM = bgmClip; // 前回再生したBGMを更新
                Debug.Log("Playing game BGM.");
            }
        }
        else
        {
            Debug.LogWarning("Failed to play game BGM. AudioSource or AudioClip is null.");
        }
    }

    // BGMを停止するメソッド
    public void StopBGM()
    {
        audioSource.Stop();
    }

    // 追加: BGMを再開するメソッド
    public void ResumeBGM()
    {
        if (audioSource != null && lastPlayedBGM != null)
        {
            audioSource.Play();
            Debug.Log("Resuming BGM.");
        }
        else
        {
            Debug.LogWarning("Failed to resume BGM. AudioSource or AudioClip is null.");
        }
    }
}
