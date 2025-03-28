using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EquipSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image slotImage;
    [SerializeField] private TMP_Text slotName;
    [SerializeField] private GearManager.GearType gearType = new GearManager.GearType();

    private Sprite itemSprite;
    private string itemName;
    private string itemDescription;

    private bool isFull = false;


    [SerializeField] public GameObject selectedShader;
    public bool isSelected;

    [SerializeField] public GameObject descBox;
    public TMP_Text itemDescriptionText;
    public TMP_Text itemDescriptionName;

    private GearManager gearManager;
    private void Start()
    {
        gearManager = GameObject.Find("GearCanvas").GetComponent<GearManager>();
    }

    public void EquipGear(string itemName, Sprite itemSprite, string itemDescription)
    {
        if (isFull)
        {
            UnEquipGear();
        }

        this.itemSprite = itemSprite;
        slotImage.sprite = itemSprite;
        slotName.enabled = false;

        this.itemName = itemName;
        this.itemDescription = itemDescription;

        //This is where we actually update the player's stats
        /*
        if (itemName == "Blaster")
        {
            PlayerMovement.maxJumps += 1;
        }
        */

        isFull = true;
        descBox.SetActive(true);
        itemDescriptionName.text = itemName;
        itemDescriptionText.text = itemDescription;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    //Selects an equipment slot
    private void OnLeftClick()
    {
        if (isSelected && isFull)
        {
            UnEquipGear();
        }
        else
        {
            gearManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            isSelected = true;
        }
    }

    //Deselects an equipment slot
    private void OnRightClick()
    {
        selectedShader.SetActive(false);
        isSelected = false;
    }

    public void UnEquipGear()
    {
        selectedShader.SetActive(false);
        isSelected = false;
        descBox.SetActive(false);

        gearManager.AddItem(itemName, itemSprite, itemDescription, gearType);


        this.itemSprite = null;
        slotImage.sprite = null;
        slotName.enabled = true;

        isFull = false;
    }
}
