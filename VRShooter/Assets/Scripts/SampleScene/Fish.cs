using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Vector3 forward;

    void Start()
    {
        forward = transform.forward * Random.Range(1.0f, 2.5f) * Time.deltaTime;
    }

    void Update()
    {
        transform.position += forward;
    }
}
