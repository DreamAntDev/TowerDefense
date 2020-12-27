using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] protected int coin;

    [SerializeField] protected TextMeshProUGUI coinText;
    

    public void MonsterCoin(int c){
       coin += c;
       coinText.text =  "Coin : " + coin.ToString();
    }

    public void GameLevel(){

    }

    
      
}
