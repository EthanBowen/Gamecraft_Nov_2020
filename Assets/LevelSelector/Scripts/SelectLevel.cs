using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    //////////////////////IMPORTANT////////////////////////////
    //All these lists must have the SAME length
    public GameObject[] Levels = new GameObject[2];
    public string[] LevelNames = new string[2];
    //public List<string> Gifts = new List<string>();

    public GameObject SelectorSprite;
    int CurrentSelection;
    private GameObject Selector;

    bool CanSelect;

    public UnityEvent GiftRemovedHandler;


    // Start is called before the first frame update
    void Start()
    {
        //sets the selector to the first place in the list
        CurrentSelection = 0;
        Selector = Instantiate(SelectorSprite, Levels[CurrentSelection].transform);
        ChangeSelection();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player selects a level then draws the sprite at that location
        //if the player presses up or down the selection changes
        //if the player presses enter selects the level and the level starts

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CurrentSelection++;
            if (CurrentSelection > Levels.Length - 1)
                CurrentSelection = 0;

            ChangeSelection();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CurrentSelection--;
            if (CurrentSelection < 0)
                CurrentSelection = Levels.Length - 1;

             ChangeSelection();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
             LevelSelect();
        }
    }


    private void LevelSelect()
    {
        //transition into the scene
        //get name from the string array

        CanSelect = DataManager.Gifts.Contains(Levels[CurrentSelection].GetComponent<LevelProperties>().Gift);

        if (CanSelect)
        {
            Debug.Log("Can Select" + LevelNames[CurrentSelection]);
            DataManager.Gifts.Remove(Levels[CurrentSelection].GetComponent<LevelProperties>().Gift);
            GiftRemovedHandler.Invoke();
            
            //SceneManager.LoadScene(LevelNames[CurrentSelection]);
        }
        else
        {
            Debug.Log("Cant select");
        }
    }

    private void ChangeSelection()
    {
        //throw new NotImplementedException();

        //puts the sprite over the selected level
        Selector.transform.position = Levels[CurrentSelection].transform.position;

        CanSelect = DataManager.Gifts.Contains(Levels[CurrentSelection].GetComponent<LevelProperties>().Gift);
    }

}
