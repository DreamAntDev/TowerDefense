using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerSceneManager : MonoBehaviour
{

   private AsyncOperation oper;
   public static TowerSceneManager _Instance;

   public static TowerSceneManager instance
    {
        get
        {
            if (_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.

                _Instance = FindObjectOfType(typeof(TowerSceneManager)) as TowerSceneManager;
            }

            // If it is still null, create a new instance
            if (_Instance == null)
            {
                GameObject obj = new GameObject("SceneManager");
                _Instance = obj.AddComponent(typeof(TowerSceneManager)) as TowerSceneManager;
            }

            return _Instance;
        }
    }

    private void Awake() {
        DontDestroyOnLoad(this);
    }

    IEnumerator ReadyLoadScene(string name)
    {
        oper = SceneManager.LoadSceneAsync(name);
        oper.allowSceneActivation = false;//true 되면 다음으로 넘어감

        yield return null;
    }

   public void LoadScene(string name){

       switch(name){
            case "Login" : 
                SceneManager.LoadSceneAsync("LoginScene");
                break;

            case "Menu" :
                SceneManager.LoadSceneAsync("MenuScene");
                break;

            case "Game" :
                SceneManager.LoadSceneAsync("GameScene");
                break;
       }
   }
   
}
