using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster{

    public class TypeMonster : MonoBehaviour
    {
        private MonsterConfiguration monsterinfo;
        
        public void SetMonsterInfo(MonsterConfiguration mf){
            monsterinfo = mf;
        }

        public MonsterConfiguration GetMonsterInfo(){
            return monsterinfo;
        }

        public int GetMonsterType(){
            return monsterinfo.GetType();
        }
        public int GetHP(){
            return monsterinfo.GetHP();
        }

        public double GetSpeed(){
            return monsterinfo.GetSpeed();
        }

        public int GetCoin(){
            return monsterinfo.GetCoin();
        }
       
    }
}
