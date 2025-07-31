using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAction : MonoBehaviour, IEnemyAction
{
    public void Attack(Enemy self)
    {
        Debug.Log("Goblin 휘두르기 공격!");
    }
}