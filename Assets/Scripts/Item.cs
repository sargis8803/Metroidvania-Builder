using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] public Sprite sprite;
    [TextArea] [SerializeField] public string itemDescription;

    public GearManager gearManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gearManager = GameObject.Find("GearCanvas").GetComponent<GearManager>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gearManager.AddItem(itemName, sprite, itemDescription);
            Destroy(gameObject);
        }
    }

    //Used for integration testing as unity is bad at simulating collisions in test framework.
    public void TryAddItemToGear()
    {
        if (gearManager != null)
        {
            gearManager.AddItem(itemName, sprite, itemDescription);
            DestroyImmediate(gameObject);
        }
    }
}
