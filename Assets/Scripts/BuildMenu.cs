using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BuildMenu : MonoBehaviour
{
    //will refactor/include later undo redo among other logic 
    [SerializeField] public Transform assetPlacement;
    [SerializeField] public GameObject[] prefab;
    [SerializeField] public TMP_Dropdown dropdown;
    private bool undoFlag;
    private bool redoFlag;

    [SerializeField] public GameObject buildPanel;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void Awake()
    {
        if (dropdown != null)
        {
            dropdown.onValueChanged.RemoveAllListeners();
            dropdown.onValueChanged.AddListener(dropDownLibrary);
        }
       
    }

    public void Exit()//does not save, add functionality to prompt user to save build pregross before exiting
    {
        //SceneManager.LoadScene("Level-Playing");
        bool isBuildPanelOpen = buildPanel.activeSelf;
        buildPanel.SetActive(!isBuildPanelOpen);
        Time.timeScale = 1;
    }

    //setters for undo and redo flags:
    public void setUndo()//will be called on click
    {
        undoFlag = true;
        redoFlag = false;
        //call sceneStateSave here
    }

    public void setRedo()
    {
        redoFlag = true;
        undoFlag = false;
    }

    public void dropDownLibrary(int index)
    {
        if (index < 0 || index >= prefab.Length)
        {
            Debug.LogWarning("error index");
            Debug.LogWarning(index);
            return;
        }

        GameObject newAsset = Instantiate(prefab[index], assetPlacement.position, Quaternion.identity);
        newAsset.transform.SetParent(assetPlacement, true);



        if (newAsset.GetComponent<Drag>() == null)//need to check for prefab validity, see if script exists 
        {
            newAsset.AddComponent<Drag>();
        }

        Debug.Log(newAsset.name + " was spawned in scene");
    }
}
