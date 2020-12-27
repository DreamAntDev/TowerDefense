using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public GameObject hitEffect;
    public GameObject target { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster") == false)
            return;

        GameObject effect = Instantiate(this.hitEffect, this.transform.position, this.transform.rotation);
        Destroy(effect, 3);
        Destroy(this.gameObject);
        other.GetComponent<MonsterState>().TakeDamage(damage);
        //Debug.Log(damage);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
