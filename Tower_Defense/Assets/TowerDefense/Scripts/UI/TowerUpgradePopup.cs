using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradePopup : MonoBehaviour
{
    public ScrollRect scrollRect;
    public GameObject TowerListItem;
    public Button closeButton;
    // Start is called before the first frame update
    void Start()
    {
        this.closeButton.onClick.AddListener(Close);
    }

    public void SetList(int towerIndex)
    {
        TowerData.Data data = TowerData.GetData(towerIndex);
        var towerList = data.GetNextTowerList();
        foreach (var idx in towerList)
        {
            var item = Instantiate<GameObject>(TowerListItem);
            var towerUpgradeItem = item.GetComponent<TowerUpgradeItem>();
            if (towerUpgradeItem != null)
            {
                var towerData = TowerData.GetData(idx);
                towerUpgradeItem.SetData(towerData);
            }
            item.transform.SetParent(scrollRect.content.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Close()
    {
        UILoader.Instance.Unload(this.gameObject);
    }
}
