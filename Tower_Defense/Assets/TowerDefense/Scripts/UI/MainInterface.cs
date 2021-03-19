using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainInterface : CanvasBehaviour
{
    public Button TowerListOpenButton;
    // Start is called before the first frame update
    void Start()
    {
        TowerListOpenButton.onClick.AddListener(this.ShowTowerList);
        
    }

    void ShowTowerList()
    {
        UILoader.Instance.Load("TowerListPopup");
    }
}
