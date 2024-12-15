using SVS.PlayerAgent;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SaleUtil : MonoBehaviour
{
    public int salePrice;
    public TextMeshPro salePriceText;
    public UnityEvent OnCannotSale;
    public UnityEvent OnSale;

    private void Start()
    {
        if (salePriceText != null)
        {
            salePriceText.text = salePrice.ToString();
        }
    }

    public void TryToSale(Object obj)
    {
        PlayerPoints pocket = obj.GetComponent<PlayerPoints>();

        var result = pocket.Use(salePrice);

        if (result)
        {
            OnSale?.Invoke();
        }
        else
        {
            OnCannotSale?.Invoke();
        }

    }
}
