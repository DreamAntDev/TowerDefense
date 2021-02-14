﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectAttack : MonoBehaviour, IDamager
{
    public int damage = 1;
    public void OnHit(GameObject obj)
    {
        var monsterState = obj.GetComponent<MonsterState>();
        if (monsterState != null)
        {
            monsterState.TakeDamage(damage);
        }
    }    
}
