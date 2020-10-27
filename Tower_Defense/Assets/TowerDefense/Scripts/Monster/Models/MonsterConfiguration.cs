using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{  
    abstract public class MonsterConfiguration
    {
        private MonsterType _Type ;

        private int _HP { get; set; }

        private double _MoveSpeed { get; set; } 

        private int _Coin { get; set; } 
    }
}
