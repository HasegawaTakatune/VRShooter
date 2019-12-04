using UnityEngine;

/// <summary>
/// 撃ちだし処理
/// </summary>
public class Shot : MonoBehaviour
{
    /// <summary>
    /// 弾丸
    /// </summary>
    [SerializeField] private GameObject bullet = default;

    /// <summary>
    /// メインループ
    /// </summary>
    private void Update()
    {
        // エディタではマウスクリック、Android上では画面タッチで処理させる
#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
            Shoot();

#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
                Shoot();
        }
#endif 
    }

    /// <summary>
    /// 弾を撃ちだす
    /// </summary>
    private void Shoot()
    {
        Instantiate(bullet, transform.position + transform.forward, transform.rotation);
    }

}
