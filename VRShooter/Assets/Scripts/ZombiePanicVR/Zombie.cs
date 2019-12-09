using System.Collections;
using UnityEngine;

/// <summary>
/// 通常ゾンビ
/// </summary>
public class Zombie : MonoBehaviour
{
    /// <summary>
    /// ゾンビステータス保持
    /// </summary>
    private ZOMBIE_STATUS state = ZOMBIE_STATUS.STOP;

    /// <summary>
    /// ゾンビステータスのゲッタ/セッタ
    /// </summary>
    public ZOMBIE_STATUS State
    {
        get { return state; }
        set
        {
            ZOMBIE_STATUS tmp = state;
            // ステータス変更ごとに処理を呼び出す
            if (state == ZOMBIE_STATUS.DEAD) return;
            state = value;
            switch (state)
            {
                case ZOMBIE_STATUS.MOVE: StartCoroutine(Move()); break;
                case ZOMBIE_STATUS.STOP: Speed = 0; break;
                case ZOMBIE_STATUS.ATTACK: StartCoroutine(Attack()); break;
                case ZOMBIE_STATUS.DEAD: Dead(); break;
            }
        }
    }

    /// <summary>
    /// スピード
    /// </summary>
    private float speed;

    /// <summary>
    /// スピードのゲッタ/セッタ
    /// </summary>
    public float Speed
    {
        get { return speed; }
        set
        {
            // 移動モーションの再設定・速度の再計算をする
            speed = value;
            animator.SetFloat("Speed", speed);
            forward = transform.forward * speed * Time.deltaTime;
        }
    }

    /// <summary>
    /// 体力
    /// </summary>
    [SerializeField] private int health = 20;

    /// <summary>
    /// ゾンビタイプ
    /// </summary>
    [SerializeField] private ZOMBIE_TYPE type = default;

    /// <summary>
    /// 移動方向（前方）
    /// </summary>
    private Vector3 forward;

    /// <summary>
    /// アニメーター
    /// </summary>
    [SerializeField] private Animator animator;

    /// <summary>
    /// バリケード
    /// </summary>
    private Barricade barricade;

    /// <summary>
    /// コンポネントアタッチ時処理
    /// </summary>
    private void Reset()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        Speed = Random.Range(1.0f, 2.0f);
        State = ZOMBIE_STATUS.MOVE;
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <returns>遅延</returns>
    private IEnumerator Move()
    {
        while (state == ZOMBIE_STATUS.MOVE)
        {
            yield return null;
            transform.position += forward;
        }
    }

    /// <summary>
    /// アタック
    /// </summary>
    /// <returns></returns>
    private IEnumerator Attack()
    {
        Speed = 0;
        animator.SetBool("Attack", true);
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0); ;

        // アニメーションステータスがアタックになるまで待機
        while (!info.IsTag("Attack"))
        {
            info = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        // 攻撃する腕を振りかぶるタイミングの時間を取得
        float time = info.length / 2.0f;

        // ステータスがアタックである限りループし、攻撃が当たる都度ダメージを与える
        while (true)
        {
            yield return new WaitForSeconds(time);
            if (state == ZOMBIE_STATUS.ATTACK) break;
            barricade.Damage(1);

            yield return new WaitForSeconds(time);
            if (state == ZOMBIE_STATUS.ATTACK) break;
        }
    }

    /// <summary>
    /// 倒された時の処理
    /// </summary>
    private void Dead()
    {
        Speed = 0;
        animator.SetBool("Dead", true);
        StopAllCoroutines();
        Score.AddScore(type);
        Destroy(gameObject, 5);
    }

    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        // バリケードまで行きついたら攻撃を開始する
        if (collision.gameObject.layer == Layer.BARRICADE)
        {
            barricade = collision.gameObject.GetComponent<Barricade>();
            State = ZOMBIE_STATUS.ATTACK;
        }
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="dmg">ダメージ量</param>
    public void Damage(int dmg)
    {
        if (state == ZOMBIE_STATUS.DEAD) return;
        health -= dmg;
        if (health <= 0)
            State = ZOMBIE_STATUS.DEAD;
    }
}