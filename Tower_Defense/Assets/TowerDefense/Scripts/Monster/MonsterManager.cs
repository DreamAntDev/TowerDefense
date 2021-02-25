using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PathCreation;
using PathCreation.Examples;


namespace Monster{
    public class MonsterManager : MonoBehaviour
    {
        private int monsterCount = 20;
        private int bossIndex = 5;
        [SerializeField] private int maxLevel = 10;

        [SerializeField] private GameObject[] monsters  = null;
        [SerializeField] private PathCreator pathCreator;

        private List<List<GameObject>> monsterList;

        private List<Dictionary<string, object>> dataList;

        private int level = 0;

        private void Awake() {
            dataList = CSVReader.Read("Data_Monster");
            monsterList = new List<List<GameObject>>();
            monsterList.Add(new List<GameObject>());
           // Read();
        }

        private void Start() {
            //List Add Fool
            MonsterFoolReady();
        }

        public void MonsterFoolReady(){
            for(int i = 1; i <= maxLevel; i++){
                if(i % bossIndex == 0){
                    MonsterFoolSpawn(i, 1);
                }else{
                    MonsterFoolSpawn(i, monsterCount);
                }
            }
        }
        
        public void Read(){
            //Debug 용
            for(int i = 1; i < dataList.Count; i++){
                Debug.Log(string.Format("Monster ID: {0}, Type : {1}, Speed : {2}, HP : {3}, Coin : {4}, PrefabPath : {5}", 
                dataList[i]["id"],dataList[i]["type"],dataList[i]["speed"],dataList[i]["hp"],dataList[i]["coin"],dataList[i]["prefab"]));
            }
        }

        private void MonsterFoolSpawn(int monsterId, int cnt){
            monsterList.Add(new List<GameObject>());
            
            for(int i = 1; i <= cnt; i++){
                //MonsterObject 인스턴스 생성
                
                //생성
                GameObject monsterObject = Instantiate(monsters[(int)dataList[monsterId]["prefab"]]).gameObject;
                //위치 monsterfool
                monsterObject.transform.parent = this.transform;
                monsterObject.SetActive(false);

                monsterList[monsterId].Add(monsterObject);

                //DataInfo
                int id = Convert.ToInt32(dataList[monsterId]["id"]);
                int monsterTypeNumber = Convert.ToInt32(dataList[monsterId]["type"]);
                float speed = Convert.ToSingle(dataList[monsterId]["speed"]);
                int hp = Convert.ToInt32(dataList[monsterId]["hp"]);
                int coin = Convert.ToInt32(dataList[monsterId]["coin"]);

                if(cnt == 2){
                    monsterObject.name = "Monster " + id + " Boss";
                }else{
                    monsterObject.name = "Monster " + id + "_" + i;
                }

                MonsterConfiguration mf = 
                new MonsterConfiguration(id, monsterTypeNumber, speed, hp, coin);

                PathFollower pf = monsterObject.AddComponent<PathFollower>();
                pf.endOfPathInstruction = EndOfPathInstruction.Stop;
                pf.pathCreator = pathCreator;
                pf.SetSpeed(speed);

                TypeMonster monsterData = monsterObject.AddComponent<TypeMonster>();
                monsterData.SetMonsterInfo(mf);
            }
        }

        public void MonsterSpawn(int level){
            StartCoroutine(MonsterFoolReSpawn(level));
        }

        IEnumerator MonsterFoolReSpawn(int level){
        for(int i = 0; i < monsterList[level].Count; i++){
                monsterList[level][i].SetActive(true);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
 
