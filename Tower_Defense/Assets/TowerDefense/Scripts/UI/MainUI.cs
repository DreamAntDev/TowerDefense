using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class MainUI : CanvasBehaviour
{
    [SerializeField] protected TextMeshProUGUI coinText;

    [SerializeField] protected TextMeshProUGUI levelText;

    [SerializeField] protected Button startButton;

    [SerializeField] protected Button skipButton;

    [SerializeField] protected GameObject levelTitle;

    [SerializeField] protected TextMeshProUGUI lifeText;

    [SerializeField] protected Button TowerListOpenButton;
    [SerializeField] protected Button TowerCreateCancleButton;


    private Animator levelTitleAnimation;
    private Text levelTitleText;

    private void Start() {
        if(levelTitle != null){
            levelTitleAnimation = levelTitle.GetComponent<Animator>();
            levelTitleText = levelTitle.GetComponentInChildren<Text>();
        }
        
        this.TowerListOpenButton.onClick.AddListener(this.ShowTowerList);
        this.TowerCreateCancleButton.onClick.AddListener(this.TowerCreateCancle);
    }

    public void SetCoinText(string text){
        coinText.text = text;
    }

    public void SetLevelText(string text){
        levelText.text = text;
    }
    public void SetTitleText(string text){
        levelTitleText.text = text;
    }

    public void SetLifeText(string text){
        lifeText.text = text;
    }
    
     public void ViewTitle(string s){
        levelText.text = s;
        ViewTitle();
    }
    public void ViewTitle(){

        // levelTitleAnimation.SetTrigger("New");
        // levelTitleAnimation.SetTrigger("Event");
        if (!levelTitle.activeSelf){
            levelTitle.SetActive(true);
        }

    }

    public void OnStartClickListener(UnityAction call){
        startButton.onClick.AddListener(call);
    }

    public void OnSkipClickListener(UnityAction call){
        skipButton.onClick.AddListener(call);
    }
    void ShowTowerList()
    {
        UILoader.Instance.Load("TowerListPopup");
    }
    void TowerCreateCancle()
    {
        PlayerControlManager.Instance.SetState(PlayerControlManager.State.Play);
    }

    public void SetTowerCreateMode(bool createMode)
    {
        this.TowerListOpenButton.gameObject.SetActive(!createMode);
        this.TowerCreateCancleButton.gameObject.SetActive(createMode);
    }
}
