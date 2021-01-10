using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerListPopup : MonoBehaviour
{
    public ScrollRect scrollRect;
    public GameObject TowerListItem;
    public Button closeButton;
    // Start is called before the first frame update
    void Start()
    {
        List<TowerData> list = new List<TowerData>();
        list.Add(TowerData.GetData(1));
        list.Add(TowerData.GetData(2));
        foreach(var data in list)
        {
            var item = Instantiate<GameObject>(TowerListItem);
            var towerListItem = item.GetComponent<TowerListItem>();
            if(towerListItem != null)
            {
                towerListItem.SetData(data);
            }
            item.transform.SetParent(scrollRect.content.transform);
        }
        
        this.closeButton.onClick.AddListener(Close);
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
