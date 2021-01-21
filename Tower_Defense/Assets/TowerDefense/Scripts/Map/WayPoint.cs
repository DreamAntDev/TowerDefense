using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] protected Monster.TowerMonsterPath path;

    [SerializeField] protected GameObject wayPoint;

    private List<Transform> waypoints;

    private string pointName = "WayPoint";

    private int[] pointIdx = {0, 0, 0, 0};

    private int idx = 0;
    
    private void Start() {
        waypoints = new List<Transform>();    
    }

    public void AddWayPoint(List<Vector3> pointlist){
        //존재하는 맵 웨이포인트 삭제 후 생성
        if(++idx > 4){
            idx = 0;
            RemovePoint(idx);
        }

        //위치값에 해당하는 위치에 현재 부모 밑으로 생성
        for(int i = 0; i < pointlist.Count; i++){
            GameObject obj = Instantiate (wayPoint, pointlist[i], Quaternion.identity);
            obj.name = pointName;
            obj.transform.SetParent(this.transform);
            waypoints.Add(obj.transform);
        }

        path.SetWayPoint(waypoints.ToArray());
    }

    public void RemovePoint(int idx){
        //GameObject 삭제
        for(int i = 0; i < pointIdx[idx]; i++){
            Destroy(waypoints[i].gameObject);
        }

        //주소 삭제
        waypoints.RemoveRange(0, pointIdx[idx]);
    }
}
