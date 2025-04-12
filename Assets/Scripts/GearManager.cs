using UnityEngine;

public class GearManager : MonoBehaviour
{
    [SerializeField] public MenuManager menuManager;
    public ItemSlot[] itemSlot;
    public EquipSlot[] equipSlot;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            menuManager.ToggleGearMenu();
            DeselectAllSlots();
        }
    }

    public void AddItem(string itemName, Sprite itemSprite, string itemDescription, GearType gearType)
    {
        //Debug.Log("itemName = " + itemName + "itemSprite = " + itemSprite);
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, itemSprite, itemDescription, gearType);
                return;
            }
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].descriptionBox.SetActive(false);
            itemSlot[i].isSelected = false;
        }
    }

    public enum GearType
    {
        none,
        head,
        weapon,
        body,
    };
}
