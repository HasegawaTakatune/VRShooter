using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    private Transform trns;

    private void Start()
    {
        trns = transform;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                Shoot();
            }
        }
#endif 
    }

    private void Shoot()
    {
        Instantiate(bullet, trns.position + trns.forward, trns.rotation);
    }

}
