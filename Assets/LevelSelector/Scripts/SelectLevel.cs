﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class SelectLevel : MonoBehaviour
{
    public UnityEvent<string, string> levelSelectedEvent;

    public List<LevelProperties> Levels;
    public TextMeshProUGUI PointsText;

    private int CurrentSelection;
    private bool CanSelect;
    private bool DialoguePlaying = false;

    public UnityEvent GiftRemovedHandler;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.LoadedIntoLevel)
            DialoguePlaying = true;

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
        if (DialoguePlaying)
            return;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CurrentSelection++;
            if (CurrentSelection > Levels.Count - 1)
                CurrentSelection = 0;

            ChangeSelection();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
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
            DataManager.SuccessText = Levels[CurrentSelection].ThanksText;
            DataManager.FailText = Levels[CurrentSelection].DenialDialog;
            DataManager.LoadedIntoLevel = true;

            DialoguePlaying = true;
            levelSelectedEvent?.Invoke(Levels[CurrentSelection].AcceptDialog, Levels[CurrentSelection].LevelName);
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
    }

    private void SetCanSelect()
    {
        CanSelect = DataManager.Gifts.TryGetValue(Levels[CurrentSelection].Gift, out DataManager.Status giftStatus) && giftStatus.GiftStatus == GiftStatus.stocked;
    }

    private string GetGiftName()
    {
        return Levels[CurrentSelection].Gift;
    }

    // To be called from an event in the Dialogue controller when text is completed
    public void Reenable()
    {
        DialoguePlaying = false;
    }
}
