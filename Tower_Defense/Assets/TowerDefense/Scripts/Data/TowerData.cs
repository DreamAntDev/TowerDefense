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
    public string prefabCode { get; private set; }
    public bool baseTower { get; private set; }
    private List<int> nextTower = new List<int>();
    public IReadOnlyCollection<int> GetNextTowerList()
    {
        return nextTower.AsReadOnly();
    }

    public static void Load()
    {
        data.Clear();
        var list = CSVReader.Read("Tower");
        foreach(var line in list)
        {
            var towerData = new TowerData();
            int index = 0;
            if(int.TryParse(line["Index"].ToString(), out index) == false)
            {
                Debug.LogErrorFormat("[Tower.csv] Index Int Parsing Error\n index : {0}",line["index"]);
                return;
            }
            towerData.index = index;
            towerData.prefabCode = (line["Prefab"].ToString());
            bool isBase;
            if (line.ContainsKey("BaseTower") == true)
            {
                if (bool.TryParse(line["BaseTower"].ToString(), out isBase) == false)
                {
                    Debug.LogErrorFormat("[Tower.csv] index({0}) BaseTower Must Use {1} or {2} or Empty", index,bool.TrueString,bool.FalseString);
                    // error
                }
                towerData.baseTower = isBase;
            }
            else
            {
                towerData.baseTower = false; // 기본값
            }
            if (line.ContainsKey("NextTower") == true)
            {
                var nextTowerList = (line["NextTower"].ToString()).Split(',');
                foreach (var str in nextTowerList)
                {
                    towerData.nextTower.Add(int.Parse(str));
                }
            }
            data.Add(index, towerData);
        }
    }
}