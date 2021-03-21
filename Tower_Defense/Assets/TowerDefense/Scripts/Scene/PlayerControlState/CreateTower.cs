using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControlState
{
    public class CreateTower : IPlayerControlState
    {
        int createTowerIndex = 0;
        public void Start()
        {
            GameManager.Instance.SetVisibleGrid(true);
        }

        public void Update()
        {

            OnClick();

        }

        public void End()
        {

        }

        void OnClick()
        {
            Vector2 screenPos;
#if UNITY_ANDROID && !UNITY_EDITOR
            if (Input.touchCount <= 0)
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
                var grid = hit.collider.gameObject.GetComponent<LocationGrid>();
                if (grid != null)
                {
                    Vector3 spawnPos;
                    grid.GetClosetCellPosition(hit.point, out spawnPos);
                    GameManager.Instance.CreateTower(createTowerIndex, spawnPos);

                    GameManager.Instance.SetVisibleGrid(false);
                    PlayerControlManager.Instance.SetState(PlayerControlManager.State.Play);
                }
            }
        }

        public void SetCreateTowerIndex(int idx)
        {
            this.createTowerIndex = idx;
        }
    }
}