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
        var state = PlayerControlManager.Instance.state as PlayerControlState.UpgradeTower;
        GameManager.Instance.UpgradeTower(state.towerObject, index);
        PlayerControlManager.Instance.SetState(PlayerControlManager.State.Play);
        var ui = UILoader.Instance.GetUI("TowerUpgradePopup");
        ui.GetComponent<TowerUpgradePopup>().Close();
    }
}
