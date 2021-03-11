using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] protected GameObject[] maps;

    [SerializeField] protected WayPoint wayPoint;

    private List<List<Vector3>> wayPointpoisitions;
    
    void Start()
    {
        wayPointpoisitions = new List<List<Vector3>>();
        SetPoistion();
        AddMap(0);
    }

    void SetPoistion(){
        //각 맵 WayPoint 좌표
        Vector3[][] poisitons = {
            new Vector3[]{
                new Vector3(-4.44f, -6.78f, -2.4f), new Vector3(-0.95f, -6.78f, -2.35f), new Vector3(1.5f, -6.68f, 4.9f), 
                new Vector3(6.53f, -6.78f, 2.39f), new Vector3(7.11f, -6.68f, -5.45f), new Vector3(9.11f, -6.68f, -3.49f),
                new Vector3(12.85f, -6.38f, -2.32f), new Vector3(13.91f, -6.28f, -2.03f)
                },
            new Vector3[]{
                new Vector3(14.97f, -4.47f, 1.26f), new Vector3(17.03f, -4.97f, 4.18f), new Vector3(22.2f, -6.4f, 6.59f), 
                new Vector3(26.54f, -6.37f, 5.48f), new Vector3(32.43f, -6.32f, 5.23f), new Vector3(33.7f, -6.37f, -2.35f),
                new Vector3(31.4f, -6.47f, -6.48f), new Vector3(33.1f, -6.32f, -11f)
                },
            new Vector3[]{
                new Vector3(33.21f, -4.92f, -12.5f), new Vector3(33.31f, -4.62f, -17.32f), new Vector3(33.2f, -3.55f, -20.88f), 
                new Vector3(31.55f, -5.37f, -25.38f), new Vector3(28.83f, -5.23f, -27.82f), new Vector3(25.51f, -5.69f, -26.23f),
                new Vector3(21.11f, -5.9f, -24.49f), new Vector3(17.91f, -5.75f, -20.42f), new Vector3(15.26f, -5.73f, -16.73f)
                },
            new Vector3[]{
                new Vector3(12.7f, -4.76f, -16.53f), new Vector3(8.11f, -5f, -18.47f), new Vector3(8.61f, -5.65f, -23.36f), 
                new Vector3(7.54f, -5.16f, -29.44f), new Vector3(5.29f, -5.14f, -29.63f), new Vector3(-2.78f, -5.77f, -25.12f),
                new Vector3(-0.24f, -6.22f, -26.16f), new Vector3(2.04f, -5.28f, -19.22f), new Vector3(1.67f, -5.38f, -13.28f)
                }
        };
        
        for(int i = 0 ; i < maps.Length; i++){
            wayPointpoisitions.Add(new List<Vector3>(poisitons[i]));
        }
    }

    public void AddMap(int idx){
        wayPoint.AddWayPoint(wayPointpoisitions[idx]);
    }
}
