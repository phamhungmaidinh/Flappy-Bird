using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game/Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Bird Settings")]
    public float flapStrength;
    public float gravityScale;

    [Header("Pipe Settings")]
    public float pipeMoveSpeed;
    public float deadZone;

    [Header("Spawner Settings")]
    public float spawnRate;
    public float heightOffsetUp;
    public float heightOffsetDown;
}