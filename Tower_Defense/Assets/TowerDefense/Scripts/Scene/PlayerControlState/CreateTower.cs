﻿using System.Collections;
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
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }

        public void End()
        {

        }

        void OnClick()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                var grid = hit.collider.gameObject.GetComponent<LocationGrid>();
                if (grid != null)
                {
                    Vector3 spawnPos;
                    grid.GetClosetCellPosition(hit.point, out spawnPos);
                    GameManager.Instance.CreateTower(createTowerIndex, spawnPos);
                }
                GameManager.Instance.SetVisibleGrid(false);
                PlayerControlManager.Instance.SetState(PlayerControlManager.State.Play);
            }
        }

        public void SetCreateTowerIndex(int idx)
        {
            this.createTowerIndex = idx;
        }
    }
}