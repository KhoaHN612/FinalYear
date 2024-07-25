using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPlaySlot : MonoBehaviour
{
    private Image inventoryImage;
    private Image selectImage;
    private bool _isSelect;
    public bool isSelect {
        get 
        { 
            return _isSelect;
        }
        set 
        { 
            _isSelect = value;
            selectImage.enabled = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Transform inventoryImageTransform = transform.Find("InvetoryImage");
        if (inventoryImageTransform != null)
        {
            inventoryImage = inventoryImageTransform.GetComponentInChildren<Image>();
        }
        inventoryImage.enabled = false;
        Transform inventoryImageSelectTransform = transform.Find("ImageSelect");
        if (inventoryImageSelectTransform != null)
        {
            selectImage = inventoryImageSelectTransform.GetComponent<Image>();
        }
        isSelect = false;
    }
    public void ReplaceItem(Sprite sprite)
    {
        inventoryImage.enabled = true;
        if (inventoryImage != null)
        {
            inventoryImage.sprite = sprite;
        }
    }
    public bool isEmpty() { return inventoryImage.sprite == null; }
    // Update is called once per frame
    void Update()
    {
        
    }
}
