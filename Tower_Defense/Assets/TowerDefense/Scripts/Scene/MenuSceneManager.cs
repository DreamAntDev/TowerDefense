using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Michsky.UI.ModernUIPack;
using Gpm.Common;
using Gpm.WebView;

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
    public void ShowUrl()
    {
        GpmWebView.ShowUrl(
            "http://dev-hojin.shop:8888/leaderboard",
            new GpmWebViewRequest.Configuration()
            {
                style = GpmWebViewStyle.FULLSCREEN,
                isClearCookie = false,
                isClearCache = false,
                isNavigationBarVisible = true,
                title = "The page title.",
                isBackButtonVisible = true,
                isForwardButtonVisible = true,
                contentMode = GpmWebViewContentMode.MOBILE
            },
            OnOpenCallback,
            OnCloseCallback,
            new List<string>()
            {
                "USER_ CUSTOM_SCHEME"
            },
            OnSchemeEvent);
    }

    private void OnOpenCallback(GpmWebViewError error)
    {
        if (error == null)
        {
            Debug.Log("[OnOpenCallback] succeeded.");
        }
        else
        {
            Debug.Log(string.Format("[OnOpenCallback] failed. error:{0}", error));
        }
    }

    private void OnCloseCallback(GpmWebViewError error)
    {
        if (error == null)
        {
            Debug.Log("[OnCloseCallback] succeeded.");
        }
        else
        {
            Debug.Log(string.Format("[OnCloseCallback] failed. error:{0}", error));
        }
    }

    private void OnSchemeEvent(string data, GpmWebViewError error)
    {
        if (error == null)
        {
            Debug.Log("[OnSchemeEvent] succeeded.");
            
            if (data.Equals("USER_ CUSTOM_SCHEME") == true || data.Contains("CUSTOM_SCHEME") == true)
            {
                Debug.Log(string.Format("scheme:{0}", data));
            }
        }
        else
        {
            Debug.Log(string.Format("[OnSchemeEvent] failed. error:{0}", error));
        }
    }
}
