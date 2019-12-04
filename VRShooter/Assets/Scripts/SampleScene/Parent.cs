using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Parent : MonoBehaviour
{

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private ENEMY_TYPE type = default;

    private void Start()
    {
        switch (type)
        {
            case ENEMY_TYPE.FISH: gameObject.AddComponent<Fish>(); break;
            case ENEMY_TYPE.TANK: break;
            default: break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void BreakCoreShell()
    {
        StartCoroutine(BreakShells());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator BreakShells()
    {
        Shell[] shells = GetComponentsInChildren<Shell>();
        shells = shells.OrderBy(i => Guid.NewGuid()).ToArray();

        foreach (Shell shell in shells)
        {
            yield return new WaitForSeconds(0.1f);
            shell.ShellBreak();
        }
        Destroy(gameObject, 0.2f);
    }
}
