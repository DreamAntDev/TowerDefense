using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerListItem : MonoBehaviour
{
    public Text TowerName;
    public Button SpawnButton;

    private string prefabPath;

    public void SetData(TowerData data)
    {
        this.TowerName.text = data.index.ToString() + ". " + data.prefabPath.Split('/')[data.prefabPath.Split('/').Length - 1];
        prefabPath = data.prefabPath;
        SpawnButton.onClick.AddListener(()=> { SelectItem(prefabPath); });
    }
    private void SelectItem(string path)
    {
        UILoader.Instance.Unload("Assets/TowerDefense/Prefabs/UI/TowerListPopup.prefab");
        PlayerControlManager.Instance.createObjectPrefabPath = path;
        GameManager.Instance.SetVisibleGrid(true);
    }
}
