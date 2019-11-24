using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField] private int timeLimit = 10;

    [SerializeField] private Text text;

    private void Reset()
    {
        text = gameObject.GetComponentInChildren<Text>();
    }

    public IEnumerator CountDown()
    {
        while (timeLimit > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timeLimit -= 1;
            text.text = timeLimit.ToString();
        }
    }
}
