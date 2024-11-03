using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBGMManager : MonoBehaviour
{
    private BackgroundMusic bgm;
    public SceneBGMData sceneBGMData; // シーンごとのBGMデータを保持するための変数

    void Start()
    {
        // BackgroundMusic オブジェクトを見つけて取得する
        bgm = FindObjectOfType<BackgroundMusic>();
        if (bgm == null)
        {
            Debug.LogWarning("BackgroundMusic script not found in the scene.");
            return;
        }

        // 現在のシーンの名前を取得し、それに対応するBGMを再生する
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlaySceneBGM(currentSceneName);

        // シーンが切り替わったときにOnSceneLoadedメソッドを実行するようにイベントを登録する
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // オブジェクトが破棄されるときに、イベントの登録を解除する
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // シーンがロードされたときに呼ばれるメソッド
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 新しいシーンの名前を取得してBGMを再生する
        PlaySceneBGM(scene.name);
    }

    // シーンごとに異なるBGMを再生するメソッド
    public void PlaySceneBGM(string sceneName)
    {
        // シーンごとのBGMを取得
        AudioClip sceneBGM = GetSceneBGM(sceneName);

        // BGMが設定されていない場合はデフォルトのBGMを再生
        if (sceneBGM == null)
        {
            bgm.PlayDefaultBGM();
        }
        else
        {
            bgm.PlayGameBGM(sceneBGM); // シーンごとのBGMを再生
        }
    }

    // シーンごとのBGMを取得するメソッド
    private AudioClip GetSceneBGM(string sceneName)
    {
        if (sceneBGMData != null)
        {
            foreach (var sceneBGM in sceneBGMData.sceneBGMs)
            {
                if (sceneBGM.sceneName == sceneName)
                {
                    return sceneBGM.bgm;
                }
            }
        }
        return null;
    }


}
