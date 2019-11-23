using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    [SerializeField] private List<Transform> point = new List<Transform>();

    [SerializeField] private float max = 5.0f;
    [SerializeField] private float min = 3.0f;

    [HideInInspector] public bool isActive = true;

    private void OnValidate()
    {
        GameObject obj;
        int i = 1;
        while (true)
        {
            obj = GameObject.Find("Point" + i);
            if (obj == null)
                break;

            point.Add(obj.transform);
            i++;
        }
    }

    private IEnumerator Start()
    {
        int length = point.Count;
        int index = Random.Range(0, length);
        float interval = Random.Range(min, max);

        while (true)
        {
            yield return new WaitForSeconds(interval);
            if (!isActive)
                continue;
            interval = Random.Range(min, max);
            index = Random.Range(0, length);
            Instantiate(prefab, point[index].position, point[index].rotation);
        }
    }

}
