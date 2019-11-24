using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePanicVRGameManager : MonoBehaviour
{
    GAME_STATUS state = GAME_STATUS.MENU;

    [SerializeField] private Title title;
    private bool showTitle = false;

    [SerializeField] private Message message;

    [SerializeField] private Player player;

    [SerializeField] private Timer timer;

    public GAME_STATUS State
    {
        get { return state; }
        set
        {
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

    private IEnumerator Menu()
    {
        // タイトル表示
        if (!showTitle)
        {
            yield return StartCoroutine(title.ShowTitle());
            showTitle = true;
        }

        // スタート選択表示
        message.SetMessage("Start");

        yield return StartCoroutine(player.Selected());
        message.Activate(false);

        State = GAME_STATUS.PLAY;
    }

    private IEnumerator Play()
    {
        yield return StartCoroutine(timer.CountDown());

        State = GAME_STATUS.END;
    }

    private IEnumerator End()
    {
        message.SetMessage("Game Over");

        yield return new WaitForSeconds(5);
        State = GAME_STATUS.MENU;
    }

    void Start()
    {
        State = GAME_STATUS.MENU;
    }

    void Update()
    {

    }
}
