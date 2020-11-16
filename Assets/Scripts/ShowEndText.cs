using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowEndText : MonoBehaviour
{
    public TextMeshProUGUI EndText;
    public TextMeshProUGUI GiftDisplay;
    public string MenuSceneName = "MenuScene 1";

    [TextArea]
    public string GiftText;
    [TextArea]
    public string WinText;
    [TextArea]
    public string LoseText;

    private bool DeliveredAll;

    // Start is called before the first frame update
    void Start()
    {
        DeliveredAll = DataManager.AllDelivered();

        DisplayText();
    }

    private void DisplayText()
    {
        if (DeliveredAll)
            EndText.text = WinText;
        else
            EndText.text = LoseText;

        GiftDisplay.text = GiftText + DataManager.GetNumGiftDelivered();
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnReset()
    {
        DataManager.Reset();
        SceneManager.LoadScene(MenuSceneName);
    }
    
}