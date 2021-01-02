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
        for(int i=0;i<10;i++)
        {
            var item = Instantiate<GameObject>(TowerListItem);
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
