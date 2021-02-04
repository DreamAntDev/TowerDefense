using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class MainUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI coinText;

    [SerializeField] protected TextMeshProUGUI levelText;

    [SerializeField] protected Button startButton;

    [SerializeField] protected Button skipButton;

    [SerializeField] protected GameObject levelTitle;

    private Animator levelTitleAnimation;
    private Text levelTitleText;

    private void Start() {
        if(levelTitle != null){
            levelTitleAnimation = levelTitle.GetComponent<Animator>();
            levelTitleText = levelTitle.GetComponentInChildren<Text>();
        }    
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
     public void ViewTitle(string s){
        levelText.text = s;
        ViewTitle();
    }
    public void ViewTitle(){
        if(!levelTitle.activeSelf){
            levelTitle.SetActive(true);
        }
        levelTitleAnimation.SetTrigger("New");
        levelTitleAnimation.SetTrigger("Event");
    }

    public void OnStartClickListener(UnityAction call){
        startButton.onClick.AddListener(call);
    }

    public void OnSkipClickListener(UnityAction call){
        skipButton.onClick.AddListener(call);
    }
}
