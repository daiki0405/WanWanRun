using UnityEngine;

public class OptionOpen : MonoBehaviour
{
    // ポップアップの表示・非表示を制御するフラグ
    private bool isPopupOpen = false;

    // ポップアップオブジェクト
    public GameObject popupObject;

    // ボタンが押されたときの処理
    public void TogglePopup()
    {
        // フラグを反転させてポップアップの表示状態を切り替える
        isPopupOpen = !isPopupOpen;

        // ポップアップオブジェクトの表示・非表示を切り替える
        if (popupObject != null)
        {
            popupObject.SetActive(isPopupOpen);
        }

        // タイムスケールを切り替える
        Time.timeScale = isPopupOpen ? 0 : 1;
    }
}
