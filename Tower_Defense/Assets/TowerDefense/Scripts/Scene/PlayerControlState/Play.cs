using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControlState
{
    public class Play : IPlayerControlState
    {
        public GameObject towerObject { get; private set; }
        public void End()
        {

        }

        public void Start()
        {

        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }
        void OnClick()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                var tower = hit.collider.gameObject.GetComponent<Tower>();
                if (tower != null)
                {
                    this.towerObject = hit.collider.gameObject;
                    var go = UILoader.Instance.Load("TowerUpgradePopup");
                    go.GetComponent<TowerUpgradePopup>().SetList(tower.towerIndex);
                }
            }
        }
    }
}