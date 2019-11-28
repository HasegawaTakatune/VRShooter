using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    const float Interval = 3.0f;

    [SerializeField] private GameObject prefab;

    public bool active = true;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        Vector3 Point = transform.position;
        Quaternion Rota = transform.rotation;

        while (active)
        {
            yield return new WaitForSeconds(1.0f);
            Point.x = Random.Range(-Interval, Interval);
            Instantiate(prefab, Point, Rota);
            break;
        }
    }
}
