using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager : SingletonBehaviour<PlayerControlManager>
{
    public enum State
    {
        None,
        Play,
        CreateTower,
        UpgradeTower,
    }
    public PlayerControlState.IPlayerControlState state { private set; get; }

    private new void Awake()
    {
        base.Awake();
        state = new PlayerControlState.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
    }

    public void SetState(State state)
    {
        this.state.End();
        switch (state)
        {
            case State.None:
                this.state = new PlayerControlState.None();
                break;
            case State.Play:
                this.state = new PlayerControlState.Play();
                break;
            case State.CreateTower:
                this.state = new PlayerControlState.CreateTower();
                break;
            case State.UpgradeTower:
                this.state = new PlayerControlState.UpgradeTower();
                break;
            default:
                this.state = new PlayerControlState.None();
                break;
        }
        this.state.Start();
    }
}
