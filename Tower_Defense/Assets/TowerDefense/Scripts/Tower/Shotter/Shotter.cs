using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotter : MonoBehaviour
{
    public Transform shotPos;
    public GameObject shotEffect;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public virtual void Shot(GameObject target) // Default Hit-Scan
    {
        //ShotEffect
        //GameObject effect = Instantiate(this.shotEffect, shotPos);
        //Destroy(effect, 3);

        //CreateBullet
        var col = target.GetComponent<BoxCollider>();
        var center = col.center;
        Ray ray = new Ray(this.shotPos.transform.position, (target.transform.position + center) - this.shotPos.transform.position);
        Debug.DrawRay(this.shotPos.transform.position, (target.transform.position+center) - this.shotPos.transform.position,Color.red,1.0f);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach(var hit in hits)
        {
            if (hit.transform.CompareTag("Monster") == true)
            {
                var vfx = Instantiate(this.bullet, shotPos.position, Quaternion.identity);
                vfx.GetComponent<ProjectileMoveScript>().SetTarget(target);
                //Instantiate(this.bullet, hit.point, Quaternion.LookRotation((target.transform.position+center) - this.shotPos.transform.position));
                break;
            }
        }
        //Debug.Log(damage);
    }
}
