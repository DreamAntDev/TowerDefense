using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private int monsterCount = 20;
    private int bossIndex = 5;

    private List<Dictionary<string, object>> dataList;
    private void Awake() {
        dataList = CSVReader.Read("Data_Monster");
    }

    public void Read(){
        //Debug 용
        for(int i = 0; i < dataList.Count; i++){
            Debug.Log(string.Format("Monster ID: {0}, Speed : {1}, HP : {2}, PrefabPath : {3}", 
            dataList[i]["id"],dataList[i]["speed"],dataList[i]["hp"],dataList[i]["prefab"]));
        }
    }
    
}

