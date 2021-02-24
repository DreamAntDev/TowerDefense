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
        public Data(Dictionary<string,object> line)
        {
            this.index = int.Parse(line["Index"].ToString());
            this.prefabCode = (line["Prefab"].ToString());

            if (string.IsNullOrEmpty(line["BaseTower"].ToString()) == true)
            {
                this.isBaseTower = false; // 기본값
            }
            else
            {
                this.isBaseTower = bool.Parse(line["BaseTower"].ToString());
            }
            
            this.nextTower = new List<int>();
            if (string.IsNullOrEmpty(line["NextTower"].ToString()) == false)
            {
                var nextTowerList = (line["NextTower"].ToString()).Split(',');
                foreach (var str in nextTowerList)
                {
                    nextTower.Add(int.Parse(str));
                }
            }

            this.cost = 0;
            if (string.IsNullOrEmpty(line["Cost"].ToString()) == false)
            {
                this.cost = int.Parse(line["Cost"].ToString());
            }

            this.damage = 0;
            if(string.IsNullOrEmpty(line["Damage"].ToString())==false)
            {
                this.damage = int.Parse(line["Damage"].ToString());
            }

            this.projectileType = ProjectileType.None;
            switch (line["ProjectileType"].ToString())
            {
                case "Direct":
                    this.projectileType = ProjectileType.Direct;
                    break;
                case "Splash":
                    this.projectileType = ProjectileType.Splash;
                    break;
                default:
                    this.projectileType = ProjectileType.None;
                    break;
            }
            if (string.IsNullOrEmpty(line["Range"].ToString()) == false)
            {
                this.range = float.Parse(line["Range"].ToString());
            }
        }
        public int index { get; private set; }
        public string prefabCode { get; private set; }
        public bool isBaseTower { get; private set; }
        private List<int> nextTower = new List<int>();
        public IReadOnlyCollection<int> GetNextTowerList()
        {
            return nextTower.AsReadOnly();
        }
        public int cost { get; private set; }
        public int damage { get; private set; }
        public ProjectileType projectileType { get; private set; }
        public float range { get; private set; }
    }

    public static void Load()
    {
        data.Clear();
        var list = CSVReader.Read("Tower");
        foreach(var line in list)
        {
            Data towerData = new Data(line);
            data.Add(towerData.index, towerData);
        }
    }
}