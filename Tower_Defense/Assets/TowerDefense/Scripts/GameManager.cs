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

    [SerializeField] protected MapManager mapManager;

    [SerializeField] protected int coin;

    [SerializeField] protected TextMeshProUGUI coinText;

    [SerializeField] private GameObject Grid;
    
    private GameObject mainUI;

    private MainUI mainUIEvent;

    private bool isSkip = false;

    private int level = 0;

    private bool isStart = false;

    private int life = 5;


    private new void Awake() {
        base.Awake();
        if(monsterManager == null){
            monsterManager = GameObject.Find("MonsterFool").GetComponent<MonsterManager>();
        }

        if(audioManager == null){
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
    }

    private void Start() {
        mainUI = UILoader.Instance.Load("MainUI");
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

    public void LifeBar(bool isBoss){
        
        if(isStart){
            if(isBoss){
                life = 0;
                mainUIEvent.SetLifeText("X " + life);
            }else{
                mainUIEvent.SetLifeText("X " + (--life));
            }
            
        }
        if(life <= 0){
            GameEnd();
            isStart = false;
        }
    }

    public void SetVisibleGrid(bool visible)
    {
        this.Grid.SetActive(visible);
    }

    public void GameStart(){
        if(!isStart){
            isStart = true;
            StartCoroutine(StartSpawn());
        }
    }

    public void GameEnd(){
        mainUIEvent.SetTitleText("END");
        mainUIEvent.ViewTitle();
    }

    public void ReadySkip(){
        isSkip = true;
    }

    public int GetLevel(){
        return level;
    }

    IEnumerator StartSpawn(){ 
        for(;;){
            level++;
            
            //추가 맵 체크
            if(level > 5 && level % 5 == 1){
                Debug.Log("Add Map");
                mainUIEvent.ViewTitle("새로운 환경이 추가됩니다.");
                mapManager.AddMap((level-1) / 5);
            }

            mainUIEvent.SetLevelText(level.ToString());
            mainUIEvent.SetTitleText("LEVEL : " + level.ToString());
            monsterManager.MonsterSpawn(level);
            mainUIEvent.ViewTitle();
            //yield return new WaitForSeconds(20f);
             Debug.Log("Map Level : " + level);
            int currentTime = 0;
            isSkip = false;
            
            for(;;){
                currentTime++;
                yield return new WaitForSeconds(0.1f);
                if(currentTime >= 300 || isSkip){
                    break;
                }
            }

            if(!isStart){
                break;
            }
        }
    }
    
    public void CreateTower(int index, Vector3 pos)
    {
        var data = TowerData.GetData(index);
        GameObject prefab = TowerResource.Instance.GetTowerResource(data.prefabCode);
        if (prefab != null)
        {
            var obj = GameObject.Instantiate(prefab, pos, new Quaternion());
            var towerObj = obj.GetComponent<Tower>();
            if (towerObj == null)
                Debug.LogError("ErrorSpawn!");
            else
            {
                towerObj.towerIndex = index;
            }
        }
    }
    public void UpgradeTower(GameObject beforeObj,int upgradeIndex)
    {
        GameManager.Instance.CreateTower(upgradeIndex, beforeObj.transform.position);
        GameObject.Destroy(beforeObj);
        PlayerControlManager.Instance.SetState(PlayerControlManager.State.Play);
    }
}
