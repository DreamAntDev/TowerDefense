using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotter : MonoBehaviour
{
    public Transform shotPos;
    //public GameObject shotEffect;
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
        var parent = this.GetComponentInParent<Tower>();
        foreach (var hit in hits)
        {
            if (hit.transform.CompareTag("Monster") == true)
            {
                var vfx = CreateProjectile(this.bullet, shotPos.position, parent.towerIndex);
                vfx.GetComponent<ProjectileMoveScript>().SetTarget(target);
                //Instantiate(this.bullet, hit.point, Quaternion.LookRotation((target.transform.position+center) - this.shotPos.transform.position));
                break;
            }
        }
        //Debug.Log(damage);
    }

    virtual internal GameObject CreateProjectile(GameObject obj, Vector3 pos, int towerIndex)
    {
        //CreateBullet
        var vfx = MonoBehaviour.Instantiate(obj, pos, Quaternion.identity);
        var towerData = TowerData.GetData(towerIndex);
        if (towerData.projectileType == ProjectileType.Direct)
        {
            var attack = vfx.AddComponent<DirectAttack>();
            attack.damage = towerData.damage;
        }
        else if (towerData.projectileType == ProjectileType.Splash)
        {
            var attack = vfx.AddComponent<SplashAttack>();
            attack.damage = towerData.damage;
            attack.range = towerData.range;
        }

        return vfx;
    }

}
