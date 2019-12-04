using UnityEngine;


public class ZombiePanicCommon : MonoBehaviour
{
}

/// <summary>
/// ゲームステータス列挙
/// </summary>
public enum GAME_STATUS
{
    MENU = 0,
    PLAY,
    END,
    LENGTH
}

/// <summary>
/// ゾンビステータス列挙
/// </summary>
public enum ZOMBIE_STATUS
{
    MOVE = 0,
    STOP,
    ATTACK,
    DEAD,
    LENGTH
}

/// <summary>
/// ゾンビタイプ列挙
/// </summary>
public enum ZOMBIE_TYPE
{
    NORMAL = 0,
    TANK,
    LENGTH
}

/// <summary>
/// レイヤ
/// </summary>
public class Layer
{
    /// <summary>
    /// バリケード
    /// </summary>
    public const int BARRICADE = 11;
}