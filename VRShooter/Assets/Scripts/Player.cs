using System.Collections;
using UnityEngine;

/// <summary>
/// プレイヤー
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// 垂直ローテーション
    /// </summary>
    private Transform verRot;

    /// <summary>
    /// 水平ローテーション
    /// </summary>
    private Transform horRot;

    /// <summary>
    /// 弾丸
    /// </summary>
    [SerializeField] private GameObject bullet = default;

    /// <summary>
    /// ワープポイント
    /// </summary>
    private Transform warpPoint = default;

    /// <summary>
    /// デリゲート
    /// </summary>
    private delegate void Action();

    /// <summary>
    /// アクション
    /// </summary>
    private Action action;

    /// <summary>
    /// 選択UI
    /// </summary>
    [SerializeField] private Selected selected = default;

    /// <summary>
    /// UIレイヤー
    /// </summary>
    [SerializeField] private LayerMask UILayer;

    /// <summary>
    /// ワープレイヤー
    /// </summary>
    [SerializeField] private LayerMask warpLayer;

    /// <summary>
    /// 弾を撃ちだす
    /// </summary>
    private void Shoot()
    {
        Instantiate(bullet, transform.position + transform.forward, transform.rotation);
    }

    /// <summary>
    /// ワープ
    /// </summary>
    private void Warp()
    {
        transform.parent.position = warpPoint.position + Vector3.up;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        verRot = transform.parent;
        horRot = transform;
        action = Shoot;
        //StartCoroutine(SelectWarpZone());
    }

    /// <summary>
    /// メインループ
    /// </summary>
    private void Update()
    {
        // エディタではマウスクリック、Android上では画面タッチで処理させる
#if UNITY_EDITOR

        float xRotation = Input.GetAxis("Mouse X");
        float yRotation = Input.GetAxis("Mouse Y");

        verRot.transform.Rotate(0, xRotation, 0);
        horRot.transform.Rotate(-yRotation, 0, 0);

        if (Input.GetMouseButtonDown(0))
            action();

#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
                action();
        }
#endif 
        Debug.DrawRay(transform.position, transform.forward, Color.red, Mathf.Infinity);
    }

    /// <summary>
    /// 選択中にゲージがたまっていく処理
    /// </summary>
    /// <returns></returns>
    public IEnumerator Selected()
    {
        selected.Activate(true);
        while (true)
        {
            yield return null;

            // 選択項目を注視している間ゲージがたまる
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, UILayer))
            {
                if (selected.TimeCount(Time.deltaTime))
                    break;
            }
            else
            {
                selected.TimeReset();
            }
        }
        selected.Activate(false);
    }

    /// <summary>
    /// ワープゾーンを選択する
    /// </summary>
    /// <returns></returns>
    public IEnumerator SelectWarpZone()
    {
        MeshRenderer renderer = null;

        while (true)
        {
            yield return null;

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, warpLayer))
            {
                if (renderer == null)
                {
                    action = Warp;
                    warpPoint = hit.collider.gameObject.transform;
                    renderer = hit.collider.gameObject.GetComponent<MeshRenderer>();
                    renderer.enabled = true;
                }
            }
            else
            {
                if (renderer != null)
                {
                    action = Shoot;
                    warpPoint = null;
                    renderer.enabled = false;
                    renderer = null;
                }
            }
        }
    }

}