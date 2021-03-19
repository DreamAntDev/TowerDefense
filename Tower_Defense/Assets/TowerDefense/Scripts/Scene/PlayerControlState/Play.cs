﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControlState
{
    public class Play : IPlayerControlState
    {
        [SerializeField] protected Camera camera;
        public GameObject towerObject { get; private set; }
        private Vector3[] mapVector = {new Vector3(4.3f, 22f, -16.5f), new Vector3(24.3f, 22f, -16.5f),
                                    new Vector3(24.3f, 22f, -37f), new Vector3(4.3f, 22f, -37f)};

        private Vector2 touchDownPosition = Vector2.zero;
        private Vector2 touchUpPosition = Vector2.zero;
        private float dragX = 0f;
        private float dragY = 0f;

        private int mapIdx = 0;

        public void End()
        {

        }

        public void Start()
        {

        }

        public void Update()
        {
            OnCameraPoistion();
            OnClick();
        }

        public void OnCameraPoistion()
        {
            //추가 맵 전까지 변경 불가 
            /*if(gameLevel <= 5){
                return;
            }*/
#if UNITY_ANDROID && !UNITY_EDITOR
            if (Input.touchCount <= 0)
                return;
            
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchDownPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchUpPosition = touch.position;

                dragX = touchUpPosition.x - touchDownPosition.x;
                dragY = touchUpPosition.y - touchDownPosition.y;

                OnDragMap(dragX, dragY);
            }
#else
            if (true == Input.GetMouseButtonDown(0))
            {
                touchDownPosition = Input.mousePosition;
            }
            else if (true == Input.GetMouseButtonUp(0))
            {
                touchUpPosition = Input.mousePosition;

                dragX = touchUpPosition.x - touchDownPosition.x;
                dragY = touchUpPosition.y - touchDownPosition.y;

                OnDragMap(dragX, dragY);
            }
#endif
        }

        public void OnDragMap(float dragX, float dragY)
        {
            int gameLevel = GameManager.Instance.GetLevel();
            bool isDragX = Mathf.Abs(dragX) > 300;
            bool isDragY = Mathf.Abs(dragY) > 200;

            if (isDragX)
            {
                if (isDragY)
                {
                    return;
                }
                else
                {
                    //X축 이동
                    if (dragX > 0)
                    {
                        //<----
                        switch (mapIdx)
                        {
                            case 1:
                                MoveCamera(0);
                                break;
                            case 2:
                                if (gameLevel > 15)
                                {
                                    MoveCamera(3);
                                }
                                break;
                        }

                    }
                    else
                    {
                        //--->
                        switch (mapIdx)
                        {
                            case 0:
                                if (gameLevel > 5)
                                {
                                    MoveCamera(1);
                                }
                                break;
                            case 3:
                                MoveCamera(2);
                                break;
                        }
                    }
                }
            }
            else
            {
                if (isDragY)
                {
                    //Y축 이동
                    if (dragY > 0)
                    {
                        //
                        switch (mapIdx)
                        {
                            case 0:
                                if (gameLevel > 20)
                                {
                                    MoveCamera(3);
                                }
                                break;
                            case 1:
                                if (gameLevel > 10)
                                {
                                    MoveCamera(2);
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (mapIdx)
                        {
                            case 2:
                                MoveCamera(1);
                                break;
                            case 3:
                                MoveCamera(0);
                                break;
                        }
                    }
                }
                else
                {
                    //드래그 미달
                    return;
                }
            }
        }

        private void MoveCamera(int idx)
        {
            camera.transform.position = mapVector[idx];
            mapIdx = idx;
        }
        void OnClick()
        {
            Vector3 screenPos = Vector3.zero;
#if UNITY_ANDROID && !UNITY_EDITOR
            if(Input.touchCount <= 0)
            {
                return;
            }
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                screenPos = touch.position;
            }
            else
            {
                return;
            }
#else
            if (Input.GetMouseButtonUp(0) == false)
                return;

            screenPos = Input.mousePosition;
#endif

            var ray = Camera.main.ScreenPointToRay(screenPos);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                var tower = hit.collider.gameObject.GetComponent<Tower>();
                if (tower != null)
                {
                    PlayerControlState.Block state = PlayerControlManager.Instance.SetState(PlayerControlManager.State.Block) as PlayerControlState.Block;
                    state.towerObject = hit.collider.gameObject;
                    var go = UILoader.Instance.Load("TowerUpgradePopup");
                    go.GetComponent<TowerUpgradePopup>().SetList(tower.towerIndex);
                }
            }
        }
    }
}