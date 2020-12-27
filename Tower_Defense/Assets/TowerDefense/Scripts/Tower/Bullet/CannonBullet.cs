using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.target == null)
            return;

        var targetPos = this.target.transform.position;
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, Time.deltaTime * 1);
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
        Debug.Log(damage);
    }
}
