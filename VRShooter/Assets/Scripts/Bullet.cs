using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 0.5f;

    [SerializeField] private int damage = 10;

    private Vector3 front;

    private Transform trns;

    private void Start()
    {
        trns = transform;
        front = trns.forward * speed;
        Destroy(gameObject, 5.0f);
    }

    private void Update()
    {
        trns.position += front;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Shell>().Damage(damage);
        Destroy(gameObject);
    }

}
