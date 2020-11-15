using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public KeyCode pauseKey = KeyCode.Escape;
    public GameObject pauseMenu;
    public GameObject settings;
    public GameObject credits;
    private bool paused = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(pauseKey))
        {
            if (paused)
                Unpause();
            else
                Pause();
        }
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        paused = true;
    }

    public void Unpause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        paused = false;
    }

    public void ToggleSettingsOpen()
    {
        if (credits.activeSelf)
            credits.SetActive(false);

        settings.SetActive(!settings.activeSelf);
    }

    public void ToggleCreditsOpen()
    {
        if(settings.activeSelf)
            settings.SetActive(false);
        
        credits.SetActive(!credits.activeSelf);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
