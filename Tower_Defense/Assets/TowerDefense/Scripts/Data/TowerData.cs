using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TowerData
{
    //static TowerData instance;
    //public static TowerData Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = new TowerData();
    //        }

    //        return instance;
    //    }
    //}
    public static Data GetData(int index)
    {
        //if (instance == null) // 로드타이밍 수정해야함
        //{
        //    instance = new TowerData();
        //    Load();
        //}
        if (data.Count == 0)
            Load();

        Data ret;
        if (data.TryGetValue(index, out ret) == false)
            return null;

        return ret;
    }

    public static List<Data> GetBaseTowerList()
    {
        if (data.Count == 0)
            Load();

        List<Data> list = data.Values.Where(x => x.isBaseTower == true).ToList();
        return list;
    }

    public static List<int> GetNextTowerIndexList()
    {
        return new List<int>();
    }

    private static Dictionary<int, Data> data = new Dictionary<int, Data>();

    public class Data
    {
        public Data(int index,string prefabCode,bool isBaseTower, List<int> nextTower)
        {
            this.index = index;
            this.prefabCode = prefabCode;
            this.isBaseTower = isBaseTower;
            this.nextTower = nextTower;
        }
        public int index { get; private set; }
        public string prefabCode { get; private set; }
        public bool isBaseTower { get; private set; }
        private List<int> nextTower = new List<int>();
        public IReadOnlyCollection<int> GetNextTowerList()
        {
            return nextTower.AsReadOnly();
        }
    }

    public static void Load()
    {
        data.Clear();
        var list = CSVReader.Read("Tower");
        foreach(var line in list)
        {
            int index = 0;
            if(int.TryParse(line["Index"].ToString(), out index) == false)
            {
                Debug.LogErrorFormat("[Tower.csv] Index Int Parsing Error\n index : {0}",line["index"]);
                return;
            }

            string prefabCode = (line["Prefab"].ToString());
            bool isBase = false;
            if (line.ContainsKey("BaseTower") == true)
            {
                if(string.IsNullOrEmpty(line["BaseTower"].ToString()) == true)
                {
                    isBase = false; // 기본값
                }
                else if (bool.TryParse(line["BaseTower"].ToString(), out isBase) == false)
                {
                    Debug.LogErrorFormat("[Tower.csv] index({0}) BaseTower Must Use {1} or {2} or Empty", index,bool.TrueString,bool.FalseString);
                    // error
                }
            }

            List<int> nextTower = new List<int>();
            if (line.ContainsKey("NextTower") == true)
            {
                if (string.IsNullOrEmpty(line["NextTower"].ToString()) == false)
                {
                    var nextTowerList = (line["NextTower"].ToString()).Split(',');
                    foreach (var str in nextTowerList)
                    {
                        nextTower.Add(int.Parse(str));
                    }
                }
            }

            Data towerData = new Data(index, prefabCode, isBase, nextTower);
            data.Add(index, towerData);
        }
    }
}