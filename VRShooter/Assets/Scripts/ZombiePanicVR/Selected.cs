using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 選択UI
/// </summary>
public class Selected : MonoBehaviour
{

    /// <summary>
    /// イメージ
    /// </summary>
    [SerializeField] private Image image;

    /// <summary>
    /// コンポネントアタッチ時処理
    /// </summary>
    private void Reset()
    {
        image = GameObject.Find("Circle").GetComponentInChildren<Image>();
    }

    /// <summary>
    /// タイムカウント処理
    /// 受け取った数値分選択ゲージがたまっていく
    /// </summary>
    /// <param name="deltaTime">単位時間</param>
    /// <returns>ゲージがたまり切ったかの判定</returns>
    public bool TimeCount(float deltaTime)
    {
        image.fillAmount += deltaTime;

        if (image.fillAmount < 1) return false;
        else return true;
    }

    /// <summary>
    /// 経過時間をリセットする
    /// </summary>
    public void TimeReset()
    {
        image.fillAmount = 0;
    }

    /// <summary>
    /// UIの表示/非表示
    /// </summary>
    /// <param name="active"></param>
    public void Activate(bool active)
    {
        image.enabled = active;
        image.fillAmount = 0;
    }

}
