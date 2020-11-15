using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectLevel : MonoBehaviour
{
    public List<LevelProperties> Levels;
    public GameObject TextField;
    public TextMeshProUGUI PointsText;

    private int CurrentSelection;
    private bool CanSelect;

    public UnityEvent GiftRemovedHandler;


    // Start is called before the first frame update
    void Start()
    {
        //sets the selector to the first place in the list
        CurrentSelection = 0;
        PointsText.text = "Gifts Delivered:  " + DataManager.Points.ToString();
        Levels[CurrentSelection].Hover.SetActive(true);
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
            if (CurrentSelection > Levels.Count - 1)
                CurrentSelection = 0;

            ChangeSelection();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CurrentSelection--;
            if (CurrentSelection < 0)
                CurrentSelection = Levels.Count - 1;

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
            Debug.Log("Can Select " + Levels[CurrentSelection].LevelName);

            var temp = DataManager.Gifts[Levels[CurrentSelection].Gift];
            temp.GiftStatus = GiftStatus.delivered;
            temp.LevelUsedOn = Levels[CurrentSelection].LevelName;
            DataManager.Gifts[Levels[CurrentSelection].Gift] = temp;

            //sets the gift status to delivered in the data manager
            GiftRemovedHandler.Invoke();

            DataManager.CurrentGiftLevel = Levels[CurrentSelection].Gift;
            /////////////////////UNCOMMENT FOR LEVEL TRANSITION///////////////////////////
            SceneManager.LoadScene(Levels[CurrentSelection].LevelName);
        }
        else
        {
            Debug.Log("Cant select");
        }
    }

    private void ChangeSelection()
    {
        foreach(LevelProperties level in Levels)
        {
            level.Hover.SetActive(false);
        }

        Levels[CurrentSelection].Hover.SetActive(true);

        SetCanSelect();

        if (CanSelect)
        {
            TextField.GetComponent<TextMeshProUGUI>().text = Levels[CurrentSelection].AcceptDialog;
        }
        else
        {
            //sets the text to display
            //thanks if the gift was delivered and denial if not
            DataManager.Gifts.TryGetValue(GetGiftName(), out DataManager.Status giftStatus);
            if ( giftStatus.GiftStatus == GiftStatus.delivered && giftStatus.LevelUsedOn == Levels[CurrentSelection].LevelName)
            {
                TextField.GetComponent<TextMeshProUGUI>().text = Levels[CurrentSelection].ThanksText;
            }
            else
            {
                TextField.GetComponent<TextMeshProUGUI>().text = Levels[CurrentSelection].DenialDialog;
            }

        }
    }

    private void SetCanSelect()
    {
        CanSelect = DataManager.Gifts.TryGetValue(Levels[CurrentSelection].Gift, out DataManager.Status giftStatus) && giftStatus.GiftStatus == GiftStatus.stocked;
    }

    private string GetGiftName()
    {
        return Levels[CurrentSelection].Gift;
    }
}
