using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class SelectLevel : MonoBehaviour
{
    public UnityEvent<string, string> levelSelectedEvent;

    public List<LevelProperties> Levels;
    public TextMeshProUGUI PointsText;

    public GameObject GiftSelector;
    public List<GameObject> GiftLocations;
    private int CurrentGiftSelection;
    private bool IsSelectingGift;

    private int CurrentSelection;
    private bool CanSelect;
    private bool DialoguePlaying = false;

    public UnityEvent GiftRemovedHandler;
    private string SelectedGift;


    // Start is called before the first frame update
    void Start()
    {


        if (DataManager.LoadedIntoLevel)
            DialoguePlaying = true;

        GiftSelector.SetActive(true);
        CurrentGiftSelection = GetNextIndexUp(0);

        IsSelectingGift = true;
        ChangeGiftSelection(); 
    }

    // Update is called once per frame
    void Update()
    {
        //if the player selects a level then draws the sprite at that location
        //if the player presses up or down the selection changes
        //if the player presses enter selects the level and the level starts
        if (DialoguePlaying)
            return;

        //if the player has already selected a gift
        if (!IsSelectingGift)
        {
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
        //if they have not selected a gift
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                CurrentGiftSelection = GetNextIndexUp(CurrentGiftSelection);
                ChangeGiftSelection();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                CurrentGiftSelection = GetNextIndexDown(CurrentGiftSelection);
                ChangeGiftSelection();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SelectGift();
            }
        }

    }

    #region Gift Select

    private void SelectGift()
    {
        SelectedGift = GiftLocations[CurrentGiftSelection].name;
        IsSelectingGift = false;
        GiftSelector.SetActive(false);

        SelectContinent();
    }

    private int GetNextIndexDown(int currentIndex)
    {
        if (CurrentGiftSelection - 1 == -1)
            return GiftLocations.Count - 1;

        int index = 0;

        for (int i = currentIndex - 1; i > 0; i--)
        {
            if (GiftLocations[i].activeSelf)
            {
                index = i;
                break;
            }
        }

        return index;
    }

    //helper method to get the next index up
    private int GetNextIndexUp(int currentIndex)
    {
        if (CurrentGiftSelection + 1 == GiftLocations.Count)
            return 0;

        int index = 0;

        for (int i = currentIndex + 1; i < GiftLocations.Count; i++)
        {
            if (GiftLocations[i].activeSelf)
            {
                index = i;
                break;
            }
        }

        return index;
    }

    private void ChangeGiftSelection()
    {
        GiftSelector.transform.position = new Vector3(
            GiftSelector.transform.position.x, 
            GiftLocations[CurrentGiftSelection].transform.position.y, 
            GiftLocations[CurrentGiftSelection].transform.position.z
        );
    }

    #endregion

    private void SelectContinent()
    {
        //sets the selector to the first place in the list
        CurrentSelection = 0;
        PointsText.text = "Gifts Delivered:  " + DataManager.Points.ToString();
        Levels[CurrentSelection].Hover.SetActive(true);
        ChangeSelection();
    }

    private void LevelSelect()
    {
        //transition into the scene
        //get name from the string array

        SetCanSelect();

        if (CanSelect)
        {
            //sets the gift to selected and the level selected to the one in question
            var temp = DataManager.Gifts[SelectedGift];
            temp.GiftStatus = GiftStatus.selected;
            temp.LevelUsedOn = Levels[CurrentSelection].LevelName;
            DataManager.Gifts[SelectedGift] = temp;

            //sets the gift status to delivered in the data manager
            GiftRemovedHandler.Invoke();


            DataManager.CurrentGiftLevel = GiftLocations[CurrentGiftSelection].name; //the gift selected

            var level = Levels[CurrentSelection].GiftToDialog;
            var levelDialogue = level.Find(delegate (DialogOptions options) {
                return options.Gift == SelectedGift;
            });

            DataManager.SuccessText = levelDialogue.ThanksText;
            DataManager.FailText = levelDialogue.DenialDialog;
            DataManager.LoadedIntoLevel = true;

            //very important so I dont forget this later
            //this script assumes that the gifts in the levels will be the same in order that they are in the world

            DialoguePlaying = true;
            levelSelectedEvent?.Invoke(levelDialogue.AcceptDialog, Levels[CurrentSelection].LevelName);
        }
        else
        {
            Debug.Log("Cant select");
        }
    }

    private void ChangeSelection()
    {
        foreach (LevelProperties level in Levels)
        {
            level.Hover.SetActive(false);
        }

        Levels[CurrentSelection].Hover.SetActive(true);
    }

    private void SetCanSelect()
    {
        foreach(KeyValuePair<string, DataManager.Status> kvp in DataManager.Gifts)
        {
            if (kvp.Value.GiftStatus == GiftStatus.stocked)
                CanSelect = true;
        }
    }

    private string GetGiftName()
    {
        return Levels[CurrentSelection].GiftToDialog[CurrentGiftSelection].Gift;
    }

    // To be called from an event in the Dialogue controller when text is completed
    public void Reenable()
    {
        DialoguePlaying = false;
    }
}
