using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class CanvasBehaviour : MonoBehaviour
{    
    // Start is called before the first frame update
    void Awake()
    {
        var canvasScaler = this.gameObject.GetComponent<CanvasScaler>();
        if (canvasScaler == null)
            Debug.LogError("캔버스 크기조절이 불가능 합니다. CanvasScaler 컴포넌트가 있는지 확인하세요.");

        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScaler.matchWidthOrHeight = 1.0f; // height 기준
    }
}
