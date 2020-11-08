using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Sensor : MonoBehaviour
{
    public string targetTag = "Monster";
    List<GameObject> targetList = new List<GameObject>();
    Tower tower = null;
    // Start is called before the first frame update
    private void Start()
    {
        tower = this.GetComponentInParent<Tower>();
        if(tower == null)
        {
            Debug.LogError(string.Format("{0} is Not Tower But Contain Sensor!", this.transform.parent.name));
        }
    }

    public void GetTarget()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals(targetTag) == false)
            return;

        var targetObj = collision.gameObject;
        if (this.targetList.Contains(targetObj) == true)
            return;

        this.targetList.Add(targetObj);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals(targetTag) == false)
            return;

        var targetObj = collision.gameObject;
        if (this.targetList.Contains(targetObj) == false)
            return;

        this.targetList.Remove(targetObj);
    }
}
