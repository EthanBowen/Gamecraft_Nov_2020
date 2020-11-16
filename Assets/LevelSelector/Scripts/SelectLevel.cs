using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectLevel : MonoBehaviour
{
    public UnityEvent<string, string> levelSelectedEvent;

    public List<LevelProperties> Levels;
    public TextMeshProUGUI PointsText;

    public GameObject GiftSelector;
    public List<GameObject> GiftLocations;
    private int CurrentGiftSelection;
    private bool IsSelectingGift;

    private int CurrentSelection = -1;
    private bool CanSelect;
    private bool DialoguePlaying = false;
    private AudioSource source;

    private string SelectedGift;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.AllUsed())
            SceneManager.LoadScene("EndScene");

        if (DataManager.LoadedIntoLevel)
            DialoguePlaying = true;

        source = GetComponent<AudioSource>();

        GiftSelector.SetActive(true);
        CurrentGiftSelection = GetNextIndexUp(CurrentSelection);

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
                source.Play();
                LevelSelect();
            }
        }
        //if they have not selected a gift
        else
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                CurrentGiftSelection = GetNextIndexUp(CurrentGiftSelection);
                ChangeGiftSelection();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                CurrentGiftSelection = GetNextIndexDown(CurrentGiftSelection);
                ChangeGiftSelection();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                source.Play();
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
        if (currentIndex - 1 == -1)
            return GetNextIndexDown(GiftLocations.Count);

        int index = 0;

        for (int i = currentIndex - 1; i > 0; i--)
        {
            if (GiftLocations[i].activeSelf)
            {
                index = i;
                break;
            }
        }

        if (index == 0 && !GiftLocations[0].activeSelf)
            return GetNextIndexDown(GiftLocations.Count);

        return index;
    }

    //helper method to get the next index up
    private int GetNextIndexUp(int currentIndex)
    {
        // A way to loop around from the beginning
        if (currentIndex + 1 == GiftLocations.Count)
            return GetNextIndexUp(-1);

        int index = 0;

        for (int i = currentIndex + 1; i < GiftLocations.Count; i++)
        {
            if (GiftLocations[i].activeSelf)
            {
                index = i;
                break;
            }
        }

        if (index == 0 && !GiftLocations[0].activeSelf)
            return GetNextIndexUp(-1);

        return index;
    }

    private void ChangeGiftSelection()
    {
        GiftSelector.transform.position = GiftLocations[CurrentGiftSelection].transform.position; /*new Vector3(
            GiftSelector.transform.position.x, 
            GiftLocations[CurrentGiftSelection].transform.position.y, 
            GiftLocations[CurrentGiftSelection].transform.position.z
        );*/
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
