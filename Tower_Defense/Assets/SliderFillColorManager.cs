using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.ModernUIPack;

public class SliderFillColorManager : MonoBehaviour
{
   [SerializeField] protected Image fillColor;

    public void FillColorSet(SliderManager sm){
        float value = sm.mainSlider.value;
        if(value > 0.25f){
            //상위 75%
            if(value > 0.75f){
                fillColor.color = new Color(1 - (value - 0.75f) * 4, 1, 0, 1);
            }else{
                fillColor.color = new Color(1 ,0 + (value - 0.5f) * 3.333f, 0, 1);
            }
        }else{
            //하위 25%
                fillColor.color = new Color(value * 4, 0, 0, 1);
        }

    }
}
