using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    private void Reset()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (particle.isStopped)
            Destroy(gameObject);
    }
}
