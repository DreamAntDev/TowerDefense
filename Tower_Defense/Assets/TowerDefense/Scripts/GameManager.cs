using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Monster;

public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField] protected MonsterManager monsterManager;
    
    [SerializeField] protected AudioManager audioManager;

    [SerializeField] protected int coin;

    [SerializeField] protected TextMeshProUGUI coinText;

    [SerializeField] private GameObject Grid;
    
    private bool isSkip = false;

    private int level = 0;

    private void Awake() {
        if(monsterManager == null){
            monsterManager = GameObject.Find("MonsterFool").GetComponent<MonsterManager>();
        }

        if(audioManager == null){
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
    }


    public void MonsterCoin(int c){
       coin += c;
       coinText.text =  "Coin : " + coin.ToString();
    }

    public void LevelTiTle(){

    }

    public void SetVisibleGrid(bool visible)
    {
        this.Grid.SetActive(visible);
    }

    public void GameStart(){
        StartCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn(){ 
        for(;;){
            level++;
            monsterManager.MonsterSpawn(level);

            yield return new WaitForSeconds(20f);
            
            int currentTime = 0;
            isSkip = false;
            for(;;){
                currentTime++;
                yield return new WaitForSeconds(0.1f);
                if(currentTime >= 300 || isSkip){
                    break;
                }
            }
        }
    }
      
}
