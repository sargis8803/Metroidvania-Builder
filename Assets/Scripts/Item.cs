using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;

    private GearManager gearManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gearManager = GameObject.Find("GearCanvas").GetComponent<GearManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gearManager.AddItem(itemName, sprite);
            Destroy(gameObject);
        }
    }
}
