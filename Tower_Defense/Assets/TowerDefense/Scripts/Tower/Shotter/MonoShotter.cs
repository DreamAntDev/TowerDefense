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
        //CreateBullet
        var parent = this.GetComponentInParent<Tower>();
        var vfx = CreateProjectile(this.bullet, shotPos.position, parent.towerIndex);
        vfx.GetComponent<ProjectileMoveScript>().SetTarget(target);
    }
}
