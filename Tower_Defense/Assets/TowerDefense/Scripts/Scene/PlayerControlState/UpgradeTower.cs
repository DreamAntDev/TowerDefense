using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControlState
{
    public class UpgradeTower : IPlayerControlState
    {
        public void End()
        {
            
        }

        public void Start()
        {
            
        }

        public void Update()
        {
            
        }

        public void TowerUpgrade(int idx, Vector3 pos)
        {
            var data = TowerData.GetData(idx);
            GameObject prefab = TowerResource.Instance.GetTowerResource(data.prefabCode);
            if (prefab != null)
            {
                var obj = GameObject.Instantiate(prefab, pos, new Quaternion());
                var towerObj = obj.GetComponent<Tower>();
                if (towerObj == null)
                    Debug.LogError("ErrorSpawn!");
                else
                    towerObj.towerIndex = idx;
            }
            PlayerControlManager.Instance.SetState(PlayerControlManager.State.Play);
        }
    }
}