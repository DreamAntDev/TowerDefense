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
        #if UNITY_EDITOR
        
        #elif UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    FirstPoint = Input.GetTouch(0).position;
                    xAngleTemp = xAngle;
                    yAngleTemp = yAngle;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    SecondPoint = Input.GetTouch(0).position;
                    xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                    yAngle = yAngleTemp - (SecondPoint.y - FirstPoint.y) * 90  / Screen.height;
    
    
                    this.transform.localRotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
                }
            }      
        #endif
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