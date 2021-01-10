using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    enum State
    {
        Idle,
        Attack,
        AttackWait,
    }

    public GameObject Head;
    public Sensor sensor;
    public Shotter shotter;
    public int RPM; // 분당 발사 횟수

    [SerializeField] private State state;
    private Dictionary<State, System.Action> StateUpdater;

    private float waitTime;
    private float lastAttackTime;
    private void Awake()
    {
        this.StateUpdater = new Dictionary<State, System.Action>();
        this.StateUpdater.Add(State.Idle, this.UpdateIdleState);
        this.StateUpdater.Add(State.Attack, this.UpdateAttackState);
        this.StateUpdater.Add(State.AttackWait, this.UpdateAttackWaitState);
    }
    // Start is called before the first frame update
    void Start()
    {
        this.state = State.Idle;
        lastAttackTime = Time.time;
        waitTime = (float)60 / RPM;
    }

    // Update is called once per frame
    void Update()
    {
        this.StateUpdater[this.state]();

        if (Time.time - this.lastAttackTime < this.waitTime)
            return;

    }
    private void LateUpdate()
    {
        
    }

    private void UpdateIdleState()
    {
        var targetList = this.sensor.GetTarget();
        if (targetList == null || targetList.Count == 0)
        {
            this.Head.transform.Rotate(Vector3.up, 0.5f);
        }
        else
        {
            this.state = State.AttackWait;
        }
    }
    private void UpdateAttackState()
    {
        var targetList = sensor.GetTarget();
        if (targetList.Count > 0)
        {
            this.lastAttackTime = Time.time;
            foreach (var target in targetList)
            {
                shotter.Shot(target);
            }
        }

        this.state = State.AttackWait;
    }
    
    private void UpdateAttackWaitState()
    {
        var targetList = sensor.GetTarget();
        if (targetList.Count > 0)
        {
            var lookPos = targetList[0].transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            //this.Head.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
            this.Head.transform.rotation = rotation;
            if (Time.time - this.lastAttackTime >= this.waitTime)
            {
                this.state = State.Attack;
            }
        }
        else
        {
            this.state = State.Idle;
        }
    }
}
