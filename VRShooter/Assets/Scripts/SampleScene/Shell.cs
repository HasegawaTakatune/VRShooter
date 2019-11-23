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
    private bool isDamage = false;

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

        if (!isDamage)
        {
            StartCoroutine(DamageColor());
        }

        if (health <= 0)
        {
            GameManager.Score += score;
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
        isDamage = true;
        Color color = renderer.material.color;
        renderer.material.color = Color.red;
        while (timer > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            timer -= Time.deltaTime;
        }
        renderer.material.color = color;
        isDamage = false;
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
        gameObject.AddComponent<Rigidbody>();
        Destroy(gameObject, 5.0f);
    }

    /// <summary>
    /// 
    /// </summary>
    private void CoreDestruction()
    {
        GetComponentInParent<Parent>().BreakCoreShell();
        gameObject.AddComponent<Rigidbody>();
    }
}
