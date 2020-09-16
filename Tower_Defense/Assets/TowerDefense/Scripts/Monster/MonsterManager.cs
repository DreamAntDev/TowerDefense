using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Level;
using TowerDefense.Agents.Data;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] protected List<AgentConfiguration> agents;
    private void Awake() {
        //WaveManager 데이터 전달
        LevelManager.instance.waveManager.initMonsterData += Read;
    }

    public void Read(){
        List<Dictionary<string, object>> dataList = CSVReader.Read("Data_Monster");
        
        for(int i = 0; i < dataList.Count; i++){
            Debug.Log(string.Format("Monster ID: {0}, Speed : {1}, HP : {2}, PrefabPath : {3}", 
            dataList[i]["id"],dataList[i]["speed"],dataList[i]["hp"],dataList[i]["prefab"]));
        }

        if(dataList != null){
           // WaveMonsterData(dataList);
        }
        
    }

    private List<Wave> WaveMonsterData(List<Dictionary<string, object>> dataList){
        List<Wave> waves = new List<Wave>();
        for(int i = 0; i < dataList.Count; i++){
            TimedWave tw = new TimedWave();
            List<SpawnInstruction> spawns = SpawnMonster(dataList[i]);
            tw.spawnInstructions = spawns;

            waves.Add(tw);
        }
        return waves;
    }

    private List<SpawnInstruction> SpawnMonster(Dictionary<string, object> data){
        List<SpawnInstruction> spawns = new List<SpawnInstruction>(int.Parse(data["id"].ToString()) % 5 == 0 ? 1:20);
        
        return spawns;
    }
    
}

