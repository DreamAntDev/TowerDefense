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
    
    private GameObject mainUI;

    private MainUI mainUIEvent;

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

    private void Start() {
        mainUI = UILoader.instance.Load("Assets/TowerDefense/Prefabs/UI/MainUI.prefab");
        if(mainUI != null){
            mainUIEvent = mainUI.GetComponent<MainUI>();
            mainUIEvent.OnStartClickListener(GameStart);
            mainUIEvent.OnSkipClickListener(ReadySkip);
        }
    }

    public void MonsterCoin(int c){
       coin += c;
       mainUIEvent.SetCoinText(coin.ToString());
    }

    public void LevelTiTle(){
        mainUIEvent.SetLevelText(level.ToString());
    }

    public void SetVisibleGrid(bool visible)
    {
        this.Grid.SetActive(visible);
    }

    public void GameStart(){
        StartCoroutine(StartSpawn());
    }

    public void ReadySkip(){
        isSkip = true;
    }

    IEnumerator StartSpawn(){ 
        for(;;){
            level++;
            mainUIEvent.SetLevelText(level.ToString());
            mainUIEvent.SetTitleText("LEVEL : " + level.ToString());
            monsterManager.MonsterSpawn(level);
            mainUIEvent.ViewTitle();
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
