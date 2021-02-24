using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamager
{
    void OnHit(GameObject obj, Vector3 contactPos);
}

public enum ProjectileType
{
    None,
    Direct,
    Splash,
}