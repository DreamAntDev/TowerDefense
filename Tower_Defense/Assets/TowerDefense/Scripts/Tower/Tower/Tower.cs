using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    enum State
    {
        WaitInitialize,
        Idle,
        Attack,
        AttackWait,
    }

    public GameObject Head;
    private Animator animator;
    public Sensor sensor;
    public Shotter shotter;
    public Text nameplateText;
    public float animWaitSecond;
    //public int RPM; // 분당 발사 횟수
    public int towerIndex { get; private set; }

    public LocationGrid grid { get; set; }
    [SerializeField] private State state;
    private Dictionary<State, System.Action> StateUpdater;

    private float waitTime;
    private float lastAttackTime;
    private void Awake()
    {
        this.StateUpdater = new Dictionary<State, System.Action>();
        this.StateUpdater.Add(State.WaitInitialize, this.UpdateWaitInitialize);
        this.StateUpdater.Add(State.Idle, this.UpdateIdleState);
        this.StateUpdater.Add(State.Attack, this.UpdateAttackState);
        this.StateUpdater.Add(State.AttackWait, this.UpdateAttackWaitState);
        this.state = State.WaitInitialize;

        this.animator = this.GetComponent<Animator>();
        if (this.Head == null)
            this.Head = this.gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.StateUpdater[this.state]();

        //if (Time.time - this.lastAttackTime < this.waitTime)
        //    return;

    }
    private void LateUpdate()
    {
        
    }
    public void Initialize(int towerIndex)
    {
        this.towerIndex = towerIndex;
        var data = TowerData.GetData(towerIndex);
        waitTime = (float)60 / data.rpm;
        lastAttackTime = Time.time;
        this.state = State.Idle;
        this.nameplateText.text = data.name;
    }
    private void UpdateWaitInitialize()
    {

    }
    private void UpdateIdleState()
    {
        var targetList = this.sensor.GetTarget();
        if (targetList == null || targetList.Count == 0)
        {
            if (this.animator == null) // 애니 없는경우 헤드 회전이 Idle애니처럼 사용
            {
                this.Head.transform.Rotate(Vector3.up, 0.5f);
            }
        }
        else
        {
            this.state = State.AttackWait;
        }
    }
    private void UpdateAttackState()
    {
        if (this.animator != null)
        {
            this.animator.Play("Attack", -1, 0f);
        }
        StartCoroutine(AttackCoroutine());
        lastAttackTime = Time.time;
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
            if (this.animator != null)
            {
                this.animator.Play("Idle", -1, 0f);
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        if(this.animWaitSecond > 0)
            yield return new WaitForSeconds(this.animWaitSecond);

        var targetList = sensor.GetTarget();
        if (targetList.Count > 0)
        {
            foreach (var target in targetList)
            {
                shotter.Shot(target);
            }
        }

        yield return null;
    }

    public void OnAttack()
    {
        //var targetList = sensor.GetTarget();
        //if (targetList.Count > 0)
        //{
        //    foreach (var target in targetList)
        //    {
        //        shotter.Shot(target);
        //    }
        //}
        //else
        //{
        //    this.animator.ResetTrigger("Attack");
        //    this.animator.SetTrigger("Idle");
        //}
    }
}
