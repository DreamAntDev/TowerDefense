using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    public GameObject testPref;
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
                if(testPref != null)
                {
                    Instantiate(testPref, spawnPos, new Quaternion());
                }
            }
        }
    }
}
