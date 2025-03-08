using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;


public class BuildMenu : MonoBehaviour
{
    
    //[SerializeField] GameObject sceneState; //simply a placeholder until I can find a better/working technique to save an instance of the scene
    [SerializeField] private TMP_Text assetPlacement;
    private bool undoFlag;
    private bool redoFlag;
    private bool anchor;
    private bool assetChosen;
    private Stack<string> undoStack = new Stack<string>();
    private Stack<string> redoStack = new Stack<string>();
    private string[] asset = new string[3];//update size based on number of build assets added

    private void Start()
    {
        this.asset[0] = "Some asset 1";
        this.asset[1] = "Some asset 2";
        this.asset[2] = "Some asset 3";
    }

    public void Exit()//does not save, add functionality to prompt user to save build pregross before exiting
    {
        SceneManager.LoadScene("Level-Playing");
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

    /*
     * Undo redo implementation design: 
     * 
     * could use two stacks to keep track of game states to fetch (how to keep track/save game state?)
     * first stack will save updated data
     * second stack can save old data 
     * 
     * every time undo is called, stack1 will pop an item which will become the current state
     * redo will use stack2, anything poped from stack1 will be pushed to stack2
    */

    private void sceneStateSave()
    {
        string fetchScene;
        if (this.undoFlag == true &&  this.redoFlag == false)
        {
            fetchScene = this.undoStack.Pop();
            this.redoStack.Push(fetchScene);
            //need a way to load the new scene, either handled here or by another method call
        }else if(this.redoFlag == true && this.undoFlag == false)
        {
            fetchScene = this.redoStack.Pop();
            this.undoStack.Push(fetchScene);
            //would load the popped item here
        }
    }
    /*
     * How to handle drop down menu logic:
     * Keep track of index position and hardcode each asset to specified index (shouldn't worry about efficiency since there won't be that many assets
     * Don't know how to implement 'free movement' when pulling and dragging
     * Can have anchor points set as toggles and user can click desired anchor point after choosing an asset (no need to drag, may be stricter but easier to implement)
     */

    public void dropDownLibrary(int index)//assuming index is automatically provided after method call
    {
        if(index == 0)
        {
            assetPlacement.text = this.asset[0]; 
        }
        else if(index == 1)
        {
            assetPlacement.text = this.asset[1];
        }
        else if(index == 2)
        {
            assetPlacement.text = this.asset[2];
        }
    }


}
