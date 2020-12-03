using UnityEngine;
using PathCreation;

namespace Monster{
    public class TowerMonsterPath : MonoBehaviour
    {
        public bool closedLoop = true;
        public Transform[] waypoints;

        void Start () {
            if (waypoints.Length > 0) {
                // Create a new bezier path from the waypoints.
                BezierPath bezierPath = new BezierPath (waypoints, closedLoop, PathSpace.xyz);
                bezierPath.GlobalNormalsAngle = 80f;
                GetComponent<PathCreator> ().bezierPath = bezierPath;
            }
        }
    }
}
