using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private void Awake() {
        Debug.Log("RRRR");    
    }
    
    void Start()
    {
        Debug.Log("CSV Reader");
        List<Dictionary<string, object>> dataList = CSVReader.Read("Data_Monster");
        
        for(int i = 0; i < dataList.Count; i++){
            Debug.Log(string.Format("Monster ID: {0}, Speed : {1}, HP : {2}, PrefabPath : {3}", 
            dataList[i]["id"],dataList[i]["speed"],dataList[i]["hp"],dataList[i]["prefab"]));
        }
    }

    public void Read(){
        Debug.Log("CSV Reader");
    }

}
