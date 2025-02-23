using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.Rendering;

public class BuildMenu : MonoBehaviour
{
    [SerializeField] GameObject sceneState; //simply a placeholder until I can find a better/working technique to save an instance of the scene
    private bool undoFlag;
    private bool redoFlag;
    private Stack<string> undoStack = new Stack<string>();
    private Stack<string> redoStack = new Stack<string>();
    private string[] asset = new string[3];//update size based on number of build assets added

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




}
