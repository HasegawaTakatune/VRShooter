using System.Collections;
using UnityEngine;

/// <summary>
/// ゲームマネージャー
/// </summary>
public class ZombiePanicVRGameManager : MonoBehaviour
{
    /// <summary>
    /// ゲームステータス
    /// </summary>
    GAME_STATUS state = GAME_STATUS.MENU;

    /// <summary>
    /// タイトルUI
    /// </summary>
    [SerializeField] private Title title = default;

    /// <summary>
    /// タイトル表示判定
    /// </summary>
    private bool showTitle = false;

    /// <summary>
    /// メッセージUI
    /// </summary>
    [SerializeField] private Message message = default;

    /// <summary>
    /// プレイヤ
    /// </summary>
    [SerializeField] private Player player = default;

    /// <summary>
    /// タイマー
    /// </summary>
    [SerializeField] private Timer timer = default;

    /// <summary>
    /// リザルト
    /// </summary>
    [SerializeField] private Result result = default;

    /// <summary>
    /// スポナ
    /// </summary>
    [SerializeField] private ZombieSpawner spawner = default;

    /// <summary>
    /// ゲームステータスのゲッタ/セッタ
    /// </summary>
    public GAME_STATUS State
    {
        get { return state; }
        set
        {
            // ステータスごとに処理を呼び出す
            state = value;
            switch (state)
            {
                case GAME_STATUS.MENU: StartCoroutine(Menu()); break;
                case GAME_STATUS.PLAY: StartCoroutine(Play()); break;
                case GAME_STATUS.END: StartCoroutine(End()); break;
                default: break;
            }
        }
    }

    /// <summary>
    /// メニュー
    /// </summary>
    /// <returns>遅延</returns>
    private IEnumerator Menu()
    {
        // タイトル表示
        if (!showTitle)
        {
            yield return StartCoroutine(title.ShowTitle());
            showTitle = true;
        }

        // スタート選択表示
        // プレイヤがスタートを選択するまで待機する
        message.SetMessage("Start");
        yield return StartCoroutine(player.Selected());
        message.Activate(false);

        spawner.Active = true;
        State = GAME_STATUS.PLAY;
    }

    /// <summary>
    /// ゲームプレイ
    /// </summary>
    /// <returns>遅延</returns>
    private IEnumerator Play()
    {
        // ワープゾーンの選択を可能とする
        StartCoroutine(player.SelectWarpZone());

        // タイムオーバーになるまで待機
        yield return StartCoroutine(timer.CountDown(120));

        StopCoroutine(player.SelectWarpZone());
        spawner.Active = false;
        State = GAME_STATUS.END;
    }

    /// <summary>
    /// ゲームエンド
    /// </summary>
    /// <returns>遅延</returns>
    private IEnumerator End()
    {
        message.SetMessage("Game Over");
        yield return new WaitForSeconds(1.0f);
        message.Activate(false);

        yield return StartCoroutine(result.ShowResult());

        yield return new WaitForSeconds(5);
        State = GAME_STATUS.MENU;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        State = GAME_STATUS.MENU;
    }
}
