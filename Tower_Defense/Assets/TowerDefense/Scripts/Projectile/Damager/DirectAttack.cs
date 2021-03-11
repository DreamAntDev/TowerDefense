using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectAttack : MonoBehaviour, IDamager
{
    public float damage = 1.0f;
    public void OnHit(GameObject obj, Vector3 contactPos)
    {
        var monsterState = obj.GetComponent<MonsterState>();
        if (monsterState != null)
        {
            monsterState.TakeDamage(damage);
        }
    }    
}
