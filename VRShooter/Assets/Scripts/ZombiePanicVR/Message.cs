using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームメッセージ表示
/// </summary>
public class Message : MonoBehaviour
{
    /// <summary>
    /// 表示テキスト
    /// </summary>
    [SerializeField] Text text;

    /// <summary>
    /// コンポネントアタッチ時処理
    /// </summary>
    private void Reset()
    {
        text = gameObject.GetComponentInChildren<Text>();
    }

    /// <summary>
    /// メッセージを設定する
    /// </summary>
    /// <param name="message">メッセージ</param>
    /// <param name="active">表示/非表示</param>
    public void SetMessage(string message, bool active = true)
    {
        text.text = message;
        gameObject.SetActive(active);
    }

    /// <summary>
    /// メッセージの表示/非表示を設定する
    /// </summary>
    /// <param name="active">表示/非表示</param>
    public void Activate(bool active)
    {
        gameObject.SetActive(active);
    }
}
