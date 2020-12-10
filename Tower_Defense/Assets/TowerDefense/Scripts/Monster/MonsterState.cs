using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;

public class MonsterState : MonoBehaviour, IDamagable
{
    private TypeMonster typeMonster;

    public float health { get ; set ; }

    private void Start() {
        typeMonster = GetComponent<TypeMonster>();
    } 

    void IDamagable.Initialize()
    {
        health = typeMonster.GetHP();
    }

    public void TakeDamage(float damage){
        health -= damage;
        Debug.Log(health);
        if(health <= 0){
            Death();
        }
    }

    public void Death(){
        Debug.Log("Death");
        typeMonster.GetCoin();
    }

    
}
