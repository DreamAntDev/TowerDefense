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
        var vfx = Instantiate(this.bullet, shotPos.position, Quaternion.identity);
        vfx.GetComponent<ProjectileMoveScript>().SetTarget(target);
    }
}
