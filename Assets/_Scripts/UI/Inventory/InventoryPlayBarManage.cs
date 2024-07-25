using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPlayBarManage : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPlaySlotPrefab;
    private List<InventoryPlaySlot> inventoryPlaySlots;
    private int indexSelect = -1;
/*    // Start is called before the first frame update
    void Start()
    {
        Initialization(10);
    }*/
    public void Initialization(int number)
    {
        if (inventoryPlaySlots != null)
        {
            foreach (var slot in inventoryPlaySlots)
            {
                Destroy(slot.gameObject);
            }
        }

        inventoryPlaySlots = new List<InventoryPlaySlot>();

        for (int i = 0; i < number; i++)
        {
            GameObject newSlot = Instantiate(inventoryPlaySlotPrefab, transform);
            InventoryPlaySlot slot = newSlot.GetComponent<InventoryPlaySlot>();
            if (slot != null)
            {
                inventoryPlaySlots.Add(slot);
            }
        }

        inventoryPlaySlots.Sort((slot1, slot2) => slot1.transform.GetSiblingIndex().CompareTo(slot2.transform.GetSiblingIndex()));
    }
    public int GetNumberInventorySlot()
    {
        return inventoryPlaySlots.Count;
    }
    public int TryToAddItem()
    {
        int i = -1;
        do
        {
            i++;
        } while ((i < inventoryPlaySlots.Count) && (inventoryPlaySlots[i].isEmpty() == false));
        
        if (i == inventoryPlaySlots.Count)
        {
            return -1;
        } 
        else
        {
            return i;
        }
    }
    public void ReplaceItem(int i, Sprite sprite) 
    {
        if (i > inventoryPlaySlots.Count)
        {
            return;
        }
        inventoryPlaySlots[i].ReplaceItem(sprite);
    }
    public void SelectInventorySlot(int i)
    {
        if ((i >= inventoryPlaySlots.Count) || (i < 0))
        {
            return;
        }
        if (indexSelect == i)
        {
            return;
        }
        if (indexSelect != -1)
        {
            inventoryPlaySlots[indexSelect].isSelect = false;
        }
        inventoryPlaySlots[i].isSelect = true;
        indexSelect = i;

    }
    // Update is called once per frame
    void Update()
    {

    }

}
