using UnityEngine;
using PathCreation;

namespace Monster{
    public class TowerMonsterPath : MonoBehaviour
    {
        public bool closedLoop = true;
        private Transform[] waypoints;

        public void SetWayPoint(Transform[] waypoints){
            this.waypoints = waypoints;
            SetPathPoint();
        }

        public void SetPathPoint(){
            if (waypoints.Length > 0) {
                // Create a new bezier path from the waypoints.
                BezierPath bezierPath = new BezierPath (waypoints, closedLoop, PathSpace.xyz);
                bezierPath.GlobalNormalsAngle = 80f;
                GetComponent<PathCreator> ().bezierPath = bezierPath;
            }
        }
    }
}
