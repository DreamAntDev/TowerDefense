using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerControlManager : SingletonBehaviour<PlayerControlManager>
{
    public enum State
    {
        None,
        Play,
        CreateTower,
        Block,
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
        
        //OnCameraPoistion();
    }

    public PlayerControlState.IPlayerControlState SetState(State state)
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
            case State.Block:
                this.state = new PlayerControlState.Block();
                break;
            default:
                this.state = new PlayerControlState.None();
                break;
        }
        this.state.Start();
        return this.state;
    }
}
