using UnityEngine;
using UnityEngine.SceneManagement; // 追加

public class FadeIn : MonoBehaviour
{
    [SerializeField] Fade fade;

    public void OnNextScene()
    {
        fade.FadeIn(0.5f, () => SceneManager.LoadScene("Gacha"));
    }
}
