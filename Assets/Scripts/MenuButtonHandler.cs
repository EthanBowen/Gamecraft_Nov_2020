using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonHandler : MonoBehaviour
{
    public GameObject CreditsMenu;
    public GameObject InstructionsMenu;

    public void OnCredits()
    {
        
        InstructionsMenu.SetActive(false);
        CreditsMenu.SetActive(!CreditsMenu.activeSelf);
    }

    public void OnInstructions()
    {
        CreditsMenu.SetActive(false);
        InstructionsMenu.SetActive(!InstructionsMenu.activeSelf);
    }
}
