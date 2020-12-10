using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void Initialize();
    float health {get; set;}
    void TakeDamage(float damage);
}
