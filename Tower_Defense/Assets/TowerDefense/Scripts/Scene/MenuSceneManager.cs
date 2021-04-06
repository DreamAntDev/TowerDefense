using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Michsky.UI.ModernUIPack;

public class MenuSceneManager : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI val_MAX_SCORE;
    [SerializeField] protected TextMeshProUGUI val_LAST_LEVEL;

    AsyncOperation asyncOperation;
    
    void Start()
    {
        val_MAX_SCORE.text = string.Format(PlayerPrefs.GetInt("SCORE",0).ToString());
        val_LAST_LEVEL.text = string.Format(PlayerPrefs.GetInt("LEVEL",0).ToString());
        asyncOperation = SceneManager.LoadSceneAsync("GameScene");
        asyncOperation.allowSceneActivation = false;
    }


    public void LoadSceneGame(){
        Debug.Log("Game Load Scene Go");
        StartCoroutine(LoadScene());
    }

    public void MenuClick(){
        Debug.Log("Menu Bar Click");
    }

    IEnumerator LoadScene(){
        asyncOperation.allowSceneActivation = true;
        while(!asyncOperation.isDone){
            Debug.Log("Game Scene Loding.. : " + asyncOperation.progress);
            yield return null;
        }
        yield return null;
    }
}
