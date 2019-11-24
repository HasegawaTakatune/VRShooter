using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    [SerializeField] Text text;

    private void Reset()
    {
        text = gameObject.GetComponentInChildren<Text>();
    }

    public void SetMessage(string message, bool active = true)
    {
        text.text = message;
        gameObject.SetActive(active);
    }

    public void Activate(bool active)
    {
        gameObject.SetActive(active);
    }
}
