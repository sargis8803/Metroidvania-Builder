using UnityEngine;

public class GearManager : MonoBehaviour
{
    public GameObject GearMenu;
    private bool isOpen;
    public ItemSlot[] itemSlot;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) &&  isOpen)
        {
            Time.timeScale = 1;
            GearMenu.SetActive(false);
            isOpen = false;
        }
        else if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            Time.timeScale = 0;
            GearMenu.SetActive(true);
            isOpen = true;
        }
    }

    public void AddItem(string itemName, Sprite itemSprite)
    {
        Debug.Log("itemName = " + itemName + "itemSprite = " + itemSprite);
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, itemSprite);
                return;
            }
        }
    }
}
