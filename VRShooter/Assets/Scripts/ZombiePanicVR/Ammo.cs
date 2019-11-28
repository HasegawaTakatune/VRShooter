using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;

    [SerializeField] private int damage = 10;

    private Vector3 front;

    private void Start()
    {
        front = transform.forward * speed * Time.deltaTime;
        Destroy(gameObject, 5.0f);
    }

    private void Update()
    {
        transform.position += front;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Zombie zombie = collision.gameObject.GetComponent<Zombie>();        
        if (zombie != null)
        {
            zombie.Damage(damage);
            Destroy(gameObject);
        }
    }
}
