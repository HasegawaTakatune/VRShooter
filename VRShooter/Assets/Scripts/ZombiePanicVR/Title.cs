using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイトル
/// </summary>
public class Title : MonoBehaviour
{
    /// <summary>
    /// タイトル
    /// </summary>
    [SerializeField] private Image title;

    /// <summary>
    /// 背景
    /// </summary>
    [SerializeField] private Image background;

    /// <summary>
    /// コンポネントアタッチ時処理
    /// </summary>
    private void Reset()
    {
        title = GameObject.Find("Title").GetComponent<Image>();
        background = GameObject.Find("Background").GetComponent<Image>();
    }

    /// <summary>
    /// タイトル表示
    /// フェードイン→数秒表示→フェードアウトで表示していく
    /// </summary>
    /// <returns>遅延</returns>
    public IEnumerator ShowTitle()
    {
        gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(FadeOut());
        gameObject.SetActive(false);
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    /// <returns>遅延</returns>
    private IEnumerator FadeIn()
    {
        // カラーを取得
        Color titleAlpha = title.color;
        Color backgroundAlpha = background.color;

        // アルファ値が最大になるまでループ
        // その間、徐々にアルファ値を上げていく
        while (titleAlpha.a < 1)
        {
            yield return new WaitForSeconds(Time.deltaTime);

            titleAlpha.a += Time.deltaTime;
            title.color = titleAlpha;

            backgroundAlpha.a += Time.deltaTime;
            background.color = backgroundAlpha;
        }
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    /// <returns>遅延</returns>
    private IEnumerator FadeOut()
    {
        // カラーを取得
        Color titleAlpha = title.color;
        Color backgroundAlpha = background.color;

        // アルファ値が最小になるまでループ
        // その間、徐々にアルファ値を下げていく
        while (titleAlpha.a > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);

            titleAlpha.a -= Time.deltaTime;
            title.color = titleAlpha;

            backgroundAlpha.a -= Time.deltaTime;
            background.color = backgroundAlpha;
        }
    }
}
