using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameState: MonoBehaviour
{
    public static GameState instance;

    /*General schema for save state:
     *Have an object that saves all script object data for most scenes in the game 
     *Use this current script to recall that data, for access on save or loading
     *If user decides to save, we call that gameobject, then convert that information into a modulr JSON fie
     *When the user clicks load game, JSON file (which would be saved locally) will unload into the current scene? (not sure how to do this yet) 
     */

    private void Awake()
    {
        if(instance == null)
        {
            Debug.LogError("Error regarding current instance of game state");
        }
        instance = this;
    }


    public void insertLoadedGame()
    {

    }

    public void instertNewGameState()
    {

    }

    public void save()
    {

    }



}
