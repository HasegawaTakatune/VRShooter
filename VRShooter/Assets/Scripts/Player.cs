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
    /// 選択UI
    /// </summary>
    [SerializeField] private Selected selected = default;

    /// <summary>
    /// レイヤマスク
    /// </summary>
    [SerializeField] private LayerMask mask = default;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        verRot = transform.parent;
        horRot = transform;
    }

    /// <summary>
    /// メインループ
    /// </summary>
    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        float xRotation = Input.GetAxis("Mouse X");
        float yRotation = Input.GetAxis("Mouse Y");

        verRot.transform.Rotate(0, xRotation, 0);
        horRot.transform.Rotate(-yRotation, 0, 0);
#endif 
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
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, mask))
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
}