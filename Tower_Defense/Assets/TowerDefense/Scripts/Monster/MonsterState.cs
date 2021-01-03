using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;

using PathCreation;
using PathCreation.Examples;

public class MonsterState : MonoBehaviour, IDamagable
{
    private TypeMonster typeMonster;

    private MonsterAnimationManager monsterAnimationManager;

    private PathFollower pf;
    private bool isDeath = false;
    public float health { get ; set ; }

    private void Start() {
        typeMonster = GetComponent<TypeMonster>();
        monsterAnimationManager = GetComponent<MonsterAnimationManager>();
        pf = GetComponent<PathFollower>();
        health = typeMonster.GetHP();
    } 

    void IDamagable.Initialize()
    {
        health = typeMonster.GetHP();
        Debug.Log("Idamable");
    }

    public void TakeDamage(float damage){
        health -= damage;
        Debug.Log(gameObject.name + " : " + health);
        
        if(health <= 0 && !isDeath){
            isDeath = true;
            Death();
        }
    }

    public void Death(){
        Debug.Log("Death");
        monsterAnimationManager.Death();
        GameManager.Instance.MonsterCoin(typeMonster.GetCoin());
        StartCoroutine(MonsterDeath());
    }   

    IEnumerator MonsterDeath(){
        pf.speed = 0;
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }
}
