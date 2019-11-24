using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selected : MonoBehaviour
{

    [SerializeField] private Image image;

    private void Reset()
    {
        image = gameObject.GetComponentInChildren<Image>();
    }

    public bool TimeCount(float deltaTime)
    {
        image.fillAmount += deltaTime;

        if (image.fillAmount < 1) return false;
        else return true;
    }

    public void TimeReset()
    {
        image.fillAmount = 0;
    }

    public void Activate(bool active)
    {
        gameObject.SetActive(active);
        image.fillAmount = 0;
    }

}
