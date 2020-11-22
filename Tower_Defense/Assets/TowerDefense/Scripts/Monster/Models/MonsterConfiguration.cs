namespace Monster
{  
    public class MonsterConfiguration
    {
        private int _id;
        private int _Type;
        
        private int _HP;

        private double _MoveSpeed;

        private int _Coin;

        public MonsterConfiguration(int id, int type, double moveSpeed, int hp, int coin){
            this._id = id;
            this._Type = type;
            this._MoveSpeed = moveSpeed;
            this._HP = hp;
            this._Coin = coin;
        }

        public int ID{
            get{
                return _id;
            }
        }

        public int Type{
            get{
                return _Type;
            }
        }

        public int HP{
            get{
                return HP;
            }
        }
        
        public double Speed{
            get{
                return _MoveSpeed;
            }
        }

        public int Coin{
            get{
                return _Coin;
            }
        }

    }
}
