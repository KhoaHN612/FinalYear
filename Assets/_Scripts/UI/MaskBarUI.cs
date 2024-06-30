using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBarUI : MonoBehaviour
{
    public Object fullBar;    
    private RectTransform barRectTransform;
    private float width;
    [Range(0, 1)]
    public float percent;
    private Vector2 initialPosition;

    public void Initialize(){
        barRectTransform = fullBar.GetComponent<RectTransform>();
        initialPosition = barRectTransform.anchoredPosition;
        width = barRectTransform.rect.width;
        ResetBar();
        SetDisplay(1);
    }
    void Update()
    {
        float distanceToMove = width * (1 - percent);
        float newXPosition = initialPosition.x - distanceToMove;
        barRectTransform.anchoredPosition = new Vector2(newXPosition, initialPosition.y);
    }
    public void ResetBar()
    {
        barRectTransform.anchoredPosition = initialPosition;
    }
    public bool SetDisplay(float Percent){
        if (Percent < 0 || Percent > 1){
            return false;
        }
        percent = Percent;
        return true;
    }

}
