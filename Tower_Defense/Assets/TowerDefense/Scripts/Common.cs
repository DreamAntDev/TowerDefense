using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Common
{
    public static GameObject CreateProjectile(GameObject obj, Vector3 pos, int towerIndex)
    {
        //CreateBullet
        var vfx = MonoBehaviour.Instantiate(obj, pos, Quaternion.identity);
        var towerData = TowerData.GetData(towerIndex);
        if (towerData.projectileType == ProjectileType.Direct)
        {
            var attack = vfx.AddComponent<DirectAttack>();
            attack.damage = towerData.damage;
        }
        else if (towerData.projectileType == ProjectileType.Splash)
        {
            var attack = vfx.AddComponent<SplashAttack>();
            attack.damage = towerData.damage;
            attack.range = towerData.range;
        }

        return vfx;
    }
}
