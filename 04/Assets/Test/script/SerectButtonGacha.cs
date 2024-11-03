using UnityEngine;
using UnityEngine.SceneManagement;

public class SerectButtonGacha : MonoBehaviour
{
    [SerializeField] Fade fade;

    public void OnNextScene()
    {
        fade.FadeIn(0.5f, () => SceneManager.LoadScene("Gacha"));
    }
}
