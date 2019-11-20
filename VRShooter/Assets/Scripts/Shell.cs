using System.Collections;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Shell : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private bool IsCoreShell = false;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private new Renderer renderer;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private int health = 100;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private int score = 100;

    /// <summary>
    /// 
    /// </summary>
    private float timer = 0;

    /// <summary>
    /// 
    /// </summary>
    private Coroutine coroutine;

    /// <summary>
    /// 
    /// </summary>
    private bool isBreak = false;

    /// <summary>
    /// 
    /// </summary>
    private void Reset()
    {
        renderer = GetComponent<Renderer>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dmg"></param>
    public void Damage(int dmg)
    {
        if (isBreak)
            return;

        health -= dmg;
        timer = 0.5f;

        if (coroutine == null)
        {
            coroutine = StartCoroutine(DamageColor());
        }

        if (health <= 0)
        {
            if (IsCoreShell)
                CoreDestruction();
            else
                ShellBreak();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator DamageColor()
    {
        Color color = renderer.material.color;
        renderer.material.color = Color.red;
        while (timer <= 0)
        {
            yield return null;
            timer -= Time.deltaTime;
            GameManager.Score += score;
        }
        renderer.material.color = color;
        coroutine = null;
    }

    /// <summary>
    /// 
    /// </summary>
    public void ShellBreak()
    {
        if (IsCoreShell)
            return;

        isBreak = true;
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        Destroy(gameObject, 0.2f);
    }

    /// <summary>
    /// 
    /// </summary>
    private void CoreDestruction()
    {
        GetComponentInParent<Parent>().BreakCoreShell();
    }
}
