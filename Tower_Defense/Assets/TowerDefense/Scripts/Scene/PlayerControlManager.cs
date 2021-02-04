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
        UpgradeTower,
    }
    
    public PlayerControlState.IPlayerControlState state { private set; get; }


    private Vector3[] mapVector = {new Vector3(4.3f, 22f, -16.5f), new Vector3(24.3f, 22f, -16.5f),
                                    new Vector3(24.3f, 22f, -37f), new Vector3(4.3f, 22f, -37f)};

    private Vector2 touchDownPosition = Vector2.zero;
    private Vector2 touchUpPosition = Vector2.zero;
    private float dragX = 0f;
    private float dragY = 0f;

    private int mapIdx = 0;

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
        OnCameraPoistion();
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

    public void OnCameraPoistion(){
        int gameLevel = GameManager.Instance.GetLevel();

        //추가 맵 전까지 변경 불가 
        /*if(gameLevel <= 5){
            return;
        }*/
        

    #if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0)){
            touchDownPosition = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0)){
            touchUpPosition = Input.mousePosition;

            dragX = touchUpPosition.x - touchDownPosition.x;
            dragY = touchUpPosition.y - touchDownPosition.y;
            Debug.Log("DRAG ] = " + dragX + " : " + dragY);

            OnDragMap(dragX, dragY);
            
        }
    #else
        if(Input.touchCount > 0){
            touchDownPosition = Input.GetTouchDown(0).position;
        }
        if(Input.touchCount > 0){
            touchUpPosition = Input.GetTouchUp(0).position;

            dragX = touchUpPosition.x - touchDownPosition.x;
            dragZ = touchUpPosition.y - touchDownPosition.y;
            Debug.Log("DRAG ] = " + dragX + " : " + dragY);

             OnDragMap(dragX, dragY);
        }
    #endif
        
    }

    public void OnDragMap(float dragX, float dragY){
        bool isDragX = Mathf.Abs(dragX) > 300;
        bool isDragY = Mathf.Abs(dragY) > 200;

        if(isDragX){
            if(isDragY){
                return;
            }else{
                //X축 이동
                if(dragX > 0){
                    //<----
                     switch(mapIdx){
                        case 1:
                            MoveCamera(0);
                            break;
                        case 2:
                            MoveCamera(3);
                            break;
                    }
                    
                }else{
                    //--->
                   switch(mapIdx){
                        case 0:
                            MoveCamera(1);
                            break;
                        case 3:
                            MoveCamera(2);
                            break;
                    }
                }
            }
        }else{
            if(isDragY){
                //Y축 이동
                if(dragY > 0){
                    //
                    switch(mapIdx){
                        case 0:
                            MoveCamera(3);
                            break;
                        case 1:
                            MoveCamera(2);
                            break;
                    }
                }else{
                    switch(mapIdx){
                        case 2:
                            MoveCamera(1);
                            break;
                        case 3:
                            MoveCamera(0);
                            break;
                    }
                }
            }else{
                //드래그 미달
                return;
            }
        }
    }

    private void MoveCamera(int idx){
        this.transform.position = mapVector[idx];
        mapIdx = idx;
    }
}
