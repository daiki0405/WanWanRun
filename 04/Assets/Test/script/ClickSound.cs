using UnityEngine;

public class ClickSound : MonoBehaviour
{
    private AudioSource audioSource;

        void Start()
        {
        audioSource = GetComponent<AudioSource>();
        }
        //ボタンをクリックした時のスクリプトです。
        public void OnClick()
        {
        audioSource.PlayOneShot(audioSource.clip);
        }

    }
