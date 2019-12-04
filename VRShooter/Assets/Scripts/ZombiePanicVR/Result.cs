using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// リザルト表示
/// </summary>
public class Result : MonoBehaviour
{
    /// <summary>
    /// テキスト
    /// </summary>
    [SerializeField] private Text text;

    /// <summary>
    /// コンポネントアタッチ時処理
    /// </summary>
    private void Reset()
    {
        text = GameObject.Find("Result").GetComponent<Text>();
    }

    /// <summary>
    /// リザルト表示
    /// </summary>
    /// <returns>遅延</returns>
    public IEnumerator ShowResult()
    {
        ReseText();

        // 別個でスコアを1つずつ表示していく
        yield return new WaitForSeconds(0.5f);
        SetText("【 Result 】");

        yield return new WaitForSeconds(0.5f);
        SetText(ZOMBIE_TYPE.NORMAL.ToString() + " x " + Score.GetKillCount(ZOMBIE_TYPE.NORMAL).ToString());

        yield return new WaitForSeconds(0.5f);
        SetText(ZOMBIE_TYPE.TANK.ToString() + " x " + Score.GetKillCount(ZOMBIE_TYPE.TANK).ToString());

        yield return new WaitForSeconds(1.0f);
        SetText("Total  " + Score.GetScore());

        yield return new WaitForSeconds(5.0f);
        ReseText();
    }

    /// <summary>
    /// テキストを空白にする
    /// </summary>
    private void ReseText() { text.text = ""; }

    /// <summary>
    /// テキストを追加する
    /// </summary>
    /// <param name="txt">表示テキスト</param>
    private void SetText(string txt) { text.text += txt + "\n"; }
}
