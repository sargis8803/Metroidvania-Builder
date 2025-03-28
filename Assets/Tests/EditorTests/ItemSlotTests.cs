using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using TMPro;

public class ItemSlotTests
{
    private ItemSlot slot;
    private GameObject itemSlotGameObject;

    private Image itemImage;
    private TMP_Text itemDescriptionText;
    public TMP_Text itemDescriptionName;

    public GameObject selectedShader;
    public GameObject descriptionBox;

    [SetUp]
    public void SetUp ()
    {
        //Creates Gameobject and requisite components for testing
        itemSlotGameObject = new GameObject("ItemSlot");
        slot = itemSlotGameObject.AddComponent<ItemSlot>();
        itemImage = new GameObject("ItemImage").AddComponent<Image>();
        slot.itemImage = itemImage;
        itemDescriptionText = new GameObject("ItemDescriptionText").AddComponent<TMP_Text>();
        slot.itemDescriptionText = itemDescriptionText;
        itemDescriptionName = new GameObject("ItemDescriptionName").AddComponent<TMP_Text>();
        slot.itemDescriptionName = itemDescriptionName;
        selectedShader = new GameObject("SelectedShader");
        slot.selectedShader = selectedShader;
        descriptionBox = new GameObject("DescriptionBox");
        slot.descriptionBox = descriptionBox;
        slot.isFull = false;
    }

    // White Box Unit Test, with 100% statement coverage on function:
    /* public void AddItem(string itemName, Sprite itemSprite, string itemDescription)
       {
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        itemImage.sprite = itemSprite;

       }
     */
    [Test]
    public void AddItem_SuccessfulAdd()
    {
        string testItemName = "Test Item";
        Sprite testItemSprite = Sprite.Create(Texture2D.blackTexture, new Rect(0, 0, 3, 3), Vector2.zero);
        string testItemDescription = "Test Description, very descriptive and informative";

        slot.AddItem(testItemName, testItemSprite, testItemDescription, GearManager.GearType.none);

        Assert.AreEqual(testItemName, slot.itemName, "Item name should be correctly assigned");
        Assert.AreEqual(testItemSprite, slot.itemSprite, "Item sprite should be correctly assigned");
        Assert.AreEqual(testItemDescription, slot.itemDescription, "Item description should be correctly assigned");
        Assert.IsTrue(slot.isFull, "Item slot should have its valued changed to represent that it is full");

        Assert.AreEqual(testItemSprite, slot.itemImage.sprite, "Item sprite should be correctly assigned");
    }
}
