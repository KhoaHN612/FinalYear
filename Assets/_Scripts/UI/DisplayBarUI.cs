using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBarUI : MonoBehaviour
{
    public float widthPerInit = 5;
    public Object bar;
    private RectTransform barRectTransform;
    public Object fullBar;    
    private RectTransform fullBarRectTransform;
    private float width;
    [Range(0, 1)]
    public float percent;
    private int _maxInit;

    public void Initialize(int maxInit){
        _maxInit = maxInit;
        barRectTransform = bar.GetComponent<RectTransform>();
        fullBarRectTransform = fullBar.GetComponent<RectTransform>();
        // initialPosition = fullBarRectTransform.anchoredPosition;
        width = widthPerInit * maxInit;

        setWidth(barRectTransform, width);
        // Vector2 sizeDelta = barRectTransform.sizeDelta;
        // sizeDelta.x = width;
        // barRectTransform.sizeDelta = sizeDelta;

        ResetBar();
        SetDisplay(_maxInit);
    }
    private void setWidth(RectTransform rectTransform, float width){
        Vector2 sizeDelta = rectTransform.sizeDelta;
        sizeDelta.x = width;
        rectTransform.sizeDelta = sizeDelta;
    }
    public void ResetBar()
    {

        fullBarRectTransform.offsetMin = Vector2.zero;
        fullBarRectTransform.offsetMax = Vector2.zero;
    }
    public bool SetDisplay(int init){
        float Percent = (float)(init)/(float)(_maxInit);

        if (Percent < 0 || Percent > 1){
            return false;
        }
        percent = Percent;

        float distanceToMove = width * (1 - percent);

        Vector2 offsetMin = fullBarRectTransform.offsetMin;
        Vector2 offsetMax = fullBarRectTransform.offsetMax;
        offsetMin.x = -distanceToMove;
        offsetMax.x = -distanceToMove;
        fullBarRectTransform.offsetMin = offsetMin;
        fullBarRectTransform.offsetMax = offsetMax;
        // float newXPosition = initialPosition.x - distanceToMove;
        // fullBarRectTransform.anchoredPosition = new Vector2(newXPosition, initialPosition.y);

        return true;
    }
}
