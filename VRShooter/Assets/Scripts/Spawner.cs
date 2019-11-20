using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    [SerializeField] private List<Transform> spawnPostion = new List<Transform>();

    [SerializeField] private float max = 5.0f;
    [SerializeField] private float min = 3.0f;

    [HideInInspector] public bool isActive = true;

    private IEnumerator Start()
    {
        int length = spawnPostion.Count;
        int index = Random.Range(0, length);
        float interval = Random.Range(min, max);

        while (true)
        {
            yield return new WaitForSeconds(interval);
            if (!isActive)
                continue;
            interval = Random.Range(min, max);
            index = Random.Range(0, length);
            Instantiate(prefab, spawnPostion[index].position, Quaternion.identity);
        }
    }

}
