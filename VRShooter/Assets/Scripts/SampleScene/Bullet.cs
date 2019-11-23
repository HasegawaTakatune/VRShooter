using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        Shell shell = collision.gameObject.GetComponent<Shell>();
        if (shell != null)
        {
            shell.Damage(damage);
            Destroy(gameObject);
        }
    }

}
