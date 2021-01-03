using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField] protected int coin;

    [SerializeField] protected TextMeshProUGUI coinText;

    [SerializeField] private GameObject Grid;

    public void MonsterCoin(int c){
       coin += c;
       coinText.text =  "Coin : " + coin.ToString();
    }

    public void GameLevel(){

    }

    public void SetVisibleGrid(bool visible)
    {
        this.Grid.SetActive(visible);
    }
      
}
