using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoShotter : Shotter
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Shot(GameObject target)
    {
        //ShotEffect
        GameObject effect = Instantiate(this.shotEffect, shotPos);
        Destroy(effect, 3);

        //CreateBullet
        var bullet = Instantiate(this.bullet, shotPos.position, shotPos.rotation);
        bullet.GetComponent<Bullet>().SetTarget(target);
    }
}
