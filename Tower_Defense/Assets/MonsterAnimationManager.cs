using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class MonsterAnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator anim;

    public string skillName;

    public float skillHoldingTime;

    public float skillWaitTime;
    public float skillCooldown;

    private bool isColldown = false;

    private PathFollower pf;

    void Start()
    {
        if(anim == null){
            anim = GetComponent<Animator>();
        }
        pf = GetComponent<PathFollower>();
        Walk();
    }

    public void Walk(){
        pf.speed = 1;
        anim.SetTrigger("isWalking");
    }

    public void Run(){

    }

    public void Death(){
        anim.SetBool("isDead", true);
        
    }

    //Skill이 발생 시 데이터화를 받아서 여기서 정리해서 발생하는 것으로
    public void Skill(){
        if(!isColldown){
            isColldown = true;
            anim.SetTrigger("isSkill");
            StartCoroutine(SetupSkill());
        }
    }
    
    IEnumerator SetupSkill(){
    
        anim.SetBool(skillName, true);
        pf.speed = 1.2f;
        yield return new WaitForSeconds(skillHoldingTime);
        anim.SetBool(skillName , false);
        pf.speed = 0f;
        yield return new WaitForSeconds(skillWaitTime);
        Walk();
        yield return new WaitForSeconds(skillCooldown);
        isColldown = false;

        yield return null;
    }
}
