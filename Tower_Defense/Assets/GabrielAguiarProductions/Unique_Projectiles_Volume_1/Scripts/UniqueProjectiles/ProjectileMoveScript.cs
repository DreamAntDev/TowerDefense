//
//NOTES:
//This script is used for DEMONSTRATION porpuses of the Projectiles. I recommend everyone to create their own code for their own projects.
//This is just a basic example.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoveScript : MonoBehaviour
{
    public float speed;
	//[Tooltip("From 0% to 100%")]
	//public float accuracy;
	public float fireRate;
	public GameObject muzzlePrefab;
	public GameObject hitPrefab;
	public AudioClip shotSFX;
	public AudioClip hitSFX;
	public List<GameObject> trails;
    public float forceMagnitude;

    private Vector3 startPos;
	//private Vector3 offset;
	private bool collided;
	private Rigidbody rb;
    private GameObject target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start () {
        startPos = transform.position;

		if (muzzlePrefab != null) {
			var muzzleVFX = Instantiate (muzzlePrefab, transform.position, Quaternion.identity);
			muzzleVFX.transform.forward = gameObject.transform.forward /*+ offset*/;
			var ps = muzzleVFX.GetComponent<ParticleSystem>();
			if (ps != null)
				Destroy (muzzleVFX, ps.main.duration);
			else {
				var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
				Destroy (muzzleVFX, psChild.main.duration);
			}
		}

		if (shotSFX != null && GetComponent<AudioSource>()) {
			GetComponent<AudioSource> ().PlayOneShot (shotSFX);
		}
	}

	void LateUpdate () {

        if (target != null)
        {
            this.transform.position += ((target.transform.position - this.transform.position).normalized * (speed * Time.deltaTime));
            if (target.activeSelf == false)
            {
                SetTarget(null);
                if (hitPrefab != null)
                {
                    var hitVFX = Instantiate(hitPrefab, this.transform.position, this.transform.rotation) as GameObject;

                    var ps = hitVFX.GetComponent<ParticleSystem>();
                    if (ps == null)
                    {
                        var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                        Destroy(hitVFX, psChild.main.duration);
                    }
                    else
                        Destroy(hitVFX, ps.main.duration);
                }

                StartCoroutine(DestroyParticle(0f));
            }
            //rotateToMouse.RotateToMouse(gameObject, target.transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("Monster") == false &&
            obj.CompareTag("Bullet") == false &&
            obj.CompareTag("BulletCollideObject") == false &&
            collided == false)
            return;

        collided = true;

        if (shotSFX != null && GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(hitSFX);
        }

        if (trails.Count > 0)
        {
            for (int i = 0; i < trails.Count; i++)
            {
                trails[i].transform.parent = null;
                var ps = trails[i].GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    ps.Stop();
                    Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
                }
            }
        }

        speed = 0;
        GetComponent<Rigidbody>().isKinematic = true;

        Vector3 contact = other.ClosestPointOnBounds(this.transform.position);
        Quaternion rot = this.transform.rotation;
        Vector3 pos = contact;
        var damager = this.gameObject.GetComponent<IDamager>();
        if (damager != null)
        {
            damager.OnHit(obj,pos);
        }
        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, pos, rot) as GameObject;

            var ps = hitVFX.GetComponent<ParticleSystem>();
            if (ps == null)
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
            else
                Destroy(hitVFX, ps.main.duration);
        }

        StartCoroutine(DestroyParticle(0f));
    }
    //void OnCollisionEnter(Collision co)
    //{
    //    if (co.gameObject.CompareTag("Monster") == false)
    //        return;

    //    co.gameObject.GetComponent<MonsterState>().TakeDamage(1);

    //    if (co.gameObject.tag != "Bullet" && !collided)
    //    {
    //        collided = true;

    //        if (shotSFX != null && GetComponent<AudioSource>())
    //        {
    //            GetComponent<AudioSource>().PlayOneShot(hitSFX);
    //        }

    //        if (trails.Count > 0)
    //        {
    //            for (int i = 0; i < trails.Count; i++)
    //            {
    //                trails[i].transform.parent = null;
    //                var ps = trails[i].GetComponent<ParticleSystem>();
    //                if (ps != null)
    //                {
    //                    ps.Stop();
    //                    Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
    //                }
    //            }
    //        }

    //        speed = 0;
    //        GetComponent<Rigidbody>().isKinematic = true;

    //        ContactPoint contact = co.contacts[0];
    //        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
    //        Vector3 pos = contact.point;

    //        if (hitPrefab != null)
    //        {
    //            var hitVFX = Instantiate(hitPrefab, pos, rot) as GameObject;

    //            var ps = hitVFX.GetComponent<ParticleSystem>();
    //            if (ps == null)
    //            {
    //                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
    //                Destroy(hitVFX, psChild.main.duration);
    //            }
    //            else
    //                Destroy(hitVFX, ps.main.duration);
    //        }

    //        StartCoroutine(DestroyParticle(0f));
    //    }
    //}

	public IEnumerator DestroyParticle (float waitTime) {

		if (transform.childCount > 0 && waitTime != 0) {
			List<Transform> tList = new List<Transform> ();

			foreach (Transform t in transform.GetChild(0).transform) {
				tList.Add (t);
			}		

			while (transform.GetChild(0).localScale.x > 0) {
				yield return new WaitForSeconds (0.01f);
				transform.GetChild(0).localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
				for (int i = 0; i < tList.Count; i++) {
					tList[i].localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
				}
			}
		}
		
		yield return new WaitForSeconds (waitTime);
		Destroy (gameObject);
	}

    public void SetTarget (GameObject trg, RotateToMouseScript rotateTo)
    {
        target = trg;
    }

    public void SetTarget(GameObject trg)
    {
        target = trg;
    }
}
