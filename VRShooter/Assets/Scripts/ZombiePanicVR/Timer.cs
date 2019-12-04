using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイマー
/// </summary>
public class Timer : MonoBehaviour
{
    /// <summary>
    /// タイムリミット
    /// </summary>
    [SerializeField] private int timeLimit = 10;

    /// <summary>
    /// テキスト
    /// </summary>
    [SerializeField] private Text text;

    /// <summary>
    /// コンポネントアタッチ時処理
    /// </summary>
    private void Reset()
    {
        text = GameObject.Find("Timer").GetComponentInChildren<Text>();
    }

    /// <summary>
    /// カウントダウン
    /// </summary>
    /// <param name="time">初期タイム</param>
    /// <returns>遅延</returns>
    public IEnumerator CountDown(int time)
    {
        timeLimit = time;

        while (timeLimit > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timeLimit -= 1;
            text.text = timeLimit.ToString();
        }
    }
}
