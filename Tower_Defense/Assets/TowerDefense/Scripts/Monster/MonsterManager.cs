using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monster{
    public class MonsterManager : MonoBehaviour
    {
        private int monsterCount = 20;
        private int bossIndex = 5;

        [SerializeField] TypeMonster[] monsters;


        private List<Dictionary<string, object>> dataList;
        private void Awake() {
            dataList = CSVReader.Read("Data_Monster");
        }

        private void Start() {
            //List Add Fool
            StartCoroutine(TestSpawn());
        }

        public void Read(){
            //Debug 용
            for(int i = 0; i < dataList.Count; i++){
                Debug.Log(string.Format("Monster ID: {0}, Speed : {1}, HP : {2}, PrefabPath : {3}", 
                dataList[i]["id"],dataList[i]["speed"],dataList[i]["hp"],dataList[i]["prefab"]));
            }
        }

        IEnumerator TestSpawn(){
            for(int i = 0; i < 20; i++){
                GameObject mosterObject = Instantiate(monsters[1]).gameObject;
                mosterObject.transform.parent = this.transform;
                yield return new WaitForSeconds(1f);
            }
            
        }
        
    }
}

