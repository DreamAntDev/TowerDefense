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

        public int ID(){
            return _id;
        }

        public int GetType(){
            return _Type;
            
        }


        public int GetHP(){
            return _HP;
        }
        
        public double GetSpeed(){
            return _MoveSpeed;
            
        }

        public int GetCoin(){
            return _Coin;
        }

    }
}
