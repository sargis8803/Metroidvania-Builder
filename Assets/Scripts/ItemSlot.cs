using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public Sprite itemSprite;
    public string itemDescription;
    public bool isFull;
    [SerializeField] public Image itemImage;
    public TMP_Text itemDescriptionText;
    public TMP_Text itemDescriptionName;

    public GameObject selectedShader;
    public bool isSelected;

    public GameObject descriptionBox;

    private GearManager gearManager;

    private void Start()
    {
        gearManager = GameObject.Find("GearCanvas").GetComponent<GearManager>();
    }


    public void AddItem(string itemName, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        itemImage.sprite = itemSprite;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }

    }

    //Selects an itemslot in the gear menu
    private void OnLeftClick()
    {
        gearManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        isSelected = true;

        descriptionBox.GetComponent<RectTransform>().position = new Vector3((GetComponent<RectTransform>().position.x + 75), (GetComponent<RectTransform>().position.y - 125), (GetComponent<RectTransform>().position.z));

        if (isFull)
        {
            descriptionBox.SetActive(true);
            itemDescriptionName.text = itemName;
            itemDescriptionText.text = itemDescription;
        }
    }

    //Deselects an itemslot in the gear menu
    private void OnRightClick()
    {
        selectedShader.SetActive(false);
        isSelected = false;

        descriptionBox.SetActive(false);
    }
}
