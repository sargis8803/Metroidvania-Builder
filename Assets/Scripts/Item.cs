using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] public Sprite sprite;
    [TextArea] [SerializeField] public string itemDescription;

    public GearManager gearManager;

    public GearManager.GearType gearType;

    private bool isPlayerColliding = false;

    public GameObject tipBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gearManager = GameObject.Find("GearCanvas").GetComponent<GearManager>();
    }

    private void Update()
    {
        if (isPlayerColliding && Input.GetKeyDown(KeyCode.F))
        {
            gearManager.AddItem(itemName, sprite, itemDescription, gearType);
            tipBox.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
           Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            pos.y += 50;
            tipBox.transform.position = pos;

            tipBox.SetActive(true);
            isPlayerColliding = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            tipBox.SetActive(false);
            isPlayerColliding = false;
        }
    }

    //Used for integration testing as unity is bad at simulating collisions in test framework.
    public void TryAddItemToGear()
    {
        if (gearManager != null)
        {
            gearManager.AddItem(itemName, sprite, itemDescription, gearType);
            DestroyImmediate(gameObject);
        }
    }
}
