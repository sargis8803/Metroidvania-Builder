using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DragTest
{
    private GameObject prefabTester;
  //private Drag dragScript;
    /*Drag unit tests setup and what to test:
    *
    *Should just test for basic functionality, not using drag event handlers (only mouse down) 
    *Test cases should include state of prefab asset, position, interaction with mouse, state from drop down to scene (maybe integration for this one)
    *
    */

    [SetUp]
    public void Setup()
    {
       //should just instantiate prefab like how it is set up in the actual project:
        prefabTester = new GameObject("prefab");
        SpriteRenderer spriteRenderer = prefabTester.AddComponent<SpriteRenderer>();
        prefabTester.AddComponent<BoxCollider2D>();
        spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));
      //dragScript = prefabTester.AddComponent<Drag>();
        prefabTester.transform.position = Vector2.zero;//set soawn since assetplacemnet isnt present here
    }

    //Black box test here
    [Test]
    public void checkDragLogic()
    {
        Vector2 oirginalPosition = prefabTester.transform.position;//save original position, test to see if value changed
       
        prefabTester.GetComponent<Drag>().OnMouseDown();
        prefabTester.GetComponent<Drag>().OnMouseDrag();
        Assert.AreNotEqual(oirginalPosition, prefabTester.transform.position, "Position needed to change after dragging.");
    }
   
    //Black box test here
    [Test]
    public void checkScaleLogic()
    {
        Vector2 originalSize = prefabTester.transform.localScale;
        prefabTester.GetComponent<Drag>().OnMouseDown();
        prefabTester.GetComponent<Drag>().scalePrefab();

        //test to see prefab size changed 
        Assert.AreNotEqual(originalSize, prefabTester.transform.localScale, "Prefab size needed to change after scaling.");
    }

    //White box test here
    [Test]
    public void checkForScaleMaintain()
    {
        Vector2 originalSize = prefabTester.transform.localScale;
        prefabTester.transform.rotation = Quaternion.identity;//maintain defaut position
        prefabTester.GetComponent<Drag>().RotateObject();
        //should be checking for size consistency when rotate is called, rotate shouldnt affect the scaling attribute of the prefab
        Assert.AreEqual(originalSize, (Vector2)prefabTester.transform.localScale, "Prefab size shouldn't chnage after rotating.");
    }



    //Black box test here
    [Test]
    public void colorChange()
    {
        //testing to see successful color change on input (C key-down)
        Color currentColor = prefabTester.GetComponent<Renderer>().material.color;
        prefabTester.GetComponent<Drag>().OnMouseDown();
        prefabTester.GetComponent<Drag>().iterateColor();
        Assert.AreNotEqual(Color.white, currentColor, "Prefab should change color after parsing color array.");//white is default
    }



}

