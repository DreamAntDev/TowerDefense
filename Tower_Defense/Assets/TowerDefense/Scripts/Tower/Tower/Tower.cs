using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Sensor sensor;
    public Shotter shotter;
    public int RPM; // 분당 발사 횟수

    private float delayTime;
    private float lastAttackTime;
    // Start is called before the first frame update
    void Start()
    {
        lastAttackTime = Time.time;
        delayTime = (float)60 / RPM;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - this.lastAttackTime < this.delayTime)
            return;

        var targetList = sensor.GetTarget();
        if (targetList.Count > 0)
        {
            this.lastAttackTime = Time.time;
            foreach (var target in targetList)
            {
                shotter.Shot(target);
            }
        }
    }
}
