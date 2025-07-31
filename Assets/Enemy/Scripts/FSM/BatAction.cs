using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAction : MonoBehaviour, IEnemyAction
{
    public void Attack(Enemy self)
    {
        Debug.Log("Bat µ¹Áø!");
    }
}
