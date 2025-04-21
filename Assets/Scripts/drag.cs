using UnityEngine;

public class Drag : MonoBehaviour
{
    private Vector2 difference = Vector2.zero;//to maintain mouse to prefab positioning
    private float scaleRate = 0.1f;
    private Color originalColor;
    private SpriteRenderer objectRenderer;
    private Color[] colors = { Color.red, Color.blue, Color.green };
    private int currentColor = -1;

    private void Start()
    {
        objectRenderer = GetComponent<SpriteRenderer>();
        originalColor = objectRenderer.material.color;
    }

    //adding color interaction when clicking on prefab for visual context
    public void OnMouseDown()
    {
        ChangeColor(originalColor);
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }

    public void OnMouseDrag()
    {
        
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
        if (Input.GetKey(KeyCode.Space))
        {
            scalePrefab();
        }

        if (Input.GetKey(KeyCode.R))
        {
            RotateObject();
        }

        if (Input.GetKeyDown(KeyCode.C))//not working?
        {
            iterateColor();
        }
    }

    

    public void scalePrefab()
    {
        Vector3 newScale = transform.localScale + new Vector3(scaleRate, scaleRate, 0);
        newScale = new Vector3(Mathf.Max(newScale.x, 0.1f), Mathf.Max(newScale.y, 0.1f), 1);
        transform.localScale = newScale;
    }

    private void ChangeColor(Color newColor)
    {
        if (objectRenderer == null)
        {
            objectRenderer = GetComponent<SpriteRenderer>();
        }

        if (objectRenderer != null)
        {
            objectRenderer.sharedMaterial.color = newColor;
        }
        
    }

    public void iterateColor()
    {
        currentColor = (currentColor + 1) % (colors.Length + 1);
        if (currentColor == colors.Length)
        {
            ChangeColor(originalColor);
        }
        else
        {
            ChangeColor(colors[currentColor]);
        }
    }

    public void RotateObject()
    {
        transform.Rotate(Vector3.forward, 100f * Time.deltaTime);
    }
}
