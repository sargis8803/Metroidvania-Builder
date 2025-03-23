using NUnit.Framework;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildDragIntegration
{
    private GameObject prefab;
    private BuildMenu buildMenu;
    private TMP_Dropdown dropdownMenu;
    //integration test across buildmenu and drag scripts


    [SetUp]
    public void Setup()
    {
        GameObject initializeBM = new GameObject();
        buildMenu = initializeBM.AddComponent<BuildMenu>();
        GameObject spawnPoint = new GameObject("AssetPlacementspawn");
        buildMenu.assetPlacement = spawnPoint.transform;
        GameObject initializeDD = new GameObject("Dropdown");
        dropdownMenu = initializeDD.AddComponent<TMP_Dropdown>();
        buildMenu.dropdown = dropdownMenu;
        prefab = new GameObject("prefab");
        prefab.AddComponent<Drag>();
        buildMenu.prefab = new GameObject[] {prefab };

        dropdownMenu.options.Add(new TMP_Dropdown.OptionData("prefab")); // Adding an option to the dropdown 
    }


    //Integration test for build menu and drag, checking for interaction flow after multiple inputs
    [Test]
    public void BMAndDragInteraction()
    {
        
        dropdownMenu.value = 0;
        dropdownMenu.onValueChanged.Invoke(dropdownMenu.value);
        GameObject prefab = GameObject.Find("prefabClone");
        Assert.IsNotNull(prefab, "Prefab should exist.");
        prefab.GetComponent<Drag>().OnMouseDown();
        Assert.AreNotEqual(Vector2.zero, prefab.transform.position, "Prefab position needed to change");
        Assert.IsNotNull(prefab.GetComponent<Drag>(), "Drag logic should be in asset/prefab");

        Vector2 scale = prefab.transform.localScale;
        Assert.Greater(scale.x, 0, "Horizonatal size the prefab should have changed");
        Assert.Greater(scale.y, 0, "Verticle size the prefab should have changed");
    }



   


}
