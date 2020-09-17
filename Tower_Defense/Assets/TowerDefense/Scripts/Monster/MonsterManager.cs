using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Level;
using TowerDefense.Agents.Data;
using TowerDefense.Nodes;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] protected List<AgentConfiguration> agents;
    [SerializeField] protected Node startingNode;

    private int monsterCount = 20;
    private int bossIndex = 5;

    private List<Dictionary<string, object>> dataList;
    private void Awake() {
        //WaveManager 데이터 전달
        LevelManager.instance.waveManager.initMonsterData += Read;
        dataList = CSVReader.Read("Data_Monster");
    }

    public void Read(){
        //Debug 용
        for(int i = 0; i < dataList.Count; i++){
            Debug.Log(string.Format("Monster ID: {0}, Speed : {1}, HP : {2}, PrefabPath : {3}", 
            dataList[i]["id"],dataList[i]["speed"],dataList[i]["hp"],dataList[i]["prefab"]));
        }

        if(dataList != null){
           LevelManager.instance.waveManager.waves = WaveMonsterData(dataList);
        }
    }

    private List<Wave> WaveMonsterData(List<Dictionary<string, object>> dataList){
        List<Wave> waves = new List<Wave>();
        List<SpawnInstruction> spawns;
        TimedWave tw;

        for(int i = 0; i < dataList.Count; i++){
            tw = this.gameObject.AddComponent<TimedWave>();
            spawns = SpawnMonster(i, dataList[i]);
            tw.spawnInstructions = spawns;
            tw.startingNode = startingNode;
            waves.Add(tw);
        }
        return waves;
    }

    private List<SpawnInstruction> SpawnMonster(int idx, Dictionary<string, object> data){
        List<SpawnInstruction> spawns;
        SpawnInstruction spawn = new SpawnInstruction();
        if(idx + 1 % bossIndex == 0){
            spawns = new List<SpawnInstruction>();
            spawn.agentConfiguration = agents[idx];
            spawn.delayToSpawn = 0f;
            spawns.Add(spawn);
        }else{
            spawns = new List<SpawnInstruction>();
            //단일 몹 생성
            for(int i = 0; i < monsterCount; i++){
                spawn.agentConfiguration = agents[idx];
                spawn.delayToSpawn = 1.5f;
                spawns.Add(spawn);
            }
            //다양 몹 생성은 각 번호에 맞게 따로 뿌려야 할듯.
        }
        return spawns;
    }
    
}

