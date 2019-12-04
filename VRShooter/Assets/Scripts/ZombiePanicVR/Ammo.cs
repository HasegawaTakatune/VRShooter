using UnityEngine;

/// <summary>
/// 弾丸
/// </summary>
public class Ammo : MonoBehaviour
{
    /// <summary>
    /// 速度
    /// </summary>
    [SerializeField] private float speed = 0.5f;

    /// <summary>
    /// ダメージ
    /// </summary>
    [SerializeField] private int damage = 10;

    /// <summary>
    /// 移動方向（前方）
    /// </summary>
    private Vector3 front;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        front = transform.forward * speed * Time.deltaTime;
        Destroy(gameObject, 5.0f);
    }

    /// <summary>
    /// メインループ
    /// </summary>
    private void Update()
    {
        transform.position += front;
    }

    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="collision">ヒット情報</param>
    private void OnCollisionEnter(Collision collision)
    {
        // あたったのがゾンビだった場合のみ、ダメージ処理がされる
        Zombie zombie = collision.gameObject.GetComponent<Zombie>();
        if (zombie != null)
        {
            zombie.Damage(damage);
            Destroy(gameObject);
        }
    }
}
