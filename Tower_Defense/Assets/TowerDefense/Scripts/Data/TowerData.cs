using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerData
{
    static TowerData instance;
    public static TowerData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TowerData();
            }

            return instance;
        }
    }
    public static TowerData GetData(int index)
    {
        if (instance == null) // 로드타이밍 수정해야함
        {
            instance = new TowerData();
            Load();
        }

        TowerData ret;
        if (data.TryGetValue(index, out ret) == false)
            return null;

        return ret;
    }

    private static Dictionary<int, TowerData> data = new Dictionary<int, TowerData>();

    public int index { get; private set; }
    public string prefabPath { get; private set; }

    public static void Load()
    {
        var list = CSVReader.Read("Tower");
        foreach(var line in list)
        {
            var towerData = new TowerData();
            int index = 0;
            if(int.TryParse(line["Index"].ToString(), out index) == false)
            {
                
            }
            towerData.index = index;
            towerData.prefabPath = (line["Prefab"].ToString());
            data.Add(index, towerData);
        }
    }
}