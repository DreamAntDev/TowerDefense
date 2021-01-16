using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager : SingletonBehaviour<PlayerControlManager>
{
    [HideInInspector]
    public int createTowerIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Clicked();
        }
    }
    void Clicked()
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
                var data = TowerData.GetData(createTowerIndex);
                GameObject prefab = TowerResource.Instance.GetTowerResource(data.prefabCode);
                if (prefab != null)
                {
                    var obj = GameObject.Instantiate(prefab, spawnPos, new Quaternion());
                    var towerObj = obj.GetComponent<Tower>();
                    if (towerObj == null)
                        Debug.LogError("ErrorSpawn!");
                    else
                        towerObj.towerIndex = createTowerIndex;

                    GameManager.Instance.SetVisibleGrid(false);
                }
            }

            var tower = hit.collider.gameObject.GetComponent<Tower>();
            if(tower != null)
            {
                var go = UILoader.Instance.Load("TowerUpgradePopup");
                go.GetComponent<TowerUpgradePopup>().SetList(tower.towerIndex);
            }
        }
    }
}
