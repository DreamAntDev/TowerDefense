using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Plane))]
[ExecuteAlways]
public class LocationGrid : MonoBehaviour
{
    const int cellSize = 1;
    public int row = 2; // 가로라인
    public int column = 3; // 세로라인
    public Tower tower { get; set; }
    private Vector3[,] cellGrid;
    private void Awake()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
#endif
        {
            InitList();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        tower = null;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Application.isPlaying == false)
        {
            this.transform.localScale = new Vector3(column / (float)10, 1, row / (float)10);
        }
#endif
    }

    private void InitList()
    {
        Vector3 leftTopCenter = this.transform.position - new Vector3(column / (float)2 - 0.5f, 0, row / (float)2 - 0.5f);
        cellGrid = new Vector3[row, column];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                cellGrid[i,j] = new Vector3(leftTopCenter.x + (j * 1), leftTopCenter.y, leftTopCenter.z + (i * 1));
            }
        }
    }

    public void GetClosetCellPosition(Vector3 worldPosition, out Vector3 cellPosition)
    {
        cellPosition = new Vector3();
        if (cellGrid == null || cellGrid.Length == 0)
        {
            return;
        }

        float maxLength = float.MaxValue;
        foreach (var cell in cellGrid)
        {
            float len = (worldPosition - cell).magnitude;
            if (len < maxLength)
            {
                cellPosition = cell;
                maxLength = len;
            }
        }
    }
}
