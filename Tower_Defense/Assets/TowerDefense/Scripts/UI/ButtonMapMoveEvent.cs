using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonMapMoveEvent : MonoBehaviour
{
    enum Arrow { LEFT, RIGHT, UP, DOWN}
    public Button[] buttons;

    private void Awake() {
        buttons = GetComponentsInChildren<Button>();
        
    }

    private void Start() {
        MapDisablebutton();
    }

    public void OnClickListeners(UnityAction[] calls){
        for(int i = 0; i < buttons.Length; i++){
            buttons[i].onClick.AddListener(calls[i]);
        }
    }

    public void MapEnableButton(int idx){
        int gameLevel = GameManager.Instance.GetLevel();
        MapDisablebutton();
        Debug.Log("ArrowEnable : "  + idx);
        switch(idx){
            case 0:
                buttons[(int)Arrow.RIGHT].gameObject.SetActive(true);
                if(gameLevel > 15){
                    buttons[(int)Arrow.DOWN].gameObject.SetActive(true);
                }
                break;
            case 1:
                buttons[(int)Arrow.LEFT].gameObject.SetActive(true);
                if(gameLevel > 10){
                    buttons[(int)Arrow.DOWN].gameObject.SetActive(true);
                }
                break;
            case 2:
                if(gameLevel > 15){
                    buttons[(int)Arrow.LEFT].gameObject.SetActive(true);
                }
                    buttons[(int)Arrow.UP].gameObject.SetActive(true);
                break;
            case 3:
                    buttons[(int)Arrow.RIGHT].gameObject.SetActive(true);
                    buttons[(int)Arrow.UP].gameObject.SetActive(true);
                break;
        }
    }

    public void MapDisablebutton(){
        foreach(Button btn in buttons){
            btn.gameObject.SetActive(false);
        }
    }
}
