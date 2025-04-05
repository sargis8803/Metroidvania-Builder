using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class ItemGearManagerIntegrationTest
{
    private GameObject gearManagerObject;
    private GearManager gearManager;
    private GameObject itemObject;
    private Item item;

    [SetUp]
    public void SetUp()
    {
        gearManagerObject = new GameObject();
        gearManager = gearManagerObject.AddComponent<GearManager>();

        gearManager.menuManager = new GameObject().AddComponent<MenuManager>();
        gearManager.itemSlot = new ItemSlot[20];

        for (int i = 0; i < gearManager.itemSlot.Length; i++)
        {
            var itemSlot = new GameObject("ItemSlot").AddComponent<ItemSlot>();
            itemSlot.isFull = false;

            // Manually assigning components that might be required
            itemSlot.itemImage = itemSlot.gameObject.AddComponent<Image>();
            itemSlot.itemDescriptionText = itemSlot.gameObject.AddComponent<TMP_Text>();
            itemSlot.itemDescriptionName = itemSlot.gameObject.AddComponent<TMP_Text>();
            itemSlot.descriptionBox = new GameObject();
            itemSlot.selectedShader = new GameObject();

            gearManager.itemSlot[i] = itemSlot;
        }

        itemObject = new GameObject();
        item = itemObject.AddComponent<Item>();
        item.itemName = "Test Item";
        item.itemDescription = "Test Description, very descriptive and informative";
        item.sprite = Sprite.Create(Texture2D.blackTexture, new Rect(0, 0, 3, 3), Vector2.zero);

        item.gearManager = gearManager;
    }

    // Integration Test
    // Units are Item and GearManager specifically testing the AddItem functionality that they share.
    [Test]
    public void TryAddItemToGear_AddsItemToFirstAvailableSlot()
    {
        item.TryAddItemToGear();

        Assert.AreEqual("Test Item", gearManager.itemSlot[0].itemName, "Item name should be correctly assigned");
        Assert.AreEqual("Test Description, very descriptive and informative", gearManager.itemSlot[0].itemDescription, "Item description should be correctly assigned");
        Assert.AreEqual(item.sprite, gearManager.itemSlot[0].itemImage.sprite, "Item sprite should be correctly assigned");
        Assert.IsTrue(gearManager.itemSlot[0].isFull, "First slot should be full after adding the item.");
    }

    // Integration Test
    // Units are Item and GearManager specifically testing the AddItem functionality that they share.
    [Test]
    public void TryAddItemToGear_DoesNotAddItemWhenNoSlotsAvailable()
    {
        foreach (var slot in gearManager.itemSlot) {slot.isFull = true;}

        item.TryAddItemToGear();

        bool itemAdded = false;
        foreach (var slot in gearManager.itemSlot)
        {
            if (slot.itemName == "Test Item")
            {
                itemAdded = true;
                break;
            }
        }

        Assert.IsFalse(itemAdded, "Item shouldn't be added when no slots are available.");
    }
}
