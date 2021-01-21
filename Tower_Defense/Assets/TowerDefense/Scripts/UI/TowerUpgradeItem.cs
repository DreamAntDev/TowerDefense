using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeItem : MonoBehaviour
{
    public Text TowerName;
    public Button SpawnButton;

    private string towerCode;

    public void SetData(TowerData.Data data)
    {
        this.TowerName.text = data.index.ToString() + ". " + data.prefabCode.Split('/')[data.prefabCode.Split('/').Length - 1];
        towerCode = data.prefabCode;
        SpawnButton.onClick.AddListener(() => { SelectItem(data.index); });
    }
    private void SelectItem(int index)
    {
        UILoader.Instance.Unload("TowerUpgradePopup");
        var state = PlayerControlManager.Instance.state as PlayerControlState.Play;
        GameObject selectedTowerObject = state.towerObject;
        Vector3 pos = selectedTowerObject.transform.position;
        if (selectedTowerObject != null)
        {
            GameObject.Destroy(selectedTowerObject);
            PlayerControlManager.Instance.SetState(PlayerControlManager.State.UpgradeTower);
            var nextState = PlayerControlManager.Instance.state as PlayerControlState.UpgradeTower;
            nextState.TowerUpgrade(index,pos);
        }
    }
}
