using UnityEngine;

/// <summary>
/// バリケード
/// </summary>
public class Barricade : MonoBehaviour
{
    /// <summary>
    /// 体力
    /// </summary>
    private int health = 1000;

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="dmg">ダメージ量</param>
    public void Damage(int dmg)
    {
        health -= dmg;
    }
}
