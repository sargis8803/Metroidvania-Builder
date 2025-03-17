using NUnit.Framework;
using UnityEngine;
using TMPro;
using System.Linq;

public class BuildMenuTest
{
    private TMP_Dropdown dropdownMenu;
    private GameObject prefab;
    private GameObject prefabTester;
    private BuildMenu buildMenu;
   
    /*Need to test the buildmenu and canvas integration
     * 
     * Test if objects spawn correctly after clicking dropdown options
     * Test the prefab/assets themselves, should be clone instances outside of the build canvas for in game interaction
     * Test the dropdown menu itself, should maintain within the index bounds initialized in the project\
     * 
     * check if prefab has dragging logic
     *
     */

    [SetUp]
    public void Setup()
    {
        //again need to set up dropdown like how it is in the project
        prefabTester = new GameObject("prefabFromBuilder");
        buildMenu = prefabTester.AddComponent<BuildMenu>();
        dropdownMenu = prefabTester.AddComponent<TMP_Dropdown>();
        buildMenu.dropdown = dropdownMenu;
        prefab = new GameObject("prefabTester"); 
        prefab.AddComponent<Drag>();
        buildMenu.prefab = new GameObject[] { prefab };
        GameObject assetPlacement = new GameObject("AssetPlacement");
        buildMenu.assetPlacement = assetPlacement.transform;
        buildMenu.Awake();
    }


  

    //Black box test here
    [Test]
    public void prefabSpawn()
    {
        //testig for existence of prefab when spawned
        dropdownMenu.value = 0;//positioning in dropdown, should be called when pressed
        dropdownMenu.onValueChanged.Invoke(dropdownMenu.value);
        GameObject newAsset = GameObject.Find("assetClone");
        Assert.IsNotNull(newAsset, "Prefab should exist.");
        Assert.AreEqual(buildMenu.assetPlacement.position, newAsset.transform.position, "Prefab position should be at spawn location");
    }

    //Black box test here
    [Test]
    public void OutsideDropDownIndex()
    {
        dropdownMenu.value = -1;
        dropdownMenu.onValueChanged.Invoke(dropdownMenu.value);
        GameObject newAsset = GameObject.FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None).FirstOrDefault(obj => obj.name == "assetClone");
        Assert.IsNull(newAsset, "Prefab should be null, does not exist given menu index");
    }

    //Black box test here 
    [Test]
    public void checkDragScript()
    {
        dropdownMenu.value = 0;
        dropdownMenu.onValueChanged.Invoke(dropdownMenu.value);
        GameObject newAsset = GameObject.Find("assetClone");
        Assert.IsNotNull(newAsset.GetComponent<Drag>(), "Drag script/logic should be included in prefab after spawning");
    }

    
}
