using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerListItem : MonoBehaviour
{
    public Text TowerName;
    public Text TowerCost;
    public Button SpawnButton;

    private TowerData.Data towerData;
    

    public void SetData(TowerData.Data data)
    {
        //this.TowerName.text = data.index.ToString() + ". " + data.prefabCode.Split('/')[data.prefabCode.Split('/').Length - 1];
        this.TowerName.text = data.name;
        this.TowerCost.text = string.Format("{0} Coin", data.cost);
        towerData = data;
        SpawnButton.onClick.AddListener(()=> { SelectItem(data.index); });
        if (GameManager.Instance.GetCoin() < data.cost)
            this.SpawnButton.interactable = false;
    }
    private void SelectItem(int index)
    {
        UILoader.Instance.Unload("TowerListPopup");
        PlayerControlManager.Instance.SetState(PlayerControlManager.State.CreateTower);
        var state = PlayerControlManager.Instance.state as PlayerControlState.CreateTower;
        state.SetCreateTowerIndex(index);
    }

    public void LateUpdate()
    {
        if (GameManager.Instance.GetCoin() < this.towerData.cost)
            this.SpawnButton.interactable = false;
        else
            this.SpawnButton.interactable = true;
    }
}
