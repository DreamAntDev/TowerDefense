using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Sensor : MonoBehaviour
{
    List<MonsterType> targetList = new List<MonsterType>();
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

    public List<MonsterType> GetTarget()
    {
        return this.targetList;
    }

    private void OnTriggerEnter(Collider other)
    {
        var targetObj = other.gameObject.GetComponent<MonsterType>();
        if (this.targetList.Contains(targetObj) == true)
            return;

        this.targetList.Add(targetObj);
    }
    private void OnTriggerExit(Collider other)
    {
        var targetObj = other.gameObject.GetComponent<MonsterType>();
        if (this.targetList.Contains(targetObj) == false)
            return;

        this.targetList.Remove(targetObj);
    }
}
