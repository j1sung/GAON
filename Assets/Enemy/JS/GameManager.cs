using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player_JS player;
    public PoolManager pool;

    private void Awake()
    {
        Instance = this;
    }
}
