using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Monster;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.SceneManagement;
using PlayerControlState;

public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField] protected MonsterManager monsterManager;

    [SerializeField] protected AudioManager audioManager;

    [SerializeField] protected MapManager mapManager;

    [SerializeField] protected int coin;

    [SerializeField] private GameObject[] grids;

    private GameObject mainUI;

    private MainUI mainUIEvent;

    private bool isSkip = false;

    private int level = 0;

    private bool isStart = false;

    private int life = 5;

    private int maxCoin = 0;

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
        LoadUI();
    }

    private void LoadUI(){

        mainUI = UILoader.Instance.Load("MainUI");
        GameObject mainMoveButton = UILoader.Instance.Load("MapMove");
        if(mainUI != null){
            mainUIEvent = mainUI.GetComponent<MainUI>();
            mainUIEvent.OnStartClickListener(GameStart);
            mainUIEvent.OnSkipClickListener(Skip);
        }
    }
    public void MonsterCoin(int c){
       coin += c;
       maxCoin += c;
       mainUIEvent.SetCoinText(coin.ToString());
    }

    public bool DecrementCoin(int c)
    {
        if(coin >= c)
        {
            coin -= c;
            mainUIEvent.SetCoinText(coin.ToString());
            return true;
        }
        return false;
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
        for(int i = 0 ; i < grids.Length; i++){
            grids[i].SetActive(visible);
        }
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

        if(level >= PlayerPrefs.GetInt("LEVEL", 0 )){
            PlayerPrefs.SetInt("LEVEL", level);

            if(maxCoin > PlayerPrefs.GetInt("SCORE", 0)){
                PlayerPrefs.SetInt("SCORE", coin);
            }
        }
        // 결과 스코어 랭킹 등록
        StartCoroutine(RewardManager.Instance.IncrScore(LoginSceneManager.UserID, maxCoin, "GameEnd"));
        SceneManager.LoadSceneAsync("MenuScene");
    }

    public void Skip(){
        isSkip = true;
    }

    public int GetLevel(){
        return level;
    }

    private void NextLevelUI()
    {
        mainUIEvent.SetLevelText(level.ToString());
        mainUIEvent.SetTitleText("LEVEL : " + level.ToString());
        mainUIEvent.ViewTitle();
    }

    private void TextNextMap()
    {
        mapManager.AddMap(1);
        mapManager.AddMap(2);
    }

    IEnumerator NextMap()
    {
        //추가 맵 체크
        mapManager.AddMap(level / 5);
        Debug.Log("Map : " + level + " ADD");
        yield return new WaitForSeconds(5f);
        yield return StartCoroutine(StartSpawn());
    }

    IEnumerator NextLevel(){
        monsterManager.MonsterSpawn(level);
        yield return null;
    }

    IEnumerator StartSpawn(){
        for (int i = 0; i < 5; i++){
            level++;

            NextLevelUI();
            monsterManager.MonsterSpawn(level);

            yield return StartCoroutine(ReadySkip());

            if(!isStart){
                break;
            }
        }
        StartCoroutine(NextMap());
        yield return null;
    }

    IEnumerator ReadySkip(){
        //30초, Skip 대기
        yield return new WaitForSeconds(20f);
        if (level % 5 != 0)
        {
            int currentTime = 0;
            isSkip = false;
            for (; ; )
            {
                currentTime++;
                yield return new WaitForSeconds(0.1f);
                if (currentTime >= 300 || isSkip)
                {
                    break;
                }
            }
        }
        else
        {
            yield return new WaitForSeconds(20f);
        }
    }

    public Tower CreateTower(int index, Vector3 pos, LocationGrid targetGrid)
    {
        var data = TowerData.GetData(index);
        if (DecrementCoin(data.cost) == false)
            return null;

        GameObject prefab = TowerResource.Instance.GetTowerResource(data.prefabCode);
        if (prefab != null)
        {
            var obj = GameObject.Instantiate(prefab, pos, new Quaternion());
            var towerObj = obj.GetComponent<Tower>();
            if (towerObj == null)
                Debug.LogError("Spawn Fail");
            else
            {
                towerObj.Initialize(index);
                towerObj.grid = targetGrid;
                targetGrid.tower = towerObj;
                return towerObj;
            }
        }
        return null;
    }

    public void UpgradeTower(Tower beforeObj,int upgradeIndex)
    {
        var newTower = GameManager.Instance.CreateTower(upgradeIndex, beforeObj.transform.position,beforeObj.grid);
        if (newTower != null)
        {
            GameObject.Destroy(beforeObj.gameObject);
        }
        PlayerControlManager.Instance.SetState(PlayerControlManager.State.Play);
    }

    public int GetCoin()
    {
        return this.coin;
    }
}
