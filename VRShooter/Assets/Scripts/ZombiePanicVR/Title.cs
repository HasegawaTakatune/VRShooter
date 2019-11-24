using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{

    [SerializeField] private Image title;
    [SerializeField] private Image background;

    private void Reset()
    {
        title = GameObject.Find("Title").GetComponent<Image>();
        background = GameObject.Find("Background").GetComponent<Image>();
    }

    public IEnumerator ShowTitle()
    {
        gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(FadeOut());
        gameObject.SetActive(false);
    }

    private IEnumerator FadeIn()
    {
        Color titleAlpha = title.color;
        Color backgroundAlpha = background.color;

        while (titleAlpha.a < 1)
        {
            yield return new WaitForSeconds(Time.deltaTime);

            titleAlpha.a += Time.deltaTime;
            title.color = titleAlpha;

            backgroundAlpha.a += Time.deltaTime;
            background.color = backgroundAlpha;
        }
    }

    private IEnumerator FadeOut()
    {
        Color titleAlpha = title.color;
        Color backgroundAlpha = background.color;

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
