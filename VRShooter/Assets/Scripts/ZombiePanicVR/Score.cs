using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スコア
/// </summary>
public class Score : MonoBehaviour
{
    /// <summary>
    /// テキスト
    /// </summary>
    [SerializeField] private Text text;

    /// <summary>
    /// スコア保存
    /// スコアは保存時には1ケタ台で保存をしているが、
    /// 表示の際は演出上100倍した数値で表示をさせる
    /// </summary>
    private static int score;

    /// <summary>
    /// スコア取得
    /// </summary>
    /// <returns>スコア</returns>
    public static int GetScore() { return score * 100; }

    /// <summary>
    /// 各敵ごとのスコア値を保持
    /// </summary>
    private static int[] scoreList = { 5, 10 };

    /// <summary>
    /// 各敵の倒した数を保持
    /// </summary>
    private static int[] killCount = new int[(int)ZOMBIE_TYPE.LENGTH];

    /// <summary>
    /// 倒した敵の数を取得する
    /// </summary>
    /// <param name="type">取得する敵の種類</param>
    /// <returns>倒した数</returns>
    public static int GetKillCount(ZOMBIE_TYPE type) { return killCount[(int)type]; }

    /// <summary>
    /// スコアが更新されたことを判定する
    /// </summary>
    private static bool isChanged = false;

    /// <summary>
    /// コンポネントアタッチ時処理
    /// </summary>
    private void Reset()
    {
        text = GameObject.Find("Score").GetComponent<Text>();
    }

    /// <summary>
    /// メインループ
    /// </summary>
    private void Update()
    {
        if (isChanged)
        {
            isChanged = false;
            text.text = "Score:" + GetScore();
        }
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="value">加算する敵の種類</param>
    public static void AddScore(ZOMBIE_TYPE value)
    {
        int index = (int)value;
        score += scoreList[index];
        killCount[index]++;
        isChanged = true;
    }
}
