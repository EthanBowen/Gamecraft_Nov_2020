using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectLevel : MonoBehaviour
{
    //////////////////////IMPORTANT////////////////////////////
    //All these lists must have the SAME length
    public GameObject[] Levels = new GameObject[2];
    public GameObject SelectorSprite;
    public GameObject TextField;

    private int CurrentSelection;
    private GameObject Selector;
    private bool CanSelect;

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

        SetCanSelect();

        if (CanSelect)
        {
            Debug.Log("Can Select " + Levels[CurrentSelection].GetComponent<LevelProperties>().LevelName);

            //sets the gift status to delivered in the data manager
            DataManager.Gifts[Levels[CurrentSelection].GetComponent<LevelProperties>().Gift] = DataManager.GiftStatus.delivered;
            GiftRemovedHandler.Invoke();

            /////////////////////UNCOMMENT FOR LEVEL TRANSITION///////////////////////////
            //SceneManager.LoadScene(Levels[CurrentSelection].GetComponent<LevelProperties>().LevelName);
        }
        else
        {
            Debug.Log("Cant select");
        }
    }

    private void ChangeSelection()
    {
        //puts the sprite over the selected level
        Selector.transform.position = Levels[CurrentSelection].transform.position;

        SetCanSelect();

        if (CanSelect)
        {
            TextField.GetComponent<TextMeshProUGUI>().text = Levels[CurrentSelection].GetComponent<LevelProperties>().AcceptDialog;
        }
        else
        {
            //sets the text to display
            //thanks if the gift was delivered and denial if not
            if(DataManager.Gifts.TryGetValue(GetGiftName(), out DataManager.GiftStatus giftStatus ) && giftStatus == DataManager.GiftStatus.delivered)
            {
                TextField.GetComponent<TextMeshProUGUI>().text = Levels[CurrentSelection].GetComponent<LevelProperties>().ThanksText;
            }
            else
            {
                TextField.GetComponent<TextMeshProUGUI>().text = Levels[CurrentSelection].GetComponent<LevelProperties>().DenialDialog;
            }

        }
    }

    private void SetCanSelect()
    {
        CanSelect = DataManager.Gifts.TryGetValue(Levels[CurrentSelection].GetComponent<LevelProperties>().Gift, out DataManager.GiftStatus giftStatus) && giftStatus == DataManager.GiftStatus.stocked;
    }

    private string GetGiftName()
    {
        return Levels[CurrentSelection].GetComponent<LevelProperties>().Gift;
    }

}
