using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerJS : MonoBehaviour
{
    public static GameManagerJS Instance;
    public PlayerMovement player;
    public PoolManager pool;

    private void Awake()
    {
        Instance = this;
    }
}
