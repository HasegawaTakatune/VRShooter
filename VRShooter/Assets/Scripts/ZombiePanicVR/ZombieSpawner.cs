using System.Collections;
using UnityEngine;

/// <summary>
/// ゾンビスポナ
/// </summary>
public class ZombieSpawner : MonoBehaviour
{
    /// <summary>
    /// スポーン間隔
    /// </summary>
    const float Interval = 3.0f;

    /// <summary>
    /// プレファブ
    /// </summary>
    [SerializeField] private GameObject prefab = default;

    /// <summary>
    /// アクティブ
    /// </summary>
    private bool active = false;

    /// <summary>
    /// アクティブのゲッタ/セッタ
    /// </summary>
    public bool Active { get { return active; } set { active = value; if (active) StartCoroutine(Spawn()); } }

    /// <summary>
    /// スポーン
    /// </summary>
    /// <returns>遅延</returns>
    private IEnumerator Spawn()
    {
        Vector3 Point = transform.position;
        Quaternion Rota = transform.rotation;

        while (active)
        {
            yield return new WaitForSeconds(1.0f);
            Point.x = Random.Range(-Interval, Interval);
            Instantiate(prefab, Point, Rota);
        }
    }
}
