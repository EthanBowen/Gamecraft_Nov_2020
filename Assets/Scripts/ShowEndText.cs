using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowEndText : MonoBehaviour
{
    public AudioClip winClip;
    public AudioClip loseClip;

    public TextMeshProUGUI EndText;
    public TextMeshProUGUI GiftDisplay;
    public string MenuSceneName = "MenuScene 1";

    [TextArea]
    public string GiftText;
    [TextArea]
    public string WinText;
    [TextArea]
    public string LoseText;

    private AudioSource source;
    private bool DeliveredAll;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        DeliveredAll = DataManager.AllDelivered();
        DisplayText();
    }

    private void DisplayText()
    {
        if (DeliveredAll)
        {
            source.clip = winClip;
            EndText.text = WinText;
        }
        else
        {
            source.clip = loseClip;
            EndText.text = LoseText;
        }

        source.Play();
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