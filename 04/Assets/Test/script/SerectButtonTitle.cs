using UnityEngine;
using UnityEngine.SceneManagement;

public class SerectButtonTitle : MonoBehaviour
{
    [SerializeField] Fade fade;
    public BackgroundMusic bgm; // BackgroundMusicスクリプトをアタッチしたオブジェクトを参照する変数

    public void OnNextScene()
    {
        fade.FadeIn(0.5f, () => SceneManager.LoadScene("Home"));

    }
}
