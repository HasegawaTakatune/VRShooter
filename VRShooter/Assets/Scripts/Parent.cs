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
            yield return new WaitForSeconds(0.5f);
            shell.ShellBreak();
        }

        GetComponent<Rigidbody>().useGravity = true;
        Destroy(gameObject, 0.2f);
    }
}
