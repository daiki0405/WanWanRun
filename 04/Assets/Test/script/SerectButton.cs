using UnityEngine;
using UnityEngine.SceneManagement;

public class SerectButton : MonoBehaviour

{
    [SerializeField] Fade fade;

    public void OnNextScene()
    {
        fade.FadeIn(0.5f, () => SceneManager.LoadScene("CharacterSerect"));
    }
}
