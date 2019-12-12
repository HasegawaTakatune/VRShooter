using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    /// ヒットエフェクト
    /// </summary>
    [SerializeField] private GameObject hitEffect = default;

    /// <summary>
    /// エイム
    /// </summary>
    [SerializeField] private Image Aim = default;

    /// <summary>
    /// ヒットエイムUI表示時間
    /// </summary>
    private float hitTime = 0;

    /// <summary>
    /// ショットエフェクト
    /// </summary>
    [SerializeField] private List<ParticleSystem> shotEffects = new List<ParticleSystem>();

    /// <summary>
    /// ダメージ
    /// </summary>
    [SerializeField] private int damage = 10;

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
    /// ゾンビレイヤー
    /// </summary>
    [SerializeField] private LayerMask ZombieLayer = default;

    /// <summary>
    /// UIレイヤー
    /// </summary>
    [SerializeField] private LayerMask UILayer = default;

    /// <summary>
    /// ワープレイヤー
    /// </summary>
    [SerializeField] private LayerMask warpLayer = default;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        verRot = transform.parent;
        horRot = transform;
        action = Shoot;
    }

    /// <summary>
    /// 弾を撃ちだす
    /// </summary>
    private void Shoot()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
            _Shoot();
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
            _Shoot();
#endif
    }

    private void _Shoot()
    {
        for (int i = 0; i < shotEffects.Count; i++) shotEffects[i].Play();
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, ZombieLayer))
        {
            if (hitTime > 0) hitTime = 0.5f;
            else { hitTime = 0.5f; StartCoroutine(Hit()); }
            hit.collider.GetComponent<Zombie>().Damage(damage);
            Instantiate(hitEffect, hit.point, Quaternion.identity);
        }
    }

    private IEnumerator Hit()
    {
        Aim.color = Color.red;

        float deltaTime = Time.deltaTime;
        while (hitTime > 0)
        {
            yield return new WaitForSeconds(deltaTime);
            hitTime -= deltaTime;
        }
        Aim.color = Color.white;
    }

    /// <summary>
    /// ワープ
    /// </summary>
    private void Warp()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            _Warp();
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
                _Warp();
        }
#endif
    }

    private void _Warp()
    {
        transform.parent.position = warpPoint.position + Vector3.up;
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
#endif
        action();
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
        WarpZone warp = default;

        while (true)
        {
            yield return null;

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, warpLayer))
            {
                if (warp == null)
                {
                    action = Warp;
                    warpPoint = hit.collider.gameObject.transform;
                    warp = hit.collider.GetComponent<WarpZone>();
                    warp.Look();
                }
            }
            else
            {
                if (warp != null)
                {
                    action = Shoot;
                    warpPoint = null;
                    warp.NotLook();
                    warp = null;
                }
            }
        }
    }

}