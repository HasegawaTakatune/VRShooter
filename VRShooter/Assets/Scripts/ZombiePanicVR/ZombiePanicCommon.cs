using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePanicCommon : MonoBehaviour
{
}

public enum GAME_STATUS
{
    MENU = 0,
    PLAY,
    END,
    LENGTH
}

public enum ZOMBIE_STATUS
{
    MOVE = 0,
    STOP,
    ATTACK,
    DEAD,
    LENGTH
}

public class Layer
{
    public const int BARRICADE = 11;
}