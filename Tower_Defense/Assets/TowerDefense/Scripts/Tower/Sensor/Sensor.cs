using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Sensor : MonoBehaviour
{
    public List<GameObject> targetList  = new List<GameObject>();
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

    public virtual List<GameObject> GetTarget()
    {
        return this.targetList;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster") == false)
            return;

        if (this.targetList.Contains(other.gameObject) == true)
            return;

        this.targetList.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Monster") == false)
            return;

        if (this.targetList.Contains(other.gameObject) == false)
            return;

        this.targetList.Remove(other.gameObject);
    }
}
