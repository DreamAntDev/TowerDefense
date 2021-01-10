using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerResource : SingletonBehaviour<TowerResource>
{
    [System.Serializable]
    struct Tower
    {
        public string code;
        public GameObject prefab;
    }
    [SerializeField]
    private List<Tower> prefabList;
    private Dictionary<string, GameObject> prefabDictionary;
    private new void Awake()
    {
        base.Awake();
        prefabDictionary = new Dictionary<string, GameObject>();
        foreach(var tower in prefabList)
        {
            if (prefabDictionary.ContainsKey(tower.code) == false)
            {
                prefabDictionary.Add(tower.code, tower.prefab);
            }
            else
            {
                Debug.LogErrorFormat("[TowerResource] code : {0} is Duplicate", tower.code);
            }
        }
    }

    public GameObject GetTowerResource(string code)
    {
        GameObject obj;
        if(prefabDictionary.TryGetValue(code, out obj) == false)
        {
            Debug.LogErrorFormat("{0} is NotExist in TowerResource", code);
            return null;
        }
        return obj;
    }
}
