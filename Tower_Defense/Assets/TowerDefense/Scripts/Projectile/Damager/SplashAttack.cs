using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashAttack : MonoBehaviour, IDamager
{
    public int damage = 1;
    public float range = 10.0f;
    public void OnHit(GameObject obj, Vector3 contactPos)
    {
        Collider[] colls = Physics.OverlapSphere(contactPos, range);
        foreach(var col in colls)
        {
            var monsterState = col.GetComponent<MonsterState>();
            if (monsterState != null)
            {
                monsterState.TakeDamage(damage);
            }
        }
    }
}
