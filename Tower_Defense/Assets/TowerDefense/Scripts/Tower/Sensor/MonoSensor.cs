using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSensor : Sensor
{
    public List<GameObject> buffer = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetList.RemoveAll(obj => obj.activeSelf == false);
        buffer.RemoveAll(obj => obj.activeSelf == false);
    }

    public override List<GameObject> GetTarget()
    {
        if(this.targetList.Count==0)
        {
            if(this.buffer.Count>0)
            {
                int idx = Random.Range(0, this.buffer.Count - 1);
                this.targetList.Add(this.buffer[idx]);
            }
        }
        return this.targetList;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster") == false)
            return;

        if (this.buffer.Contains(other.gameObject) == true)
            return;

        this.buffer.Add(other.gameObject);
        if (this.targetList.Count == 0)
            this.targetList.Add(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Monster") == false)
            return;

        if (this.buffer.Contains(other.gameObject) == false)
            return;

        if (other.gameObject.activeSelf == false)
            OnTriggerExit(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Monster") == false)
            return;

        if (this.buffer.Contains(other.gameObject) == false)
            return;

        this.buffer.Remove(other.gameObject);
        if (this.targetList.Contains(other.gameObject) == true)
        {
            this.targetList.Clear();
        }
    }
}
