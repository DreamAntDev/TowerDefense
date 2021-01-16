using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerListItem : MonoBehaviour
{
    public Text TowerName;
    public Button SpawnButton;

    private string towerCode;

    public void SetData(TowerData.Data data)
    {
        this.TowerName.text = data.index.ToString() + ". " + data.prefabCode.Split('/')[data.prefabCode.Split('/').Length - 1];
        towerCode = data.prefabCode;
        SpawnButton.onClick.AddListener(()=> { SelectItem(data.index); });
    }
    private void SelectItem(int index)
    {
        UILoader.Instance.Unload("TowerListPopup");
        PlayerControlManager.Instance.createTowerIndex = index;
        GameManager.Instance.SetVisibleGrid(true);
    }
}
